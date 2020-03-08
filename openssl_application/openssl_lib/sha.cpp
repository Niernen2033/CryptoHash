#include "sha.h"
#include <openssl/sha.h>
#include <openssl/err.h>

const EVP_MD* Sha_GetEvpMd(SHA_TYPE shaType)
{
	switch (shaType)
	{
	case SHA_TYPE::SHA_224:
		return EVP_sha224();
	case SHA_TYPE::SHA_256:
		return EVP_sha256();
	case SHA_TYPE::SHA_384:
		return EVP_sha384();
	case SHA_TYPE::SHA_512:
		return EVP_sha512();
	default:
		return NULL;
	}
}

uint32_t Sha_GetDigsetBytes(SHA_TYPE shaType)
{
	switch (shaType)
	{
	case SHA_TYPE::SHA_224:
		return SHA_224_DIGEST_LEN_BYTES;
	case SHA_TYPE::SHA_256:
		return SHA_256_DIGEST_LEN_BYTES;
	case SHA_TYPE::SHA_384:
		return SHA_384_DIGEST_LEN_BYTES;
	case SHA_TYPE::SHA_512:
		return SHA_512_DIGEST_LEN_BYTES;
	default:
		return 0;
	}
}

uint32_t Sha_GetDigsetBits(SHA_TYPE shaType)
{
	switch (shaType)
	{
	case SHA_TYPE::SHA_224:
		return SHA_224_DIGEST_LEN_BITS;
	case SHA_TYPE::SHA_256:
		return SHA_256_DIGEST_LEN_BITS;
	case SHA_TYPE::SHA_384:
		return SHA_384_DIGEST_LEN_BITS;
	case SHA_TYPE::SHA_512:
		return SHA_512_DIGEST_LEN_BITS;
	default:
		return 0;
	}
}

crypto_data_t Sha_GenerateNormal(SHA_TYPE shaType, uint8_t msg[], uint32_t msgBytes)
{
	crypto_data_t result = { 0 };

	EVP_MD_CTX* ctx = EVP_MD_CTX_new();

	if (ctx == NULL)
	{
		result.status = CRYPTO_STATUS::CRYPTO_INIT_ERROR;
		return result;
	}

	const EVP_MD* evp_md = Sha_GetEvpMd(shaType);
	do
	{
		if (evp_md == NULL)
		{
			result.status = CRYPTO_STATUS::CRYPTO_INIT_ERROR;
			break;
		}

		if (!EVP_DigestInit_ex(ctx, evp_md, NULL))
		{
			result.status = CRYPTO_STATUS::CRYPTO_INIT_ERROR;
			break;
		}


		if (!EVP_DigestUpdate(ctx, msg, msgBytes))
		{
			result.status = CRYPTO_STATUS::CRYPTO_UPDATE_ERROR;
			break;
		}

		if (!EVP_DigestFinal_ex(ctx, result.data.buffer, &result.data.bytes))
		{
			result.status = CRYPTO_STATUS::CRYPTO_FINAL_ERROR;
			break;
		}

		if (result.data.bytes != Sha_GetDigsetBytes(shaType))
		{
			result.status = CRYPTO_STATUS::CRYPTO_BUFFER_SIZE_MISMATCH_ERROR;
			break;
		}
	} while (0);

	if (ctx != NULL)
	{
		EVP_MD_CTX_free(ctx);
	}

	return result;
}