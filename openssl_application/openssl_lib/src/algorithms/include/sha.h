#pragma once

#include "crypto_types.h"
#include "crypto_utils.h"
#include <openssl/evp.h>

#define SHA_224_DIGEST_LEN_BITS         224
#define SHA_224_DIGEST_LEN_BYTES        BITS_TO_BYTES(SHA_224_DIGEST_LEN_BITS)
#define SHA_256_DIGEST_LEN_BITS         256
#define SHA_256_DIGEST_LEN_BYTES        BITS_TO_BYTES(SHA_256_DIGEST_LEN_BITS)
#define SHA_384_DIGEST_LEN_BITS         384
#define SHA_384_DIGEST_LEN_BYTES        BITS_TO_BYTES(SHA_384_DIGEST_LEN_BITS)
#define SHA_512_DIGEST_LEN_BITS         512
#define SHA_512_DIGEST_LEN_BYTES        BITS_TO_BYTES(SHA_512_DIGEST_LEN_BITS)

typedef enum sha_type_e
{
    SHA_224,
    SHA_256,
    SHA_384,
    SHA_512
} sha_type_e;


crypto_status_e Sha_Generate(sha_type_e shaType, uint8_t* msg, uint32_t msgBytes);
crypto_status_e Sha_Generate(sha_type_e shaType, uint8_t* msg, uint32_t msgBytes, crypto_buffer_t* digset);

const EVP_MD* Sha_GetEvpMd(sha_type_e shaType);
uint32_t Sha_GetDigsetBytes(sha_type_e shaType);
uint32_t Sha_GetDigsetBits(sha_type_e shaType);