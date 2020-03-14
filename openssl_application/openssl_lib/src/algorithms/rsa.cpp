#include <rsa.h>
#include <crypto_result.h>
#include <memory>
#include <openssl//rsa.h>
#include <openssl/bn.h>
#include <openssl/err.h>

static int getAlgorithmType(sha_type_e shaType);

crypto_status_e Rsa_Verify(sha_type_e shaType, uint8_t msg[], uint32_t msgBytes, uint8_t e[], uint32_t eBytes,
	uint8_t n[], uint32_t nBytes, uint8_t s[], uint32_t sBytes)
{
	crypto_status_e status = CRYPTO_SUCCESS;

	crypto_buffer_t shaData = { 0 };
	crypto_status_e shaDataStatus = Sha_Generate(shaType, msg, msgBytes, &shaData);
	if (shaDataStatus != CRYPTO_SUCCESS)
	{
		return CRYPTO_ALG_ERROR;
	}

	RSA *rsa = RSA_new();
	BIGNUM *bnN = BN_new();
	BIGNUM *bnE = BN_new();

	do
	{
		if (rsa == NULL || bnN == NULL || bnE == NULL)
		{
			status = CRYPTO_NULL_PTR_ERROR;
			break;
		}

		if (!BN_bin2bn(e, eBytes, bnE))
		{
			status = CRYPTO_ALG_ERROR;
			break;
		}

		if (!BN_bin2bn(n, nBytes, bnN))
		{
			status = CRYPTO_ALG_ERROR;
			break;
		}

		if (!RSA_set0_key(rsa, bnN, bnE, NULL))
		{
			status = CRYPTO_ALG_ERROR;
			break;
		}

		int rsaResult = RSA_verify(getAlgorithmType(shaType), shaData.buffer, shaData.bytes, s, sBytes, rsa);
		if (rsaResult == 0)
		{
			status = CRYPTO_ERROR;
		}
	} while (0);

	if (rsa != NULL)
	{
		RSA_free(rsa);
	}
	if (bnN != NULL)
	{
		//BN_free(bnN);
	}
	if (bnE != NULL)
	{
		//BN_free(bnE);
	}

	return status;
}

static int getAlgorithmType(sha_type_e shaType)
{
    int result = NID_sha256;

    switch (shaType)
    {
    case SHA_224:
        result = NID_sha224;
        break;
    case SHA_256:
        result = NID_sha256;
        break;
    case SHA_384:
        result = NID_sha384;
        break;
    case SHA_512:
        result = NID_sha512;
        break;
    }

    return result;
}