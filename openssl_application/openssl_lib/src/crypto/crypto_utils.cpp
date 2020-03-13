#include <version.h>
#include <crypto_utils.h>
#include <e_assert.h>
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

void* memsetAssert(void* _Dst, size_t _DstBytes, int _Val, size_t _Bytes)
{
#if ENABLE_SAFE_MEMORY
	ASSERT_M(_DstBytes >= _Bytes, "_DstBytes >= _Bytes");
	ASSERT_M(_Dst != nullptr, "_Dst != nullptr");
#endif // ENABLE_SAFE_MEMORY
	return std::memset(_Dst, _Val, _Bytes);
}

void* memcpyAssert(void* _Dst, size_t _DstBytes, void* _Src, size_t _Bytes)
{
#if ENABLE_SAFE_MEMORY
	ASSERT_M(_DstBytes >= _Bytes, "_DstBytes >= _Bytes");
	ASSERT_M(_Dst != nullptr, "_Dst != nullptr");
	ASSERT_M(_Src != nullptr, "_Src != nullptr");
#endif // ENABLE_SAFE_MEMORY
	return std::memcpy(_Dst, _Src, _Bytes);
}