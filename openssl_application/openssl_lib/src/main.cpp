#include <version.h>
#include <memory>
#include <Windows.h>
#include <NLog.h>
#include <crypto_result.h>
#include <sha.h>
#include <hmac_sha.h>
#include <hmac_drgb.h>
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
		NLog_Init();
		CryRes_Init();
		break;
	case DLL_THREAD_ATTACH:
		// A thread is created. Do any required initialization on a per thread basis
		NLog_Init();
		CryRes_Init();
		break;
	case DLL_THREAD_DETACH:
		// Thread exits with cleanup
		NLog_Cleanup();
		CryRes_Cleanup();
		break;
	case DLL_PROCESS_DETACH:
		// The DLL unmapped from process's address space. Do necessary cleanup
		NLog_Cleanup();
		CryRes_Cleanup();
		break;
	}
	return TRUE;
}

EXPORT_C int computeSha(int shaType, uint8_t* msg, uint32_t msgBytes, crypto_buffer_t* digset)
{
	sha_type_e tmpShaType = (sha_type_e)shaType;
	int result = (int)Sha_Generate(tmpShaType, msg, msgBytes, digset);
	return result;
}

EXPORT_C int computeHmacSha(int shaType, uint8_t msg[], uint32_t msgBytes, uint8_t key[], uint32_t keyBytes, crypto_buffer_t* digset)
{
	sha_type_e tmpShaType = (sha_type_e)shaType;
	int result = (int)Hmac_Sha_Generate(tmpShaType, msg, msgBytes, key, keyBytes, digset);
	return result;
}

EXPORT_C int computeRsaVerify(int shaType, uint8_t msg[], uint32_t msgBytes, uint8_t e[], uint32_t eBytes,
	uint8_t n[], uint32_t nBytes, uint8_t s[], uint32_t sBytes)
{
	sha_type_e tmpShaType = (sha_type_e)shaType;
	int result = (int)Rsa_Verify(tmpShaType, msg, msgBytes, e, eBytes, n, nBytes, s, sBytes);
	return result;
}

EXPORT_C int computeHkdf(int shaType, uint8_t key[], uint32_t keyBytes, uint8_t fixedData[], uint32_t fixedDataBytes, uint32_t outputKeyBytes, crypto_buffer_t* digset)
{
	sha_type_e tmpShaType = (sha_type_e)shaType;
	int result = (int)Hkdf_Generate(tmpShaType, key, keyBytes, fixedData, fixedDataBytes, outputKeyBytes, digset);
	return result;
}

EXPORT_C int computeAesEncrypt(int aesType, uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes, crypto_buffer_t* digset)
{
	aes_type_e tmpAesType = (aes_type_e)aesType;
	int result = (int)Aes_Encrypt_Generate(tmpAesType, key, keyBytes, msg, msgBytes, iv, ivBytes, digset);
	return result;
}

EXPORT_C int computeAesDecrypt(int aesType, uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes, crypto_buffer_t* digset)
{
	aes_type_e tmpAesType = (aes_type_e)aesType;
	int result = (int)Aes_Decrypt_Generate(tmpAesType, key, keyBytes, msg, msgBytes, iv, ivBytes, digset);
	return result;
}

EXPORT_C int computeKeyWrap(uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes, crypto_buffer_t* digset)
{
	int result = (int)Aes_KeyWrap_Generate(key, keyBytes, msg, msgBytes, iv, ivBytes, digset);
	return result;
}

EXPORT_C int computeKeyUnwrap(uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes, crypto_buffer_t* digset)
{
	int result = (int)Aes_KeyUnwrap_Generate(key, keyBytes, msg, msgBytes, iv, ivBytes, digset);
	return result;
}

EXPORT_C int nlogDump(int logId, const char* filePath, uint32_t filePathLen)
{
	nlog_id_e tmpLogId = (nlog_id_e)logId;
	std::string tmpFilePath = std::string(filePath, filePathLen);
	int result = (int)NLog_Dump(tmpLogId, filePath);
	return result;
}

