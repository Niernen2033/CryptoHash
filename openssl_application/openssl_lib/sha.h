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

typedef struct sha_data_t
{
    sha_type_e shaType;
    crypto_buffer_t msg;
    crypto_buffer_t digset;
} sha_data_t;

typedef struct sha_runtime_data_t
{
    EVP_MD_CTX* ctx;
    const EVP_MD* evp_md;
} sha_runtime_data_t;

void Sha_Cleanup();
crypto_status_e Sha_Initialize(sha_type_e shaType);
crypto_status_e Sha_Update(uint8_t* msg, uint32_t msgBytes);
crypto_status_e Sha_Finalize(uint8_t* digset, uint32_t digsetBytes, uint32_t* resultBytes);

const EVP_MD* Sha_GetEvpMd(sha_type_e shaType);
uint32_t Sha_GetDigsetBytes(sha_type_e shaType);
uint32_t Sha_GetDigsetBits(sha_type_e shaType);