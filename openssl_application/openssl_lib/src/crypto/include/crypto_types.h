#pragma once

#include <cstdint>
#include <memory>

#define CRYPTO_BUFFER_DEFAULT_BYTES         2048

typedef enum crypto_status_e
{
	CRYPTO_SUCCESS = 0,
	CRYPTO_ERROR,
	CRYPTO_ABORT_ERROR,
	CRYPTO_NOT_INITIALIZE_ERROR,
	CRYPTO_ALG_ERROR,
	CRYPTO_BUFFER_OVERFLOW_ERROR,
	CRYPTO_BUFFER_MISMATCH_ERROR,
	CRYPTO_NULL_PTR_ERROR,
	CRYPTO_INTEGRITY_ERROR,
	CRYPTO_NOT_SUPPORTED
} crypto_status_e;

typedef struct crypto_buffer_t
{
	uint8_t buffer[CRYPTO_BUFFER_DEFAULT_BYTES];
	uint32_t bytes;
} crypto_buffer_t;