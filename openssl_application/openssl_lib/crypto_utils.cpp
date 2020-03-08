#include "crypto_utils.h"
#include <sstream>
#include <iomanip>

std::string make_hex_string(uint8_t data[], uint32_t dataBytes)
{
	if (data == NULL || dataBytes == 0)
	{
		return "null";
	}

	std::stringstream stream;
	for (uint32_t i = 0; i < dataBytes; i++)
	{
		stream << std::hex << std::setw(2) << std::setfill('0') << static_cast<int>(data[i]);
	}
	return stream.str();
}