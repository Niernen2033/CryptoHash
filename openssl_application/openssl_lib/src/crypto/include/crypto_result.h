#pragma once

#include <crypto_types.h>

typedef struct crypto_result_t
{
	crypto_buffer_t data;
	crypto_status_e status;
} crypto_result_t;

bool CryRes_Init();
bool CryRes_Cleanup();
void CryRes_SetLastResult(crypto_buffer_t* cBuffer, crypto_status_e status);
crypto_result_t* CryRes_GetLastResult();