#include <hkdf.h>
#include <hmac_sha.h>
#include <crypto_utils.h>
#include <crypto_result.h>

crypto_status_e Hkdf_Generate(sha_type_e shaType, uint8_t key[], uint32_t keyBytes, uint8_t fixedData[], uint32_t fixedDataBytes, uint32_t outputKeyBytes, crypto_buffer_t* digset)
{
	crypto_buffer_t result = { 0 };
	crypto_status_e status = CRYPTO_SUCCESS;


	uint32_t shaDigsetBits = Sha_GetDigsetBits(shaType);
	uint32_t shaDigsetBytes = Sha_GetDigsetBytes(shaType);
	uint32_t n = ((BYTES_TO_BITS(outputKeyBytes) + (shaDigsetBits - 1)) / shaDigsetBits);
	uint32_t acctualTempMsgBytes = fixedDataBytes + HKDF_ITERATOR_DIGSET_BYTES;

	if (acctualTempMsgBytes > CRYPTO_BUFFER_DEFAULT_BYTES)
	{
		CryRes_SetLastResult(&result, CRYPTO_BUFFER_OVERFLOW_ERROR);
		return CRYPTO_BUFFER_OVERFLOW_ERROR;
	}

	uint8_t tmpMsg[CRYPTO_BUFFER_DEFAULT_BYTES];
	uint8_t tmpHash[CRYPTO_BUFFER_DEFAULT_BYTES];
	uint32_t tempAcctualHashSize = 0;

	for (uint32_t i = 1; i <= n; i++)
	{
		uint32_t iterator = REVERSE_ENDIAN_32_BITS(i);
		memcpyAssert(tmpMsg, CRYPTO_BUFFER_DEFAULT_BYTES , &iterator, HKDF_ITERATOR_DIGSET_BYTES);
		memcpyAssert(tmpMsg + HKDF_ITERATOR_DIGSET_BYTES, CRYPTO_BUFFER_DEFAULT_BYTES, fixedData, fixedDataBytes);

		crypto_buffer_t loopTempHash = { 0 };
		crypto_status_e loopTempHashStatus = Hmac_Sha_Generate(shaType, tmpMsg, acctualTempMsgBytes, key, keyBytes, &loopTempHash);
		if (loopTempHashStatus != CRYPTO_SUCCESS)
		{
			status = loopTempHashStatus;
			break;
		}
		if (loopTempHash.bytes != shaDigsetBytes)
		{
			status = CRYPTO_BUFFER_OVERFLOW_ERROR;
			break;
		}
		if ((tempAcctualHashSize + loopTempHash.bytes) > CRYPTO_BUFFER_DEFAULT_BYTES)
		{
			status = CRYPTO_BUFFER_OVERFLOW_ERROR;
			break;
		}
		memcpyAssert(tmpHash + tempAcctualHashSize, CRYPTO_BUFFER_DEFAULT_BYTES, loopTempHash.buffer, loopTempHash.bytes);
		tempAcctualHashSize += loopTempHash.bytes;
	}

	if (status == CRYPTO_SUCCESS)
	{
		if (tempAcctualHashSize > CRYPTO_BUFFER_DEFAULT_BYTES)
		{
			CryRes_SetLastResult(&result, CRYPTO_BUFFER_OVERFLOW_ERROR);
			return CRYPTO_BUFFER_OVERFLOW_ERROR;
		}
		memcpyAssert(result.buffer, CRYPTO_BUFFER_DEFAULT_BYTES, tmpHash, outputKeyBytes);
		result.bytes = outputKeyBytes;
	}

	if (digset != nullptr)
	{
		*digset = result;
	}

	CryRes_SetLastResult(&result, status);

	return status;
}

crypto_status_e Hkdf_Generate(sha_type_e shaType, uint8_t key[], uint32_t keyBytes, uint8_t fixedData[], uint32_t fixedDataBytes, uint32_t outputKeyBytes)
{
	return Hkdf_Generate(shaType, key, keyBytes, fixedData, fixedDataBytes, outputKeyBytes, nullptr);
}