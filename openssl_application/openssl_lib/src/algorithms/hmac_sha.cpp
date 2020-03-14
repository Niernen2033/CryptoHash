#include <hmac_sha.h>
#include <crypto_result.h>
#include <openssl/hmac.h>
#include <openssl/err.h>

crypto_status_e Hmac_Sha_Generate(sha_type_e shaType, uint8_t msg[], uint32_t msgBytes, uint8_t key[], uint32_t keyBytes, crypto_buffer_t* digset)
{
	crypto_buffer_t result = { 0 };
	crypto_status_e status = CRYPTO_SUCCESS;

	HMAC_CTX *ctx = HMAC_CTX_new();
	if (ctx == NULL)
	{
		CryRes_SetLastResult(&result, CRYPTO_NULL_PTR_ERROR);
		return CRYPTO_NULL_PTR_ERROR;
	}

	const EVP_MD* evp = Sha_GetEvpMd(shaType);
	do
	{
		if (evp == NULL)
		{
			status = CRYPTO_NULL_PTR_ERROR;
			break;
		}

		if (!HMAC_Init_ex(ctx, key, keyBytes, evp, NULL))
		{
			status = CRYPTO_ALG_ERROR;
			break;
		}

		if (!HMAC_Update(ctx, msg, msgBytes))
		{
			status = CRYPTO_ALG_ERROR;
			break;
		}

		if (!HMAC_Final(ctx, result.buffer, &result.bytes))
		{
			status = CRYPTO_ALG_ERROR;
			break;
		}
	} while (0);

	if (ctx != NULL)
	{
		HMAC_CTX_free(ctx);
	}

	if (digset != nullptr)
	{
		*digset = result;
	}

	CryRes_SetLastResult(&result, status);

	return status;
}

crypto_status_e Hmac_Sha_Generate(sha_type_e shaType, uint8_t msg[], uint32_t msgBytes, uint8_t key[], uint32_t keyBytes)
{
	return Hmac_Sha_Generate(shaType, msg, msgBytes, key, keyBytes, nullptr);
}
