#include <memory>
#include <Windows.h>
#include <sha.h>
#include <NLog.h>
#include <crypto_result.h>

#define EXPORT extern "C" __declspec(dllexport)

EXPORT int computeSha(int shaType, uint8_t* msg, uint32_t msgBytes, crypto_buffer_t* digset)
{
	sha_type_e tmpShaType = (sha_type_e)shaType;
	int result = (int)Sha_Generate(tmpShaType, msg, msgBytes, digset);
	return result;
}

EXPORT int nlogDump(int logId, const char* filePath, uint32_t filePathLen)
{
	nlog_id_e tmpLogId = (nlog_id_e)logId;
	std::string tmpFilePath = std::string(filePath, filePathLen);
	int result = (int)NLog_Dump(tmpLogId, filePath);
	return result;
}

extern "C" BOOL WINAPI DllMain(HANDLE hModule, DWORD fdwreason, LPVOID lpReserved)
{
	switch (fdwreason) {
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