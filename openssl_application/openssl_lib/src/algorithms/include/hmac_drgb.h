#pragma once

#include <hmac_sha.h>

#define ENTROPY_INPUT_MAX_SIZE_BYTES                        128
#define HMAC_DRBG_MAX_OUTLEN_BYTES                          64
#define NONCE_MAX_SIZE_BYTES                                64
#define PERSONALIZATION_STRING_MAX_SIZE_BYTES               32
#define HMAC_DRBG_SHA256_OUTLEN_BYTES                       32
#define SECURITY_STRENGTH_BITS                              256
#define HMAC_DRBG_SEED_MATERIAL_BYTES                       (ENTROPY_INPUT_MAX_SIZE_BYTES + NONCE_MAX_SIZE_BYTES + PERSONALIZATION_STRING_MAX_SIZE_BYTES)
#define UPDATE_TEMP_BUFFER_BYTES                            (ENTROPY_INPUT_MAX_SIZE_BYTES + NONCE_MAX_SIZE_BYTES + PERSONALIZATION_STRING_MAX_SIZE_BYTES + 1 + HMAC_DRBG_MAX_OUTLEN_BYTES)
#define ADDITIONAL_INPUT_MAX_SIZE_BYTES                     (NONCE_MAX_SIZE_BYTES + PERSONALIZATION_STRING_MAX_SIZE_BYTES)
#define MAXIMUM_NUMBER_OF_BITS_PER_REQEUST                  7500
#define RESEED_INTERVAL                                     10000

/*

crypto_status_e Hmac_Drgb_GenerateNormal(sha_type_e shaType, bool preRes, uint8_t perStr[], uint32_t perStrBytes, uint8_t entInp[], uint32_t entInpBytes,
	uint8_t nonce[], uint32_t nonceBytes, uint8_t entInpRes[], uint32_t entInpResBytes, uint8_t addInpRes[], uint32_t addInpResBytes,
	uint8_t addInp1[], uint32_t addInp1Bytes, uint8_t addInp2[], uint32_t addInp2Bytes,
	uint8_t entInpPr1[], uint32_t entInpPr1Bytes, uint8_t entInpPr2[], uint32_t entInpPr2Bytes,
	uint32_t returnedBytes);

*/