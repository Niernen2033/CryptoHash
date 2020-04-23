#include <rsa.h>
#include <crypto_result.h>
#include <memory>
#include <openssl//rsa.h>
#include <openssl/bn.h>
#include <openssl/err.h>
#include <NLog.h>

static int getAlgorithmType(sha_type_e shaType);


crypto_status_e Rsa_Sign(sha_type_e shaType, uint8_t msg[], uint32_t msgBytes, uint8_t e[], uint32_t eBytes,
	uint8_t n[], uint32_t nBytes, uint8_t d[], uint32_t dBytes, crypto_buffer_t* signature)
{
	crypto_buffer_t result = { 0 };
	crypto_status_e status = CRYPTO_SUCCESS;

	crypto_buffer_t shaData = { 0 };
	crypto_status_e shaDataStatus = Sha_Generate(shaType, msg, msgBytes, &shaData);
	if (shaDataStatus != CRYPTO_SUCCESS)
	{
		NLog_Error("shaDataStatus != CRYPTO_SUCCESS");
		return CRYPTO_ALG_ERROR;
	}

	RSA* rsa = RSA_new();
	BIGNUM* bnN = BN_new();
	BIGNUM* bnE = BN_new();
	BIGNUM* bnD = BN_new();

	do
	{
		if (rsa == NULL || bnN == NULL || bnE == NULL || bnD == NULL)
		{
			NLog_Error("rsa == NULL || bnN == NULL || bnE == NULL || bnD == NULL");
			status = CRYPTO_NULL_PTR_ERROR;
			break;
		}

		if (!BN_bin2bn(e, eBytes, bnE))
		{
			NLog_Error(ERR_error_string(ERR_get_error(), NULL));
			status = CRYPTO_ALG_ERROR;
			break;
		}

		if (!BN_bin2bn(n, nBytes, bnN))
		{
			NLog_Error(ERR_error_string(ERR_get_error(), NULL));
			status = CRYPTO_ALG_ERROR;
			break;
		}

		if (!BN_bin2bn(d, dBytes, bnD))
		{
			NLog_Error(ERR_error_string(ERR_get_error(), NULL));
			status = CRYPTO_ALG_ERROR;
			break;
		}

		if (!RSA_set0_key(rsa, bnN, bnE, bnD))
		{
			NLog_Error(ERR_error_string(ERR_get_error(), NULL));
			status = CRYPTO_ALG_ERROR;
			break;
		}

		unsigned int sBytes = 0;
		int rsaResult = RSA_sign(getAlgorithmType(shaType), shaData.buffer, shaData.bytes, result.buffer, &sBytes, rsa);
		if (rsaResult == 0)
		{
			status = CRYPTO_ERROR;
		}
		else
		{
			result.bytes = sBytes;
		}
	} while (0);

	if (rsa != NULL)
	{
		RSA_free(rsa);
	}

	if (bnN != NULL)
	{
		//BN_clear_free(bnN);
	}
	if (bnE != NULL)
	{
		//BN_clear_free(bnE);
	}

	if (bnD != NULL)
	{
		//BN_clear_free(bnD);
	}

	if (signature != nullptr)
	{
		*signature = result;
	}

	CryRes_SetLastResult(&result, status);

	return status;
}

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

	RSA* rsa = RSA_new();
	BIGNUM* bnN = BN_new();
	BIGNUM* bnE = BN_new();

	do
	{
		if (rsa == NULL || bnN == NULL || bnE == NULL)
		{
			NLog_Error("rsa == NULL || bnN == NULL || bnE == NULL");
			status = CRYPTO_NULL_PTR_ERROR;
			break;
		}

		if (!BN_bin2bn(e, eBytes, bnE))
		{
			NLog_Error(ERR_error_string(ERR_get_error(), NULL));
			status = CRYPTO_ALG_ERROR;
			break;
		}

		if (!BN_bin2bn(n, nBytes, bnN))
		{
			NLog_Error(ERR_error_string(ERR_get_error(), NULL));
			status = CRYPTO_ALG_ERROR;
			break;
		}

		if (!RSA_set0_key(rsa, bnN, bnE, NULL))
		{
			NLog_Error(ERR_error_string(ERR_get_error(), NULL));
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