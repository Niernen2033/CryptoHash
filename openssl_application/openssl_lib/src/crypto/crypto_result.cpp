#include <crypto_result.h>
#include <crypto_utils.h>
#include <e_assert.h>
#include <memory>

static std::unique_ptr<crypto_result_t> resultData;
static bool resultInitStatus = false;

void CryRes_Init()
{
    resultInitStatus = false;
    resultData = std::make_unique<crypto_result_t>();
	if (resultData == nullptr)
	{
		ASSERT_M(false, "resultInitStatus = false");
		return;
	}
	memsetAssert(resultData.get(), sizeof(crypto_result_t), 0, sizeof(crypto_result_t));
	resultInitStatus = true;
}

void CryRes_Cleanup()
{
	if (resultInitStatus)
	{
		resultData.reset(nullptr);
		resultInitStatus = false;
	}
}

void CryRes_SetLastResult(crypto_buffer_t* cBuffer, crypto_status_e status)
{
	if (!resultInitStatus)
	{
		ASSERT_M(false, "resultInitStatus = false");
		return;
	}

	if (cBuffer == nullptr)
	{
		return;
	}

	resultData->status = status;
	memcpyAssert(&resultData->data, sizeof(crypto_buffer_t), cBuffer, sizeof(*cBuffer));
}

crypto_result_t* CryRes_GetLastResult()
{
	if (!resultInitStatus)
	{
		ASSERT_M(false, "resultInitStatus = false");
		return nullptr;
	}

	return resultData.get();
}