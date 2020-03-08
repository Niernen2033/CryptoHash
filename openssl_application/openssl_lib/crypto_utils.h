#pragma once

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