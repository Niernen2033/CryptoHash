#include "crypto_utils.h"
#include "assert.h"
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

void* memsetSafe(void* _Dst, size_t _DstBytes, int _Val, size_t _Bytes)
{
	ASSERT_M(_DstBytes >= _Bytes, "_DstBytes >= _Bytes");
	return std::memset(_Dst, _Val, _Bytes);
}

void* memsetSafe(crypto_buffer_t _Dst, int _Val, size_t _Bytes)
{
	_Dst.bytes = _Bytes;
	return memsetSafe(_Dst.buffer, sizeof(_Dst.buffer), _Val, _Bytes);
}

void* memcpySafe(void* _Dst, size_t _DstBytes, void* _Src, size_t _Bytes)
{
	ASSERT_M(_DstBytes >= _Bytes, "_DstBytes >= _Bytes");
	return std::memcpy(_Dst, _Src, _Bytes);
}

void* memcpySafe(crypto_buffer_t _Dst, void* _Src, size_t _Bytes)
{
	_Dst.bytes = _Bytes;
	return memcpySafe(_Dst.buffer, sizeof(_Dst.buffer), _Src, _Bytes);
}

void* memcpySafe(void* _Dst, size_t _DstBytes, crypto_buffer_t _Src)
{
	return memcpySafe(_Dst, _DstBytes, _Src.buffer, _Src.bytes);
}