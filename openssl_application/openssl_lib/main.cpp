#include <memory>
#include <Windows.h>
#include "sha.h"
#include "NLog.h"

#define EXPORT extern "C" __declspec(dllexport)


EXPORT int ShaInitialize(int shaType)
{
	sha_type_e tmpShaType = static_cast<sha_type_e>(shaType);
	crypto_status_e status = Sha_Initialize(tmpShaType);
	return (int)status;
}

EXPORT int ShaUpdate(uint8_t* msg, uint32_t msgBytes)
{
	crypto_status_e status = Sha_Update(msg, msgBytes);
	return (int)status;
}

EXPORT int ShaFinalize(uint8_t* digset, uint32_t digsetBytes, uint32_t* resultBytes)
{
	crypto_status_e status = Sha_Finalize(digset, digsetBytes, resultBytes);
	return (int)status;
}


extern "C" BOOL WINAPI DllMain(HANDLE hModule, DWORD fdwreason, LPVOID lpReserved)
{
	switch (fdwreason) {
	case DLL_PROCESS_ATTACH:
		// The DLL is being mapped into process's address space
		// Do any required initialization on a per application basis, return FALSE if failed
		NLog_Init();
		break;
	case DLL_THREAD_ATTACH:
		// A thread is created. Do any required initialization on a per thread basis
		NLog_Init();
		break;
	case DLL_THREAD_DETACH:
		// Thread exits with cleanup
		Sha_Cleanup();
		break;
	case DLL_PROCESS_DETACH:
		// The DLL unmapped from process's address space. Do necessary cleanup
		Sha_Cleanup();
		break;
	}
	return TRUE;
}