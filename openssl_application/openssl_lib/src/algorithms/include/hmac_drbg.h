#pragma once

#include <hmac_sha.h>

#define ENTROPY_INPUT_MAX_SIZE_BYTES                        128
#define HMAC_DRBG_MAX_OUTLEN_BYTES                          64
#define NONCE_MAX_SIZE_BYTES                                64
#define PERSONALIZATION_STRING_MAX_SIZE_BYTES               32
#define HMAC_DRBG_SHA256_OUTLEN_BYTES                       32
#define SECURITY_STRENGTH_BITS                              128
#define HMAC_DRBG_SEED_MATERIAL_BYTES                       (ENTROPY_INPUT_MAX_SIZE_BYTES + NONCE_MAX_SIZE_BYTES + PERSONALIZATION_STRING_MAX_SIZE_BYTES)
#define UPDATE_TEMP_BUFFER_BYTES                            (ENTROPY_INPUT_MAX_SIZE_BYTES + NONCE_MAX_SIZE_BYTES + PERSONALIZATION_STRING_MAX_SIZE_BYTES + 1 + HMAC_DRBG_MAX_OUTLEN_BYTES)
#define ADDITIONAL_INPUT_MAX_SIZE_BYTES                     (NONCE_MAX_SIZE_BYTES + PERSONALIZATION_STRING_MAX_SIZE_BYTES)
#define MAXIMUM_NUMBER_OF_BITS_PER_REQEUST                  7500
#define RESEED_INTERVAL                                     10000


bool Hmac_Drbg_Init();
bool Hmac_Drbg_Cleanup();

crypto_status_e Hmac_Drbg_Instantiate(bool requestPredictionResistance, sha_type_e shaType,
	uint8_t* personalizationString, uint32_t personalizationStringBytes,
	uint8_t* entropy, uint32_t entropyBytes,
	uint8_t* nonce, uint32_t nonceBytes);

crypto_status_e Hmac_Drbg_Reseed(bool requestPredictionResistance,
	uint8_t* entropy, uint32_t entropyBytes,
	uint8_t* additionalInput, uint32_t additionalInputBytes);

crypto_status_e Hmac_Drbg_Generate(uint32_t bytesRequested,
	uint8_t* entropy, uint32_t entropyBytes,
	uint8_t* additionalInput, uint32_t additionalInputBytes,
	crypto_buffer_t* bytesReturned);

crypto_status_e Hmac_Drbg_Uninstantiate();


//preRes = FALSE
//1. Hmac_Drgb_Instantiate
//2. Hmac_Drgb_Reseed
//3. Hmac_Drgb_Generate
//4. Hmac_Drgb_Generate

//preRes = TRUE
//1. Hmac_Drgb_Instantiate
//2. Hmac_Drgb_Generate
//3. Hmac_Drgb_Generate