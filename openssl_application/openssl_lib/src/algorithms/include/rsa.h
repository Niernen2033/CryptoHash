#pragma once

#include <sha.h>

crypto_status_e Rsa_Verify(sha_type_e shaType, uint8_t msg[], uint32_t msgBytes, uint8_t e[], uint32_t eBytes,
    uint8_t n[], uint32_t nBytes, uint8_t s[], uint32_t sBytes);

