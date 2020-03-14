#pragma once

#include <crypto_types.h>
#include <openssl/evp.h>

#define AES_KEY_WRAP_IV_BYTES                         8
#define AES_KEY_WRAP_ENCRYPTION_BUFFER_BYTES          16

typedef enum aes_type_e
{
	//ECB
	AES_ECB_128 = 0,
	AES_ECB_192,
	AES_ECB_256,
	//CBC
	AES_CBC_128,
	AES_CBC_192,
	AES_CBC_256,
	//CTR
	AES_CTR_128,
	AES_CTR_192,
	AES_CTR_256,
	//GCM
	AES_GCM_128,
	AES_GCM_192,
	AES_GCM_256,
	//XTS
	AES_XTS_128,
	AES_XTS_256,
} aes_type_e;

const EVP_CIPHER* Aes_GetEvpCipher(aes_type_e aesType);

crypto_status_e Aes_Encrypt_Generate(aes_type_e aesType, uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes);
crypto_status_e Aes_Encrypt_Generate(aes_type_e aesType, uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes, crypto_buffer_t* digset);
crypto_status_e Aes_Decrypt_Generate(aes_type_e aesType, uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes);
crypto_status_e Aes_Decrypt_Generate(aes_type_e aesType, uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes, crypto_buffer_t* digset);

crypto_status_e Aes_KeyWrap_Generate(uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes);
crypto_status_e Aes_KeyWrap_Generate(uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes, crypto_buffer_t* digset);
crypto_status_e Aes_KeyUnwrap_Generate(uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes);
crypto_status_e Aes_KeyUnwrap_Generate(uint8_t key[], uint32_t keyBytes, uint8_t msg[], uint32_t msgBytes,
	uint8_t iv[], uint32_t ivBytes, crypto_buffer_t* digset);
