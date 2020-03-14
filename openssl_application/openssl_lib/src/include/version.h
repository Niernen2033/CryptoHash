#pragma once

#include <Windows.h>

#define _SILENCE_EXPERIMENTAL_FILESYSTEM_DEPRECATION_WARNING    1
#define EXPORT_C                                                extern "C" __declspec(dllexport)
#define DLL_MAIN_C                                              extern "C" BOOL WINAPI

#if DEBUG
 #define ENABLE_ASSERT                                           (1)
 #define ENABLE_NLOG                                             (1)
 #define ENABLE_SAFE_MEMORY                                      (1 && ENABLE_ASSERT)
#else // DEBUG
 #define ENABLE_ASSERT                                           (1)
 #define ENABLE_NLOG                                             (1)
 #define ENABLE_SAFE_MEMORY                                      (1 && ENABLE_ASSERT)
#endif // DEBUG
