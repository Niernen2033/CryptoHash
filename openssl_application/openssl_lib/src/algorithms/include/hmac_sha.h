#pragma once

#include <sha.h>

crypto_status_e Hmac_Sha_Generate(sha_type_e shaType, uint8_t msg[], uint32_t msgBytes, uint8_t key[], uint32_t keyBytes);
crypto_status_e Hmac_Sha_Generate(sha_type_e shaType, uint8_t msg[], uint32_t msgBytes, uint8_t key[], uint32_t keyBytes, crypto_buffer_t* digset);


