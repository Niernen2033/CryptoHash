#pragma once

#include <sha.h>

#define HKDF_ITERATOR_DIGSET_BYTES            (4)

crypto_status_e Hkdf_Generate(sha_type_e shaType, uint8_t key[], uint32_t keyBytes, uint8_t fixedData[], uint32_t fixedDataBytes, uint32_t outputKeyBytes);
crypto_status_e Hkdf_Generate(sha_type_e shaType, uint8_t key[], uint32_t keyBytes, uint8_t fixedData[], uint32_t fixedDataBytes, uint32_t outputKeyBytes, crypto_buffer_t* digset);


