#pragma once

#include <cstdint>
#include <memory>

#define CRYPTO_BUFFER_MAX_BYTES         1024

typedef enum
{
	CRYPTO_SUCCESS = 0,
	CRYPTO_ERROR,
	CRYPTO_INIT_ERROR,
	CRYPTO_UPDATE_ERROR,
	CRYPTO_FINAL_ERROR,
	CRYPTO_BUFFER_OVERFLOW_ERROR,
	CRYPTO_BUFFER_SIZE_MISMATCH_ERROR,
	CRYPTO_UNSUPPORTED,
	CRYPTO_NULL_PTR_ERROR,
	CRYPTO_INTEGRITY_ERROR,
	CRYPTO_NOT_SUPPORTED
} CRYPTO_STATUS;

typedef struct
{
	uint8_t buffer[CRYPTO_BUFFER_MAX_BYTES];
	uint32_t bytes;
} buffer_t;

typedef struct
{
	buffer_t data;
	CRYPTO_STATUS status;
} crypto_data_t;