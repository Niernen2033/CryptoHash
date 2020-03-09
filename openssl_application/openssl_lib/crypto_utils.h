#pragma once

#include "crypto_types.h"
#include <cstdint>
#include <string>

#define BITS_PER_BYTE                                   8

#define REVERSE_ENDIAN_32_BITS( n ) (uint32_t)(\
    ((((uint32_t)(n)) & 0x000000FF) << 24) | \
    ((((uint32_t)(n)) & 0x0000FF00) <<  8) | \
    ((((uint32_t)(n)) & 0x00FF0000) >>  8) | \
    ((((uint32_t)(n)) & 0xFF000000) >> 24))

#define BITS_TO_BYTES( n ) (uint32_t)((uint32_t)(n) >> 3)
#define BYTES_TO_BITS( n ) (uint32_t)((uint32_t)(n) << 3)

std::string make_hex_string(uint8_t data[], uint32_t dataBytes);
void* memsetSafe(void* _Dst, size_t _DstBytes, int _Val, size_t _Bytes);
void* memsetSafe(crypto_buffer_t _Dst, int _Val, size_t _Bytes);

void* memcpySafe(void* _Dst, size_t _DstBytes, void* _Src, size_t _Bytes);
void* memcpySafe(crypto_buffer_t _Dst, void* _Src, size_t _Bytes);
void* memcpySafe(void* _Dst, size_t _DstBytes, crypto_buffer_t _Src);