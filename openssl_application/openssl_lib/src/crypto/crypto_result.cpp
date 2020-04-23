#include <crypto_result.h>
#include <crypto_utils.h>
#include <e_assert.h>
#include <memory>
#include <NLog.h>

static std::unique_ptr<crypto_result_t> resultData;

bool CryRes_Init()
{
    resultData = std::make_unique<crypto_result_t>();
	if (resultData == nullptr)
	{
		ASSERT_M(false, "resultInitStatus = false");
		return false;
	}
	memsetAssert(resultData.get(), sizeof(crypto_result_t), 0, sizeof(crypto_result_t));
	return true;
}

bool CryRes_Cleanup()
{
	resultData.reset(nullptr);
	return true;
}

void CryRes_SetLastResult(crypto_buffer_t* cBuffer, crypto_status_e status)
{
	if (cBuffer == nullptr)
	{
		NLog_Error("cBuffer == nullptr");
		return;
	}

	resultData->status = status;
	memcpyAssert(&resultData->data, sizeof(crypto_buffer_t), cBuffer, sizeof(*cBuffer));
}

crypto_result_t* CryRes_GetLastResult()
{
	return resultData.get();
}