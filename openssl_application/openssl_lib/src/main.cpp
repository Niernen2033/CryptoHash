#include <version.h>
#include <memory>
#include <Windows.h>
#include <NLog.h>
#include <crypto_result.h>
#include <sha.h>
#include <hmac_sha.h>
#include <hmac_drbg.h>
#include <aes.h>
#include <hkdf.h>
#include <rsa.h>

DLL_MAIN_C DllMain(HANDLE hModule, DWORD fdwreason, LPVOID lpReserved)
{
	switch (fdwreason)
	{
	case DLL_PROCESS_ATTACH:
		// The DLL is being mapped into process's address space
		// Do any required initialization on a per application basis, return FALSE if failed
		if (!NLog_Init())
		{
			return FALSE;
		}
		else
		{
			NLog_Debug("NLog_Init PASSED");
		}
		if (!CryRes_Init())
		{
			NLog_Debug("CryRes_Init PASSED");
			return FALSE;
		}
		else
		{
			NLog_Debug("CryRes_Init FAILED");
		}
		if (!Hmac_Drbg_Init())
		{
			NLog_Debug("Hmac_Drbg_Init PASSED");
			return FALSE;
		}
		else
		{
			NLog_Debug("Hmac_Drbg_Init FAILED");
		}
		break;
	case DLL_THREAD_ATTACH:
		// A thread is created. Do any required initialization on a per thread basis
		if (!NLog_Init())
		{
			return FALSE;
		}
		else
		{
			NLog_Debug("NLog_Init PASSED");
		}
		if (!CryRes_Init())
		{
			NLog_Debug("CryRes_Init PASSED");
			return FALSE;
		}
		else
		{
			NLog_Debug("CryRes_Init FAILED");
		}
		if (!Hmac_Drbg_Init())
		{
			NLog_Debug("Hmac_Drbg_Init PASSED");
			return FALSE;
		}
		else
		{
			NLog_Debug("Hmac_Drbg_Init FAILED");
		}
		break;
	case DLL_THREAD_DETACH:
		// Thread exits with cleanup
		if (!NLog_Cleanup())
		{
			return FALSE;
		}
		if (!CryRes_Cleanup())
		{
			return FALSE;
		}
		if (!Hmac_Drbg_Cleanup())
		{
			return FALSE;
		}
		break;
	case DLL_PROCESS_DETACH:
		// The DLL unmapped from process's address space. Do necessary cleanup
		if (!NLog_Cleanup())
		{
			return FALSE;
		}
		if (!CryRes_Cleanup())
		{
			return FALSE;
		}
		if (!Hmac_Drbg_Cleanup())
		{
			return FALSE;
		}
		break;
	}
	return TRUE;
}

EXPORT_C int computeSha(int shaType, uint8_t* msg, uint32_t msgBytes, crypto_buffer_t* digset)
{
	NLog_Info("computeSha(...):");
	NLog_Info("shaType = " + std::to_string(shaType));
	NLog_Info("msg = " + make_hex_string(msg, msgBytes));
	NLog_Info("msgBytes = " + std::to_string(msgBytes));

	sha_type_e tmpShaType = (sha_type_e)shaType;
	int result = (int)Sha_Generate(tmpShaType, msg, msgBytes, digset);
	return result;
}

EXPORT_C int computeHmacDrbg_Instantiate(bool requestPredictionResistance, int shaType,
	uint8_t* personalizationString, uint32_t personalizationStringBytes,
	uint8_t* entropy, uint32_t entropyBytes,
	uint8_t* nonce, uint32_t nonceBytes)
{
	NLog_Info("computeHmacDrbg_Instantiate(...):");
	NLog_Info("requestPredictionResistance = " + std::to_string(requestPredictionResistance));
	NLog_Info("shaType = " + std::to_string(shaType));
	NLog_Info("personalizationString = " + make_hex_string(personalizationString, personalizationStringBytes));
	NLog_Info("personalizationStringBytes = " + std::to_string(personalizationStringBytes));
	NLog_Info("entropy = " + make_hex_string(entropy, entropyBytes));
	NLog_Info("entropyBytes = " + std::to_string(entropyBytes));
	NLog_Info("nonce = " + make_hex_string(nonce, nonceBytes));
	NLog_Info("nonceBytes = " + std::to_string(nonceBytes));

	sha_type_e tmpShaType = (sha_type_e)shaType;
	int result = (int)Hmac_Drbg_Instantiate(requestPredictionResistance, tmpShaType,
		personalizationString, personalizationStringBytes,
		entropy, entropyBytes,
		nonce, nonceBytes);
	return result;
}

EXPORT_C int computeHmacDrbg_Reseed(bool requestPredictionResistance,
	uint8_t* entropy, uint32_t entropyBytes,
	uint8_t* additionalInput, uint32_t additionalInputBytes)
{
	NLog_Info("computeHmacDrbg_Reseed(...):");
	NLog_Info("requestPredictionResistance = " + std::to_string(requestPredictionResistance));
	NLog_Info("entropy = " + make_hex_string(entropy, entropyBytes));
	NLog_Info("entropyBytes = " + std::to_string(entropyBytes));
	NLog_Info("additionalInput = " + make_hex_string(additionalInput, additionalInputBytes));
	NLog_Info("additionalInputBytes = " + std::to_string(additionalInputBytes));

	int result = (int)Hmac_Drbg_Reseed(requestPredictionResistance,
		entropy, entropyBytes,
		additionalInput, additionalInputBytes);
	return result;
}

EXPORT_C int computeHmacDrbg_Generate(uint32_t bytesRequested,
	uint8_t* entropy, uint32_t entropyBytes,
	uint8_t* additionalInput, uint32_t additionalInputBytes,
	crypto_buffer_t* bytesReturned)
{
	NLog_Info("computeHmacDrbg_Generate(...):");
	NLog_Info("bytesRequested = " + std::to_string(bytesRequested));
	NLog_Info("entropy = " + make_hex_string(entropy, entropyBytes));
	NLog_Info("entropyBytes = " + std::to_string(entropyBytes));
	NLog_Info("additionalInput = " + make_hex_string(additionalInput, additionalInputBytes));
	NLog_Info("additionalInputBytes = " + std::to_string(additionalInputBytes));

	int result = (int)Hmac_Drbg_Generate(bytesRequested,
		entropy, entropyBytes,
		additionalInput, additionalInputBytes,
		bytesReturned);
	return result;
}

EXPORT_C int computeHmacDrbg_Uninstantiate()
{
	NLog_Info("computeHmacDrbg_Instantiate(requestPredictionResistance, shaType, personalizationString, personalizationStringBytes, entInp, entInpBytes, nonce, nonceBytes, entInpRes, entInpResBytes, addInpRes, addInpResBytes, addInp1, addInp1Bytes, addInp2, addInp2Bytes, entInpPr1, entInpPr1Bytes, entInpPr2, entInpPr2Bytes, returnedBytes):");

	int result = (int)Hmac_Drbg_Uninstantiate();
	return result;
}

EXPORT_C int computeHmacSha(int shaType, uint8_t msg[], uint32_t msgBytes, uint8_t key[], uint32_t keyBytes, crypto_buffer_t* digset)
{
	NLog_Info("computeHmacSha(...):");
	NLog_Info("shaType = " + std::to_string(shaType));
	NLog_Info("msg = " + make_hex_string(msg, msgBytes));
	NLog_Info("msgBytes = " + std::to_string(msgBytes));
	NLog_Info("key = " + make_hex_string(key, keyBytes));
	NLog_Info("keyBytes = " + std::to_string(keyBytes));

	sha_type_e tmpShaType = (sha_type_e)shaType;
	int result = (int)Hmac_Sha_Generate(tmpShaType, msg, msgBytes, key, keyBytes, digset);
	return result;
}

EXPORT_C int computeRsaVerify(int shaType, uint8_t msg[], uint32_t msgBytes, uint8_t e[], uint32_t eBytes,
	uint8_t n[], uint32_t nBytes, uint8_t s[], uint32_t sBytes)
{
	NLog_Info("computeRsaVerify(...):");
	NLog_Info("shaType = " + std::to_string(shaType));
	NLog_Info("msg = " + make_hex_string(msg, msgBytes));
	NLog_Info("msgBytes = " + std::to_string(msgBytes));
	NLog_Info("e = " + make_hex_string(e, eBytes));
	NLog_Info("eBytes = " + std::to_string(eBytes));
	NLog_Info("n = " + make_hex_string(n, nBytes));
	NLog_Info("nBytes = " + std::to_string(nBytes));
	NLog_Info("s = " + make_hex_string(s, sBytes));
	NLog_Info("sBytes = " + std::to_string(sBytes));

	sha_type_e tmpShaType = (sha_type_e)shaType;
	int result = (int)Rsa_Verify(tmpShaType, msg, msgBytes, e, eBytes, n, nBytes, s, sBytes);
	return result;
}

EXPORT_C int computeRsaSign(int shaType, uint8_t msg[], uint32_t msgBytes, uint8_t e[], uint32_t eBytes,
	uint8_t n[], uint32_t nBytes, uint8_t d[], uint32_t dBytes, crypto_buffer_t* signature)
{
	NLog_Info("computeRsaSign(...):");
	NLog_Info("shaType = " + std::to_string(shaType));
	NLog_Info("msg = " + make_hex_string(msg, msgBytes));
	NLog_Info("msgBytes = " + std::to_string(msgBytes));
	NLog_Info("e = " + make_hex_string(e, eBytes));
	NLog_Info("eBytes = " + std::to_string(eBytes));
	NLog_Info("n = " + make_hex_string(n, nBytes));
	NLog_Info("nBytes = " + std::to_string(nBytes));
	NLog_Info("d = " + make_hex_string(d, dBytes));
	NLog_Info("dBytes = " + std::to_string(dBytes));

	sha_type_e tmpShaType = (sha_type_e)shaType;
	int result = (int)Rsa_Sign(tmpShaType, msg, msgBytes, e, eBytes, n, nBytes, d, dBytes, signature);
	return result;
}

EXPORT_C int computeHkdf(int shaType, uint8_t key[], uint32_t keyBytes, uint8_t fixedData[], uint32_t fixedDataBytes, uint32_t outputKeyBytes, crypto_buffer_t* digset)
{
	NLog_Info("computeHkdf(...):");
	NLog_Info("shaType = " + std::to_string(shaType));
	NLog_Info("key = " + make_hex_string(key, keyBytes));
	NLog_Info("keyBytes = " + std::to_string(keyBytes));
	NLog_Info("fixedData = " + make_hex_string(fixedData, fixedDataBytes));
	NLog_Info("fixedDataBytes = " + std::to_string(fixedDataBytes));
	NLog_Info("outputKeyBytes = " + std::to_string(outputKeyBytes));

	sha_type_e tmpShaType = (sha_type_e)shaType;
	int result = (int)Hkdf_Generate(tmpShaType, key, keyBytes, fixedData, fixedDataBytes, outputKeyBytes, digset);
	return result;
}

EXPORT_C int computeAesEncrypt(int aesType, uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes, crypto_buffer_t* digset)
{
	NLog_Info("computeAesEncrypt(...):");
	NLog_Info("aesType = " + std::to_string(aesType));
	NLog_Info("key = " + make_hex_string(key, keyBytes));
	NLog_Info("keyBytes = " + std::to_string(keyBytes));
	NLog_Info("msg = " + make_hex_string(msg, msgBytes));
	NLog_Info("msgBytes = " + std::to_string(msgBytes));
	NLog_Info("iv = " + make_hex_string(iv, ivBytes));
	NLog_Info("ivBytes = " + std::to_string(ivBytes));

	aes_type_e tmpAesType = (aes_type_e)aesType;
	int result = (int)Aes_Encrypt_Generate(tmpAesType, key, keyBytes, msg, msgBytes, iv, ivBytes, digset);
	return result;
}

EXPORT_C int computeAesDecrypt(int aesType, uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes, crypto_buffer_t* digset)
{
	NLog_Info("computeAesDecrypt(...):");
	NLog_Info("aesType = " + std::to_string(aesType));
	NLog_Info("key = " + make_hex_string(key, keyBytes));
	NLog_Info("keyBytes = " + std::to_string(keyBytes));
	NLog_Info("msg = " + make_hex_string(msg, msgBytes));
	NLog_Info("msgBytes = " + std::to_string(msgBytes));
	NLog_Info("iv = " + make_hex_string(iv, ivBytes));
	NLog_Info("ivBytes = " + std::to_string(ivBytes));

	aes_type_e tmpAesType = (aes_type_e)aesType;
	int result = (int)Aes_Decrypt_Generate(tmpAesType, key, keyBytes, msg, msgBytes, iv, ivBytes, digset);
	return result;
}

EXPORT_C int computeKeyWrap(uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes, crypto_buffer_t* digset)
{
	NLog_Info("computeKeyWrap(...):");
	NLog_Info("key = " + make_hex_string(key, keyBytes));
	NLog_Info("keyBytes = " + std::to_string(keyBytes));
	NLog_Info("msg = " + make_hex_string(msg, msgBytes));
	NLog_Info("msgBytes = " + std::to_string(msgBytes));
	NLog_Info("iv = " + make_hex_string(iv, ivBytes));
	NLog_Info("ivBytes = " + std::to_string(ivBytes));

	int result = (int)Aes_KeyWrap_Generate(key, keyBytes, msg, msgBytes, iv, ivBytes, digset);
	return result;
}

EXPORT_C int computeKeyUnwrap(uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes, crypto_buffer_t* digset)
{
	NLog_Info("computeKeyUnwrap(...):");
	NLog_Info("key = " + make_hex_string(key, keyBytes));
	NLog_Info("keyBytes = " + std::to_string(keyBytes));
	NLog_Info("msg = " + make_hex_string(msg, msgBytes));
	NLog_Info("msgBytes = " + std::to_string(msgBytes));
	NLog_Info("iv = " + make_hex_string(iv, ivBytes));
	NLog_Info("ivBytes = " + std::to_string(ivBytes));

	int result = (int)Aes_KeyUnwrap_Generate(key, keyBytes, msg, msgBytes, iv, ivBytes, digset);
	return result;
}

EXPORT_C int nlogDump(uint8_t dumpId, const char* filePath, uint32_t filePathLen)
{
	std::string tmpFilePath = std::string(filePath, filePathLen);
	int result = (int)NLog_Dump(dumpId, tmpFilePath);
	return result;
}

EXPORT_C int nlogDumpAll(const char* filePath, uint32_t filePathLen)
{
	std::string tmpFilePath = std::string(filePath, filePathLen);
	int result = (int)NLog_DumpAll(tmpFilePath);
	return result;
}

