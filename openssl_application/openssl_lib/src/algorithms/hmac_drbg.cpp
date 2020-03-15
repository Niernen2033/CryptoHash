#include <hmac_drbg.h>
#include <crypto_types.h>
#include <crypto_result.h>
#include <crypto_utils.h>
#include <e_assert.h>
#include <NLog.h>

typedef enum hmac_drbg_status_e
{
	HMAC_DRBG_UNINSTANTIATE = 0,
	HMAC_DRBG_INSTANTIATE,
	HMAC_DRBG_RESEED,
	HMAC_DRBG_GENERATE
} hmac_drbg_status_e;

typedef struct hmac_drbg_temp_crypto_data_t
{
	uint8_t buffer[HMAC_DRBG_MAX_OUTLEN_BYTES];
	uint32_t bytes;
} hmac_drbg_temp_crypto_data_t;

typedef struct internal_hmac_drbg_state_t
{
	sha_type_e shaType;
	bool predictionResistance;
	uint8_t V[HMAC_DRBG_MAX_OUTLEN_BYTES];
	hmac_drbg_temp_crypto_data_t tmpV;
	uint8_t Key[HMAC_DRBG_MAX_OUTLEN_BYTES];
	hmac_drbg_temp_crypto_data_t tmpK;
	uint32_t outlenBytes;
	uint64_t reseedCounter;
	crypto_buffer_t tmpHashData;
	hmac_drbg_status_e currentStatus;
	crypto_status_e health;
} internal_hmac_drbg_state_t;

static std::unique_ptr<internal_hmac_drbg_state_t> internalHmacDrbgData;
static bool hmacDrbgInitStatus = false;

static crypto_status_e Hmac_Drbg_GenerateHashFromMessage(sha_type_e shaType, uint8_t* key, uint32_t keyBytes,
	uint8_t* message, uint32_t messageBytes,
	uint8_t* digest, uint32_t digestBytes);
static crypto_status_e Hmac_Drbg_Internal_Update(uint8_t* providedData, uint32_t providedDataBytes);
static crypto_status_e Hmac_Drbg_Internal_Instantiate(uint8_t* entropy, uint32_t entropyBytes,
	uint8_t* nonce, uint32_t nonceBytes,
	uint8_t* personalizationString, uint32_t personalizationStringBytes);
static crypto_status_e Hmac_Drbg_Internal_Reseed(uint8_t* entropy, uint32_t entropyBytes,
	uint8_t* additionalInput, uint32_t additionalInputBytes);
static crypto_status_e Hmac_Drbg_Internal_Generate(uint32_t bytesRequested,
	uint8_t* additionalInput, uint32_t additionalInputBytes, crypto_buffer_t* digset);


bool Hmac_Drbg_Init()
{
	hmacDrbgInitStatus = false;
	internalHmacDrbgData = std::make_unique<internal_hmac_drbg_state_t>();
	if (internalHmacDrbgData == nullptr)
	{
		ASSERT_M(false, "hmacDrbgInitStatus = false");
		return false;
	}
	memsetAssert(internalHmacDrbgData.get(), sizeof(internal_hmac_drbg_state_t), 0x00, sizeof(internal_hmac_drbg_state_t));
	hmacDrbgInitStatus = true;
}

bool Hmac_Drbg_Cleanup()
{
	if (hmacDrbgInitStatus)
	{
		internalHmacDrbgData.reset(nullptr);
		hmacDrbgInitStatus = false;
	}
	return true;
}

crypto_status_e Hmac_Drbg_Instantiate(bool requestPredictionResistance, sha_type_e shaType,
	uint8_t* personalizationString, uint32_t personalizationStringBytes,
	uint8_t* entropy, uint32_t entropyBytes,
	uint8_t* nonce, uint32_t nonceBytes)
{
	memsetAssert(internalHmacDrbgData.get(), sizeof(internal_hmac_drbg_state_t), 0x00, sizeof(internal_hmac_drbg_state_t));

	if ((personalizationStringBytes != 0 && personalizationString == NULL))
	{
		internalHmacDrbgData->health = CRYPTO_NULL_PTR_ERROR;
		NLog_Error("(personalizationStringBytes != 0 && personalizationString == NULL)");
		return CRYPTO_NULL_PTR_ERROR;
	}

	if (entropyBytes > ENTROPY_INPUT_MAX_SIZE_BYTES)
	{
		internalHmacDrbgData->health = CRYPTO_BUFFER_MISMATCH_ERROR;
		NLog_Error("entropyBytes > ENTROPY_INPUT_MAX_SIZE_BYTES");
		return CRYPTO_BUFFER_MISMATCH_ERROR;
	}

	if (nonceBytes > NONCE_MAX_SIZE_BYTES)
	{
		internalHmacDrbgData->health = CRYPTO_BUFFER_MISMATCH_ERROR;
		NLog_Error("nonceBytes > NONCE_MAX_SIZE_BYTES");
		return CRYPTO_BUFFER_MISMATCH_ERROR;
	}

	if ((entropyBytes * BITS_PER_BYTE) < SECURITY_STRENGTH_BITS)
	{
		internalHmacDrbgData->health = CRYPTO_BUFFER_MISMATCH_ERROR;
		NLog_Error("(entropyBytes * BITS_PER_BYTE) < SECURITY_STRENGTH_BITS");
		return CRYPTO_BUFFER_MISMATCH_ERROR;
	}

	if (personalizationStringBytes > PERSONALIZATION_STRING_MAX_SIZE_BYTES)
	{
		internalHmacDrbgData->health = CRYPTO_BUFFER_MISMATCH_ERROR;
		NLog_Error("personalizationStringBytes > PERSONALIZATION_STRING_MAX_SIZE_BYTES");
		return CRYPTO_BUFFER_MISMATCH_ERROR;
	}

	internalHmacDrbgData->predictionResistance = requestPredictionResistance;
	internalHmacDrbgData->shaType = shaType;
	internalHmacDrbgData->outlenBytes = Sha_GetDigsetBytes(shaType);

	internalHmacDrbgData->health = Hmac_Drbg_Internal_Instantiate(entropy, entropyBytes, nonce, nonceBytes, personalizationString, personalizationStringBytes);
	if (internalHmacDrbgData->health == CRYPTO_SUCCESS)
	{
		internalHmacDrbgData->currentStatus = HMAC_DRBG_INSTANTIATE;
	}
	return internalHmacDrbgData->health;
}

crypto_status_e Hmac_Drbg_Reseed(bool requestPredictionResistance,
	uint8_t* entropy, uint32_t entropyBytes,
	uint8_t* additionalInput, uint32_t additionalInputBytes)
{
	if (internalHmacDrbgData->health != CRYPTO_SUCCESS)
	{
		return internalHmacDrbgData->health;
	}

	if (internalHmacDrbgData->currentStatus == HMAC_DRBG_UNINSTANTIATE)
	{
		return CRYPTO_ABORT_ERROR;
	}

	if (additionalInputBytes != 0 && additionalInput == NULL)
	{
		internalHmacDrbgData->health = CRYPTO_NULL_PTR_ERROR;
		return CRYPTO_NULL_PTR_ERROR;
	}

	if (entropyBytes > ENTROPY_INPUT_MAX_SIZE_BYTES)
	{
		internalHmacDrbgData->health = CRYPTO_BUFFER_MISMATCH_ERROR;
		return CRYPTO_BUFFER_MISMATCH_ERROR;
	}

	if (additionalInputBytes > ADDITIONAL_INPUT_MAX_SIZE_BYTES)
	{
		internalHmacDrbgData->health = CRYPTO_BUFFER_MISMATCH_ERROR;
		return CRYPTO_BUFFER_MISMATCH_ERROR;
	}

	if ((entropyBytes * BITS_PER_BYTE) < SECURITY_STRENGTH_BITS)
	{
		internalHmacDrbgData->health = CRYPTO_BUFFER_MISMATCH_ERROR;
		return CRYPTO_BUFFER_MISMATCH_ERROR;
	}

	internalHmacDrbgData->predictionResistance = requestPredictionResistance;

	internalHmacDrbgData->health = Hmac_Drbg_Internal_Reseed(entropy, entropyBytes, additionalInput, additionalInputBytes);
	if (internalHmacDrbgData->health == CRYPTO_SUCCESS)
	{
		internalHmacDrbgData->currentStatus = HMAC_DRBG_RESEED;
	}
	return internalHmacDrbgData->health;
}

crypto_status_e Hmac_Drbg_Generate(uint32_t bytesRequested,
	uint8_t* entropy, uint32_t entropyBytes,
	uint8_t* additionalInput, uint32_t additionalInputBytes,
	crypto_buffer_t* bytesReturned)
{
	if (internalHmacDrbgData->health != CRYPTO_SUCCESS)
	{
		return internalHmacDrbgData->health;
	}

	if (internalHmacDrbgData->currentStatus == HMAC_DRBG_UNINSTANTIATE)
	{
		return CRYPTO_ABORT_ERROR;
	}

	if (additionalInputBytes != 0 && additionalInput == NULL)
	{
		internalHmacDrbgData->health = CRYPTO_NULL_PTR_ERROR;
		return CRYPTO_NULL_PTR_ERROR;
	}

	if (additionalInputBytes > ADDITIONAL_INPUT_MAX_SIZE_BYTES)
	{
		internalHmacDrbgData->health = CRYPTO_BUFFER_MISMATCH_ERROR;
		return CRYPTO_BUFFER_MISMATCH_ERROR;
	}

	if (bytesRequested > BITS_TO_BYTES(MAXIMUM_NUMBER_OF_BITS_PER_REQEUST))
	{
		internalHmacDrbgData->health = CRYPTO_BUFFER_MISMATCH_ERROR;
		return CRYPTO_BUFFER_MISMATCH_ERROR;
	}

	if (internalHmacDrbgData->predictionResistance)
	{
		if (entropyBytes != 0 && entropy == NULL)
		{
			internalHmacDrbgData->health = CRYPTO_NULL_PTR_ERROR;
			return CRYPTO_NULL_PTR_ERROR;
		}

		if (entropyBytes > ENTROPY_INPUT_MAX_SIZE_BYTES)
		{
			internalHmacDrbgData->health = CRYPTO_BUFFER_MISMATCH_ERROR;
			return CRYPTO_BUFFER_MISMATCH_ERROR;
		}

		internalHmacDrbgData->health = Hmac_Drbg_Reseed(internalHmacDrbgData->predictionResistance, entropy, entropyBytes, additionalInput, additionalInputBytes);

		if (internalHmacDrbgData->health != CRYPTO_SUCCESS)
		{
			return internalHmacDrbgData->health;
		}

		internalHmacDrbgData->health = Hmac_Drbg_Internal_Generate(bytesRequested, NULL, 0, bytesReturned);
	}
	else
	{

		internalHmacDrbgData->health = Hmac_Drbg_Internal_Generate(bytesRequested, additionalInput, additionalInputBytes, bytesReturned);
	}
	if (internalHmacDrbgData->health == CRYPTO_SUCCESS)
	{
		bytesReturned->bytes = bytesRequested;
		internalHmacDrbgData->currentStatus = HMAC_DRBG_GENERATE;
	}
	return internalHmacDrbgData->health;
}

crypto_status_e Hmac_Drbg_Uninstantiate()
{
	memsetAssert(internalHmacDrbgData.get(), sizeof(internal_hmac_drbg_state_t), 0x00, sizeof(internal_hmac_drbg_state_t));
	return CRYPTO_SUCCESS;
}

static crypto_status_e Hmac_Drbg_Internal_Generate(uint32_t bytesRequested,
	uint8_t* additionalInput, uint32_t additionalInputBytes, crypto_buffer_t* digset)
{
	internalHmacDrbgData->tmpK.bytes = internalHmacDrbgData->outlenBytes;
	internalHmacDrbgData->tmpV.bytes = internalHmacDrbgData->outlenBytes;

	uint32_t bytesGenerated = 0;

	memsetAssert(digset->buffer, sizeof(digset->buffer), 0x00, CRYPTO_BUFFER_DEFAULT_BYTES);

	if (internalHmacDrbgData->reseedCounter > RESEED_INTERVAL)
	{
		internalHmacDrbgData->health = CRYPTO_BUFFER_MISMATCH_ERROR;
		return CRYPTO_BUFFER_MISMATCH_ERROR;
	}

	if (additionalInput != NULL && additionalInputBytes != 0)
	{
		internalHmacDrbgData->health = Hmac_Drbg_Internal_Update(additionalInput, additionalInputBytes);

		if (internalHmacDrbgData->health != CRYPTO_SUCCESS)
		{
			return internalHmacDrbgData->health;
		}
	}

	memcpyAssert(internalHmacDrbgData->tmpV.buffer, sizeof(internalHmacDrbgData->tmpV.buffer), internalHmacDrbgData->V, internalHmacDrbgData->outlenBytes);

	while (bytesGenerated < bytesRequested)
	{
		internalHmacDrbgData->health = Hmac_Drbg_GenerateHashFromMessage(internalHmacDrbgData->shaType,
			internalHmacDrbgData->Key, internalHmacDrbgData->outlenBytes,
			internalHmacDrbgData->tmpV.buffer, internalHmacDrbgData->tmpV.bytes,
			internalHmacDrbgData->tmpV.buffer, internalHmacDrbgData->tmpV.bytes);

		if (internalHmacDrbgData->health != CRYPTO_SUCCESS)
		{
			return internalHmacDrbgData->health;
		}

		if ((bytesGenerated + internalHmacDrbgData->tmpV.bytes) <= bytesRequested)
		{
			if ((bytesGenerated + internalHmacDrbgData->tmpV.bytes) > CRYPTO_BUFFER_DEFAULT_BYTES)
			{
				internalHmacDrbgData->health = CRYPTO_BUFFER_OVERFLOW_ERROR;
				return CRYPTO_BUFFER_OVERFLOW_ERROR;
			}
			memcpyAssert(digset->buffer + bytesGenerated, sizeof(digset->buffer) - bytesGenerated, internalHmacDrbgData->tmpV.buffer, internalHmacDrbgData->tmpV.bytes);
		}
		else
		{
			uint32_t bytesRemaining = bytesRequested - bytesGenerated;
			if ((bytesGenerated + bytesRemaining) > CRYPTO_BUFFER_DEFAULT_BYTES)
			{
				internalHmacDrbgData->health = CRYPTO_BUFFER_OVERFLOW_ERROR;
				return CRYPTO_BUFFER_OVERFLOW_ERROR;
			}
			memcpyAssert(digset->buffer + bytesGenerated, sizeof(digset->buffer) - bytesGenerated, internalHmacDrbgData->tmpV.buffer, bytesRemaining);
		}
		bytesGenerated += internalHmacDrbgData->tmpV.bytes;
	}

	memcpyAssert(internalHmacDrbgData->V, sizeof(internalHmacDrbgData->V), internalHmacDrbgData->tmpV.buffer, internalHmacDrbgData->tmpV.bytes);

	internalHmacDrbgData->health = Hmac_Drbg_Internal_Update(additionalInput, additionalInputBytes);

	if (internalHmacDrbgData->health != CRYPTO_SUCCESS)
	{
		return internalHmacDrbgData->health;
	}

	internalHmacDrbgData->reseedCounter++;
	memsetAssert(&internalHmacDrbgData->tmpK, sizeof(internalHmacDrbgData->tmpK), 0x00, sizeof(internalHmacDrbgData->tmpK));
	memsetAssert(&internalHmacDrbgData->tmpV, sizeof(internalHmacDrbgData->tmpV), 0x00, sizeof(internalHmacDrbgData->tmpV));

	return internalHmacDrbgData->health;
}

static crypto_status_e Hmac_Drbg_Internal_Reseed(uint8_t* entropy, uint32_t entropyBytes,
	uint8_t* additionalInput, uint32_t additionalInputBytes)
{
	if (entropyBytes > ENTROPY_INPUT_MAX_SIZE_BYTES)
	{
		internalHmacDrbgData->health = CRYPTO_BUFFER_MISMATCH_ERROR;
		return CRYPTO_BUFFER_MISMATCH_ERROR;
	}

	if (additionalInputBytes > ADDITIONAL_INPUT_MAX_SIZE_BYTES)
	{
		internalHmacDrbgData->health = CRYPTO_BUFFER_MISMATCH_ERROR;
		return CRYPTO_BUFFER_MISMATCH_ERROR;
	}

	if (entropyBytes + additionalInputBytes > HMAC_DRBG_SEED_MATERIAL_BYTES)
	{
		internalHmacDrbgData->health = CRYPTO_BUFFER_MISMATCH_ERROR;
		return CRYPTO_BUFFER_MISMATCH_ERROR;
	}

	uint8_t seedMaterial[HMAC_DRBG_SEED_MATERIAL_BYTES];
	uint8_t seedMaterialBytes = 0;
	memsetAssert(seedMaterial, sizeof(seedMaterial), 0x00, HMAC_DRBG_SEED_MATERIAL_BYTES);

	memcpyAssert(seedMaterial, sizeof(seedMaterial), entropy, entropyBytes);
	seedMaterialBytes += entropyBytes;

	memcpyAssert(seedMaterial + seedMaterialBytes, sizeof(seedMaterial) - seedMaterialBytes, additionalInput, additionalInputBytes);
	seedMaterialBytes += additionalInputBytes;

	internalHmacDrbgData->health = Hmac_Drbg_Internal_Update(seedMaterial, seedMaterialBytes);
	internalHmacDrbgData->reseedCounter = 1;

	return internalHmacDrbgData->health;
}

static crypto_status_e Hmac_Drbg_Internal_Instantiate(uint8_t* entropy, uint32_t entropyBytes,
	uint8_t* nonce, uint32_t nonceBytes,
	uint8_t* personalizationString, uint32_t personalizationStringBytes)
{
	uint8_t seedMaterial[HMAC_DRBG_SEED_MATERIAL_BYTES];
	uint8_t seedMaterialBytes = 0;

	memsetAssert(seedMaterial, sizeof(seedMaterial), 0x00, HMAC_DRBG_SEED_MATERIAL_BYTES);
	if (entropyBytes > ENTROPY_INPUT_MAX_SIZE_BYTES)
	{
		internalHmacDrbgData->health = CRYPTO_BUFFER_MISMATCH_ERROR;
		return CRYPTO_BUFFER_MISMATCH_ERROR;
	}

	if (personalizationStringBytes > PERSONALIZATION_STRING_MAX_SIZE_BYTES)
	{
		internalHmacDrbgData->health = CRYPTO_BUFFER_MISMATCH_ERROR;
		return CRYPTO_BUFFER_MISMATCH_ERROR;
	}

	if (nonceBytes > NONCE_MAX_SIZE_BYTES)
	{
		internalHmacDrbgData->health = CRYPTO_BUFFER_MISMATCH_ERROR;
		return CRYPTO_BUFFER_MISMATCH_ERROR;
	}

	uint32_t totalBytes = entropyBytes + nonceBytes + personalizationStringBytes;
	if (totalBytes > HMAC_DRBG_SEED_MATERIAL_BYTES)
	{
		internalHmacDrbgData->health = CRYPTO_BUFFER_MISMATCH_ERROR;
		return CRYPTO_BUFFER_MISMATCH_ERROR;
	}

	//seedMaterial = entropy_input || nonce || personalization_string
	memcpyAssert(seedMaterial, sizeof(seedMaterial), entropy, entropyBytes);
	seedMaterialBytes += entropyBytes;

	memcpyAssert(seedMaterial + seedMaterialBytes, sizeof(seedMaterial) - seedMaterialBytes, nonce, nonceBytes);
	seedMaterialBytes += nonceBytes;

	memcpyAssert(seedMaterial + seedMaterialBytes, sizeof(seedMaterial) - seedMaterialBytes, personalizationString, personalizationStringBytes);
	seedMaterialBytes += personalizationStringBytes;

	memsetAssert(internalHmacDrbgData->Key, sizeof(internalHmacDrbgData->Key), 0x00, internalHmacDrbgData->outlenBytes);
	memsetAssert(internalHmacDrbgData->V, sizeof(internalHmacDrbgData->V), 0x01, internalHmacDrbgData->outlenBytes);


	internalHmacDrbgData->health = Hmac_Drbg_Internal_Update(seedMaterial, seedMaterialBytes);
	internalHmacDrbgData->reseedCounter = 1;

	return internalHmacDrbgData->health;
}

static crypto_status_e Hmac_Drbg_Internal_Update(uint8_t* providedData, uint32_t providedDataBytes)
{
	internalHmacDrbgData->tmpK.bytes = internalHmacDrbgData->outlenBytes;
	internalHmacDrbgData->tmpV.bytes = internalHmacDrbgData->outlenBytes;

	uint8_t buffer[UPDATE_TEMP_BUFFER_BYTES];
	uint32_t bufferBytes = 0;
	memsetAssert(buffer, sizeof(buffer), 0x00, UPDATE_TEMP_BUFFER_BYTES);

	uint8_t zero = 0;
	uint8_t one = 1;

	memsetAssert(internalHmacDrbgData->tmpK.buffer, sizeof(internalHmacDrbgData->tmpK.buffer), 0x00, internalHmacDrbgData->tmpK.bytes);
	memsetAssert(internalHmacDrbgData->tmpV.buffer, sizeof(internalHmacDrbgData->tmpV.buffer), 0x00, internalHmacDrbgData->tmpV.bytes);

	if (providedDataBytes > 0 && providedData == NULL)
	{
		internalHmacDrbgData->health = CRYPTO_NULL_PTR_ERROR;
		return CRYPTO_NULL_PTR_ERROR;
	}

	memsetAssert(buffer, sizeof(buffer), 0x00, UPDATE_TEMP_BUFFER_BYTES);
	memcpyAssert(buffer, sizeof(buffer), internalHmacDrbgData->V, internalHmacDrbgData->outlenBytes);
	bufferBytes += internalHmacDrbgData->outlenBytes;

	memcpyAssert(buffer + bufferBytes, UPDATE_TEMP_BUFFER_BYTES - bufferBytes, &zero, 1);
	bufferBytes += 1;

	memcpyAssert(buffer + bufferBytes, UPDATE_TEMP_BUFFER_BYTES - bufferBytes, providedData, providedDataBytes);
	bufferBytes += providedDataBytes;

	internalHmacDrbgData->health = Hmac_Drbg_GenerateHashFromMessage(internalHmacDrbgData->shaType, 
		internalHmacDrbgData->Key, internalHmacDrbgData->outlenBytes, buffer, bufferBytes,
		internalHmacDrbgData->tmpK.buffer, internalHmacDrbgData->tmpK.bytes);

	memsetAssert(buffer, sizeof(buffer), 0x00, UPDATE_TEMP_BUFFER_BYTES);
	bufferBytes = 0;

	if (internalHmacDrbgData->health != CRYPTO_SUCCESS)
	{
		return internalHmacDrbgData->health;
	}

	internalHmacDrbgData->health = Hmac_Drbg_GenerateHashFromMessage(internalHmacDrbgData->shaType, 
		internalHmacDrbgData->tmpK.buffer, internalHmacDrbgData->tmpK.bytes, 
		internalHmacDrbgData->V, internalHmacDrbgData->outlenBytes,
		internalHmacDrbgData->tmpV.buffer, internalHmacDrbgData->tmpV.bytes);


	if (internalHmacDrbgData->health != CRYPTO_SUCCESS)
	{
		return internalHmacDrbgData->health;
	}

	if (providedDataBytes != 0)
	{
		memcpyAssert(buffer, sizeof(buffer), internalHmacDrbgData->tmpV.buffer, internalHmacDrbgData->tmpV.bytes);
		bufferBytes += internalHmacDrbgData->tmpV.bytes;

		memcpyAssert(buffer + bufferBytes, sizeof(buffer) - bufferBytes, &one, 1);
		bufferBytes += 1;

		memcpyAssert(buffer + bufferBytes, sizeof(buffer) - bufferBytes, providedData, providedDataBytes);
		bufferBytes += providedDataBytes;

		internalHmacDrbgData->health = Hmac_Drbg_GenerateHashFromMessage(internalHmacDrbgData->shaType, 
			internalHmacDrbgData->tmpK.buffer, internalHmacDrbgData->tmpK.bytes,
			buffer, bufferBytes,
			internalHmacDrbgData->tmpK.buffer, internalHmacDrbgData->tmpK.bytes);

		if (internalHmacDrbgData->health != CRYPTO_SUCCESS)
		{
			return internalHmacDrbgData->health;
		}

		internalHmacDrbgData->health = Hmac_Drbg_GenerateHashFromMessage(internalHmacDrbgData->shaType, 
			internalHmacDrbgData->tmpK.buffer, internalHmacDrbgData->tmpK.bytes,
			internalHmacDrbgData->tmpV.buffer, internalHmacDrbgData->tmpV.bytes,
			internalHmacDrbgData->tmpV.buffer, internalHmacDrbgData->tmpV.bytes);

		if (internalHmacDrbgData->health != CRYPTO_SUCCESS)
		{
			return internalHmacDrbgData->health;
		}
	}

	memcpyAssert(internalHmacDrbgData->Key, sizeof(internalHmacDrbgData->Key), internalHmacDrbgData->tmpK.buffer, internalHmacDrbgData->tmpK.bytes);
	memcpyAssert(internalHmacDrbgData->V, sizeof(internalHmacDrbgData->V), internalHmacDrbgData->tmpV.buffer, internalHmacDrbgData->tmpV.bytes);

	memsetAssert(&internalHmacDrbgData->tmpK, sizeof(internalHmacDrbgData->tmpK), 0x00, sizeof(internalHmacDrbgData->tmpK));
	memsetAssert(&internalHmacDrbgData->tmpV, sizeof(internalHmacDrbgData->tmpV), 0x00, sizeof(internalHmacDrbgData->tmpV));

	return internalHmacDrbgData->health;
}

static crypto_status_e Hmac_Drbg_GenerateHashFromMessage(sha_type_e shaType, uint8_t* key, uint32_t keyBytes,
	uint8_t* message, uint32_t messageBytes,
	uint8_t* digest, uint32_t digestBytes)
{
	memsetAssert(&internalHmacDrbgData->tmpHashData, sizeof(internalHmacDrbgData->tmpHashData), 0x00, sizeof(internalHmacDrbgData->tmpHashData));
	internalHmacDrbgData->health = Hmac_Sha_Generate(shaType, message, messageBytes, key, keyBytes, &internalHmacDrbgData->tmpHashData);
	if (internalHmacDrbgData->health != CRYPTO_SUCCESS)
	{
		return internalHmacDrbgData->health;
	}
	if (digestBytes < internalHmacDrbgData->tmpHashData.bytes)
	{
		return CRYPTO_BUFFER_OVERFLOW_ERROR;
	}
	else
	{
		memcpyAssert(digest, digestBytes, internalHmacDrbgData->tmpHashData.buffer, internalHmacDrbgData->tmpHashData.bytes);
	}

	return internalHmacDrbgData->health;
}