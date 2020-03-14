#include <aes.h>
#include <crypto_result.h>
#include <openssl/err.h>
#include <crypto_utils.h>

static const uint8_t AES_Key_Wrap_Default_IV[AES_KEY_WRAP_IV_BYTES] = { 0xA6, 0xA6, 0xA6, 0xA6, 0xA6, 0xA6, 0xA6, 0xA6 };

static crypto_status_e Aes_Normal_Generate(aes_type_e aesType, bool encrypt, uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes, crypto_buffer_t* digset);
static crypto_status_e Aes_Kw_Ku_Generate(bool wrap, aes_type_e aesType, uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes, crypto_buffer_t* digset);

const EVP_CIPHER* Aes_GetEvpCipher(aes_type_e aesType)
{
	switch (aesType)
	{
		//AES
	case AES_ECB_128:
		return EVP_aes_128_ecb();
	case AES_ECB_192:
		return EVP_aes_192_ecb();
	case AES_ECB_256:
		return EVP_aes_256_ecb();

		//CBC
	case AES_CBC_128:
		return EVP_aes_128_cbc();
	case AES_CBC_192:
		return EVP_aes_192_cbc();
	case AES_CBC_256:
		return EVP_aes_256_cbc();

		//CTR
	case AES_CTR_128:
		return EVP_aes_128_ctr();
	case AES_CTR_192:
		return EVP_aes_192_ctr();
	case AES_CTR_256:
		return EVP_aes_256_ctr();

		//GCM
	case AES_GCM_128:
		return EVP_aes_128_gcm();
	case AES_GCM_192:
		return EVP_aes_192_gcm();
	case AES_GCM_256:
		return EVP_aes_256_gcm();

		//XTS
	case AES_XTS_128:
		return EVP_aes_128_xts();
	case AES_XTS_256:
		return EVP_aes_256_xts();
	default:
		return NULL;
	}
}

crypto_status_e Aes_Encrypt_Generate(aes_type_e aesType, uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes)
{
	return Aes_Encrypt_Generate(aesType, key, keyBytes, msg, msgBytes, iv, ivBytes, nullptr);
}

crypto_status_e Aes_Encrypt_Generate(aes_type_e aesType, uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes, crypto_buffer_t* digset)
{
	return Aes_Normal_Generate(aesType, true, key, keyBytes, msg, msgBytes, iv, ivBytes, digset);
}

crypto_status_e Aes_Decrypt_Generate(aes_type_e aesType, uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes)
{
	return Aes_Decrypt_Generate(aesType, key, keyBytes, msg, msgBytes, iv, ivBytes, nullptr);
}

crypto_status_e Aes_Decrypt_Generate(aes_type_e aesType, uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes, crypto_buffer_t* digset)
{
	return Aes_Normal_Generate(aesType, false, key, keyBytes, msg, msgBytes, iv, ivBytes, digset);
}

static crypto_status_e Aes_Normal_Generate(aes_type_e aesType, bool encrypt, uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes, crypto_buffer_t* digset)
{
	crypto_buffer_t result = { 0 };
	crypto_status_e status = CRYPTO_SUCCESS;

	EVP_CIPHER_CTX *ctx = EVP_CIPHER_CTX_new();
	if (ctx == NULL)
	{
		CryRes_SetLastResult(&result, CRYPTO_NULL_PTR_ERROR);
		return CRYPTO_NULL_PTR_ERROR;
	}

	do
	{
		const EVP_CIPHER* evp = Aes_GetEvpCipher(aesType);
		if (evp == NULL)
		{
			status = CRYPTO_NOT_SUPPORTED;
			break;
		}

		if (!EVP_CipherInit_ex(ctx, evp, NULL, NULL, NULL, encrypt))
		{
			status = CRYPTO_ALG_ERROR;
			break;
		}

		if (!EVP_CipherInit_ex(ctx, NULL, NULL, key, iv, encrypt))
		{
			status = CRYPTO_ALG_ERROR;
			break;
		}

		if (EVP_CIPHER_CTX_key_length(ctx) > EVP_MAX_KEY_LENGTH)
		{
			status = CRYPTO_ALG_ERROR;
			break;
		}

		if (EVP_CIPHER_CTX_iv_length(ctx) > EVP_MAX_IV_LENGTH)
		{
			status = CRYPTO_ALG_ERROR;
			break;
		}

		if (!EVP_CIPHER_CTX_set_padding(ctx, 0))
		{
			status = CRYPTO_ALG_ERROR;
			break;
		}

		int tmpHashBytes = 0;
		if (!EVP_CipherUpdate(ctx, result.buffer, &tmpHashBytes, msg, msgBytes))
		{
			status = CRYPTO_ALG_ERROR;
			break;
		}
		else
		{
			result.bytes = tmpHashBytes;
		}

		int tempFinalBytes = 0;
		if (!EVP_CipherFinal_ex(ctx, result.buffer, &tempFinalBytes))
		{
			status = CRYPTO_ALG_ERROR;
			break;
		}

		result.bytes += tempFinalBytes;

	} while (0);

	if (ctx != NULL)
	{
		EVP_CIPHER_CTX_free(ctx);
	}

	if (digset != nullptr)
	{
		*digset = result;
	}

	CryRes_SetLastResult(&result, status);

	return status;
}

crypto_status_e Aes_KeyWrap_Generate(uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes)
{
	return Aes_KeyWrap_Generate(key, keyBytes, msg, msgBytes, iv, ivBytes, nullptr);
}

crypto_status_e Aes_KeyWrap_Generate(uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes, crypto_buffer_t* digset)
{
	return Aes_Kw_Ku_Generate(true, AES_ECB_256, key, keyBytes, msg, msgBytes, iv, ivBytes, digset);
}

crypto_status_e Aes_KeyUnwrap_Generate(uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes)
{
	return Aes_KeyUnwrap_Generate(key, keyBytes, msg, msgBytes, iv, ivBytes, nullptr);
}

crypto_status_e Aes_KeyUnwrap_Generate(uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes, crypto_buffer_t* digset)
{
	return Aes_Kw_Ku_Generate(false, AES_ECB_256, key, keyBytes, msg, msgBytes, iv, ivBytes, digset);
}

static crypto_status_e Aes_Kw_Ku_Generate(bool wrap, aes_type_e aesType, uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes, crypto_buffer_t* digset)
{
	crypto_buffer_t result = { 0 };
	crypto_status_e status = CRYPTO_SUCCESS;

	if (aesType != AES_ECB_256)
	{
		CryRes_SetLastResult(&result, CRYPTO_NOT_SUPPORTED);
		return CRYPTO_NOT_SUPPORTED;
	}

	const uint8_t *initialization_vector = NULL;
	if (ivBytes != 0)
	{
		if (ivBytes != AES_KEY_WRAP_IV_BYTES)
		{
			CryRes_SetLastResult(&result, CRYPTO_ALG_ERROR);
			return CRYPTO_ALG_ERROR;
		}
		else
		{
			initialization_vector = iv;
		}
	}
	else
	{
		if (sizeof(AES_Key_Wrap_Default_IV) != AES_KEY_WRAP_IV_BYTES)
		{
			CryRes_SetLastResult(&result, CRYPTO_ALG_ERROR);
			return CRYPTO_ALG_ERROR;
		}
		else
		{
			initialization_vector = AES_Key_Wrap_Default_IV;
		}
	}

	unsigned int step_count1 = 0;
	unsigned int step_count2 = 0;
	int acctualHashBytes = 0;
	int blocks_64_to_process = 0;
	uint8_t* tmpEncryptBuffer1 = nullptr;
	uint8_t* tmpEncryptBuffer2 = nullptr;
	uint8_t encryptBuffer[AES_KEY_WRAP_ENCRYPTION_BUFFER_BYTES];
	int i, j, k;

	tmpEncryptBuffer1 = encryptBuffer;
	if (wrap == true)
	{
		if ((msgBytes + AES_KEY_WRAP_IV_BYTES) > CRYPTO_BUFFER_DEFAULT_BYTES)
		{
			CryRes_SetLastResult(&result, CRYPTO_BUFFER_OVERFLOW_ERROR);
			return CRYPTO_BUFFER_OVERFLOW_ERROR;
		}

		//determine the number of 64-bit blocks to process
		blocks_64_to_process = msgBytes >> 3;
		memcpyAssert(tmpEncryptBuffer1, AES_KEY_WRAP_ENCRYPTION_BUFFER_BYTES, (void *)initialization_vector, AES_KEY_WRAP_IV_BYTES);
		memcpyAssert(result.buffer + AES_KEY_WRAP_IV_BYTES, CRYPTO_BUFFER_DEFAULT_BYTES - AES_KEY_WRAP_IV_BYTES, msg, msgBytes);

		for (j = 0, step_count1 = 1; j <= 5; j++)
		{
			for (i = 1, tmpEncryptBuffer2 = result.buffer + AES_KEY_WRAP_IV_BYTES; i <= blocks_64_to_process; i++, step_count1++, tmpEncryptBuffer2 += AES_KEY_WRAP_IV_BYTES)
			{
				memcpyAssert(encryptBuffer + AES_KEY_WRAP_IV_BYTES, AES_KEY_WRAP_ENCRYPTION_BUFFER_BYTES - AES_KEY_WRAP_IV_BYTES, tmpEncryptBuffer2, AES_KEY_WRAP_IV_BYTES);
				crypto_buffer_t tempHash = { 0 };
				crypto_status_e tempHashStatus = Aes_Normal_Generate(aesType, wrap, key, keyBytes, encryptBuffer, AES_KEY_WRAP_ENCRYPTION_BUFFER_BYTES, NULL, 0, &tempHash);

				if (tempHashStatus != CRYPTO_SUCCESS)
				{
					status = tempHashStatus;
					break;
				}
				else
				{
					if (tempHash.bytes > AES_KEY_WRAP_ENCRYPTION_BUFFER_BYTES)
					{
						status = CRYPTO_BUFFER_OVERFLOW_ERROR;
						break;
					}
					memcpyAssert(encryptBuffer, AES_KEY_WRAP_ENCRYPTION_BUFFER_BYTES, tempHash.buffer, tempHash.bytes);
				}

				for (k = 7, step_count2 = step_count1; (k >= 0) && (step_count2 > 0); k--, step_count2 >>= 8)
				{
					tmpEncryptBuffer1[k] ^= (uint8_t)(step_count2 & 0xFF);
				}
				memcpyAssert(tmpEncryptBuffer2, CRYPTO_BUFFER_DEFAULT_BYTES - AES_KEY_WRAP_IV_BYTES, encryptBuffer + AES_KEY_WRAP_IV_BYTES, AES_KEY_WRAP_IV_BYTES);
			}
			if (status != CRYPTO_SUCCESS)
			{
				break;
			}
		}
		if (status == CRYPTO_SUCCESS)
		{
			memcpyAssert(result.buffer, CRYPTO_BUFFER_DEFAULT_BYTES, tmpEncryptBuffer1, AES_KEY_WRAP_IV_BYTES);
			result.bytes = msgBytes + AES_KEY_WRAP_IV_BYTES;
		}
	}
	else
	{
		if ((msgBytes - AES_KEY_WRAP_IV_BYTES) > CRYPTO_BUFFER_DEFAULT_BYTES)
		{
			CryRes_SetLastResult(&result, CRYPTO_BUFFER_OVERFLOW_ERROR);
			return CRYPTO_BUFFER_OVERFLOW_ERROR;
		}

		blocks_64_to_process = (msgBytes - AES_KEY_WRAP_IV_BYTES) >> 3;
		memcpyAssert(tmpEncryptBuffer1, AES_KEY_WRAP_ENCRYPTION_BUFFER_BYTES, msg, AES_KEY_WRAP_IV_BYTES);
		memcpyAssert(result.buffer, CRYPTO_BUFFER_DEFAULT_BYTES, msg + AES_KEY_WRAP_IV_BYTES, msgBytes - AES_KEY_WRAP_IV_BYTES);

		for (j = 5, step_count1 = 6 * blocks_64_to_process; j >= 0; j--)
		{
			for (i = blocks_64_to_process, tmpEncryptBuffer2 = (result.buffer + msgBytes - AES_KEY_WRAP_ENCRYPTION_BUFFER_BYTES); i >= 1; i--, step_count1--, tmpEncryptBuffer2 -= AES_KEY_WRAP_IV_BYTES)
			{
				for (k = AES_KEY_WRAP_IV_BYTES - 1, step_count2 = step_count1; (k >= 0) && (step_count2 > 0); k--, step_count2 >>= AES_KEY_WRAP_IV_BYTES)
				{
					tmpEncryptBuffer1[k] ^= (uint8_t)(step_count2 & 0xFF);
				}
				memcpyAssert(encryptBuffer + AES_KEY_WRAP_IV_BYTES, AES_KEY_WRAP_ENCRYPTION_BUFFER_BYTES - AES_KEY_WRAP_IV_BYTES, tmpEncryptBuffer2, AES_KEY_WRAP_IV_BYTES);

				crypto_buffer_t tempHash = { 0 };
				crypto_status_e tempHashStatus = Aes_Normal_Generate(aesType, wrap, key, keyBytes, encryptBuffer, AES_KEY_WRAP_ENCRYPTION_BUFFER_BYTES, NULL, 0, &tempHash);

				if (tempHashStatus != CRYPTO_SUCCESS)
				{
					status = tempHashStatus;
					break;
				}
				else
				{
					if (tempHash.bytes > AES_KEY_WRAP_ENCRYPTION_BUFFER_BYTES)
					{
						status = CRYPTO_BUFFER_OVERFLOW_ERROR;
						break;
					}
					memcpyAssert(encryptBuffer, AES_KEY_WRAP_ENCRYPTION_BUFFER_BYTES, tempHash.buffer, tempHash.bytes);
				}
				memcpyAssert(tmpEncryptBuffer2, CRYPTO_BUFFER_DEFAULT_BYTES - msgBytes + AES_KEY_WRAP_ENCRYPTION_BUFFER_BYTES,encryptBuffer + AES_KEY_WRAP_IV_BYTES, AES_KEY_WRAP_IV_BYTES);
			}
			if (status != CRYPTO_SUCCESS)
			{
				break;
			}
		}

		if (status == CRYPTO_SUCCESS)
		{
			result.bytes = msgBytes - AES_KEY_WRAP_IV_BYTES;
			if (memcpyAssert((void*)initialization_vector, AES_KEY_WRAP_IV_BYTES, tmpEncryptBuffer1, AES_KEY_WRAP_IV_BYTES != 0))
			{
				CryRes_SetLastResult(&result, CRYPTO_INTEGRITY_ERROR);
				return CRYPTO_INTEGRITY_ERROR;
			}
		}
	}

	if (digset != nullptr)
	{
		*digset = result;
	}

	CryRes_SetLastResult(&result, status);

	return status;
}