#include "sha.h"
#include <openssl/sha.h>
#include <openssl/err.h>
#include <memory>

static std::unique_ptr<sha_runtime_data_t> shaRuntimeData;
static std::unique_ptr<sha_data_t> shaData;

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

void Sha_Cleanup()
{
	if (shaRuntimeData != nullptr)
	{
		if (shaRuntimeData->ctx != NULL)
		{
			EVP_MD_CTX_free(shaRuntimeData->ctx);
		}
		shaRuntimeData.reset(nullptr);
	}
	if (shaData != nullptr)
	{
		shaData.reset(nullptr);
	}
}

crypto_status_e Sha_Initialize(sha_type_e shaType)
{
	shaRuntimeData = std::make_unique<sha_runtime_data_t>();
	shaData = std::make_unique<sha_data_t>();
	if (shaRuntimeData == nullptr || shaData == nullptr)
	{
		Sha_Cleanup();
		return CRYPTO_NULL_PTR_ERROR;
	}
	memsetSafe(shaRuntimeData.get(), sizeof(sha_runtime_data_t), 0, sizeof(sha_runtime_data_t));
	memsetSafe(shaData.get(), sizeof(sha_data_t), 0, sizeof(sha_data_t));

	shaData->shaType = shaType;

	shaRuntimeData->ctx = EVP_MD_CTX_new();
	if (shaRuntimeData->ctx == NULL)
	{
		Sha_Cleanup();
		return CRYPTO_NULL_PTR_ERROR;
	}

	shaRuntimeData->evp_md = Sha_GetEvpMd(shaData->shaType);
	if (shaRuntimeData->evp_md == NULL)
	{
		Sha_Cleanup();
		return CRYPTO_NULL_PTR_ERROR;
	}

	if (!EVP_DigestInit_ex(shaRuntimeData->ctx, shaRuntimeData->evp_md, NULL))
	{
		Sha_Cleanup();
		return CRYPTO_ALG_ERROR;
	}

	return CRYPTO_SUCCESS;
}

crypto_status_e Sha_Update(uint8_t* msg, uint32_t msgBytes)
{
	if (shaRuntimeData == nullptr || shaData == nullptr)
	{
		Sha_Cleanup();
		return CRYPTO_NOT_INITIALIZE_ERROR;
	}
	memcpySafe(shaData->msg, msg, msgBytes);

	if (!EVP_DigestUpdate(shaRuntimeData->ctx, shaData->msg.buffer, shaData->msg.bytes))
	{
		Sha_Cleanup();
		return CRYPTO_ALG_ERROR;
	}

	return CRYPTO_SUCCESS;
}

crypto_status_e Sha_Finalize(uint8_t* digset, uint32_t digsetBytes, uint32_t* resultBytes)
{
	if (shaRuntimeData == nullptr || shaData == nullptr)
	{
		Sha_Cleanup();
		return CRYPTO_NOT_INITIALIZE_ERROR;
	}

	unsigned int tmpDigsetBytes = 0;
	if (!EVP_DigestFinal_ex(shaRuntimeData->ctx, shaData->digset.buffer, &tmpDigsetBytes))
	{
		Sha_Cleanup();
		return CRYPTO_ALG_ERROR;
	}

	if (tmpDigsetBytes != Sha_GetDigsetBytes(shaData->shaType))
	{
		Sha_Cleanup();
		return CRYPTO_DIGSET_MISMATCH_ERROR;
	}

	shaData->digset.bytes = tmpDigsetBytes;

	if (digsetBytes < shaData->digset.bytes)
	{
		Sha_Cleanup();
		return CRYPTO_BUFFER_OVERFLOW_ERROR;
	}

	memcpySafe(digset, digsetBytes, shaData->digset);
	*resultBytes = shaData->digset.bytes;

	Sha_Cleanup();
	return CRYPTO_SUCCESS;
}

/*
crypto_data_t Sha_GenerateNormal(sha_type_e shaType, uint8_t* msg, uint32_t msgBytes)
{
	crypto_data_t result;

	EVP_MD_CTX* ctx = EVP_MD_CTX_new();

	if (ctx == NULL)
	{
		result.status = CRYPTO_NULL_PTR_ERROR;
		return result;
	}

	const EVP_MD* evp_md = Sha_GetEvpMd(shaType);
	do
	{
		if (evp_md == NULL)
		{
			result.status = CRYPTO_ALG_ERROR;
			break;
		}

		if (!EVP_DigestInit_ex(ctx, evp_md, NULL))
		{
			result.status = CRYPTO_ALG_ERROR;
			break;
		}


		if (!EVP_DigestUpdate(ctx, msg, msgBytes))
		{
			result.status = CRYPTO_ALG_ERROR;
			break;
		}

		unsigned int digsetBytes = 0;
		if (!EVP_DigestFinal_ex(ctx, result.data.buffer, &digsetBytes))
		{
			result.status = CRYPTO_ALG_ERROR;
			break;
		}

		if (digsetBytes != Sha_GetDigsetBytes(shaType))
		{
			result.status = CRYPTO_DIGSET_MISMATCH_ERROR;
			break;
		}
		result.data.bytes = digsetBytes;
	} while (0);

	if (ctx != NULL)
	{
		EVP_MD_CTX_free(ctx);
	}

	return result;
}
*/