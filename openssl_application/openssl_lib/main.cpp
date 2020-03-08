#include <memory>
#include "sha.h"

#define EXPORT extern "C" __declspec(dllexport)

EXPORT crypto_data_t computeSha(SHA_TYPE shaType, uint8_t msg[], uint32_t msgBytes)
{
	crypto_data_t result = Sha_GenerateNormal(shaType, msg, msgBytes);
	return result;
}