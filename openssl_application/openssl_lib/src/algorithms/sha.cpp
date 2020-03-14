#include <sha.h>
#include <openssl/sha.h>
#include <openssl/err.h>
#include <memory>
#include <crypto_result.h>
#include <NLog.h>

const EVP_MD* Sha_GetEvpMd(sha_type_e shaType)
{
	switch (shaType)
	{
	case SHA_224:
		return EVP_sha224();
	case SHA_256:
		return EVP_sha256();
	case SHA_384:
		return EVP_sha384();
	case SHA_512:
		return EVP_sha512();
	default:
		return NULL;
	}
}

uint32_t Sha_GetDigsetBytes(sha_type_e shaType)
{
	switch (shaType)
	{
	case SHA_224:
		return SHA_224_DIGEST_LEN_BYTES;
	case SHA_256:
		return SHA_256_DIGEST_LEN_BYTES;
	case SHA_384:
		return SHA_384_DIGEST_LEN_BYTES;
	case SHA_512:
		return SHA_512_DIGEST_LEN_BYTES;
	default:
		return 0;
	}
}

uint32_t Sha_GetDigsetBits(sha_type_e shaType)
{
	switch (shaType)
	{
	case SHA_224:
		return SHA_224_DIGEST_LEN_BITS;
	case SHA_256:
		return SHA_256_DIGEST_LEN_BITS;
	case SHA_384:
		return SHA_384_DIGEST_LEN_BITS;
	case SHA_512:
		return SHA_512_DIGEST_LEN_BITS;
	default:
		return 0;
	}
}

crypto_status_e Sha_Generate(sha_type_e shaType, uint8_t* msg, uint32_t msgBytes, crypto_buffer_t* digset)
{
	crypto_buffer_t result = { 0 };
	crypto_status_e status = CRYPTO_SUCCESS;

	EVP_MD_CTX* ctx = EVP_MD_CTX_new();

	if (ctx == NULL)
	{
		CryRes_SetLastResult(&result, CRYPTO_NULL_PTR_ERROR);
		return CRYPTO_NULL_PTR_ERROR;
	}

	const EVP_MD* evp_md = Sha_GetEvpMd(shaType);
	do
	{
		if (evp_md == NULL)
		{
			status = CRYPTO_ALG_ERROR;
			break;
		}

		if (!EVP_DigestInit_ex(ctx, evp_md, NULL))
		{
			status = CRYPTO_ALG_ERROR;
			break;
		}


		if (!EVP_DigestUpdate(ctx, msg, msgBytes))
		{
			status = CRYPTO_ALG_ERROR;
			break;
		}

		unsigned int digsetBytes = 0;
		if (!EVP_DigestFinal_ex(ctx, result.buffer, &digsetBytes))
		{
			status = CRYPTO_ALG_ERROR;
			break;
		}

		if (digsetBytes != Sha_GetDigsetBytes(shaType))
		{
			status = CRYPTO_BUFFER_MISMATCH_ERROR;
			break;
		}
		result.bytes = digsetBytes;
	} while (0);

	if (ctx != NULL)
	{
		EVP_MD_CTX_free(ctx);
	}

	if (digset != nullptr)
	{
		*digset = result;
	}

	CryRes_SetLastResult(&result, status);

	return status;
}

crypto_status_e Sha_Generate(sha_type_e shaType, uint8_t* msg, uint32_t msgBytes)
{
	return Sha_Generate(shaType, msg, msgBytes, nullptr);
}