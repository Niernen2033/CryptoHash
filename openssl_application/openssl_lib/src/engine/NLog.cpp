#include <version.h>
#include <NLog.h>
#include <e_assert.h>
#include <crypto_utils.h>
#include <iostream>
#include <fstream>

#ifdef __cpp_lib_filesystem
#include <filesystem>
using std::filesystem::path;
#elif __cpp_lib_experimental_filesystem
#include <experimental/filesystem>
using std::experimental::filesystem::path;
#else 
#error No filesystem support
#endif

#if ENABLE_NLOG
static std::unique_ptr<nlog_data_t> nlogData[NLOG_ID_MAX_SIZE];
static bool nlogInitStatus = false;
#endif // ENABLE_NLOG

void NLog_Init()
{
#if ENABLE_NLOG
	nlogInitStatus = false;
	for (int i = 0; i < NLOG_ID_MAX_SIZE; i++)
	{
		nlogData[i] = std::make_unique<nlog_data_t>();
		if (nlogData[i] == nullptr)
		{
			ASSERT_M(false, "nlogInitStatus = false");
			return;
		}
		nlogData[i]->id = (nlog_id_e)i;
		nlogData[i]->logs.clear();
	}
	nlogInitStatus = true;
#endif // ENABLE_NLOG
}

void NLog_Cleanup()
{
#if ENABLE_NLOG
	if (nlogInitStatus)
	{
		for (int i = 0; i < NLOG_ID_MAX_SIZE; i++)
		{
			nlogData[i].reset(nullptr);
		}
		nlogInitStatus = false;
	}
#endif // ENABLE_NLOG
}

void NLog_Add(nlog_id_e logId, const char* file, int line, std::string msg)
{
#if ENABLE_NLOG
	if (!nlogInitStatus)
	{
		ASSERT_M(false, "nlogInitStatus = false");
		return;
	}
	std::string log = path(file).filename().string() + "(" + std::to_string(line) + "): " + msg;
	nlogData[logId]->logs.push_back(log);
#endif // ENABLE_NLOG
}

bool NLog_Dump(nlog_id_e logId, std::string filePath)
{
#if ENABLE_NLOG
	if (!nlogInitStatus)
	{
		ASSERT_M(false, "nlogInitStatus = false");
		return false;
	}

	std::ofstream file;
	file.exceptions(std::ios::failbit | std::ios::badbit);
	try
	{
		file.open(filePath, std::ios::out);
		for (int i = 0; i < nlogData[logId]->logs.size(); i++)
		{
			if (file)
			{
				file << nlogData[logId]->logs[i] << std::endl;
			}
		}
		file.close();
	}
	catch (std::exception const& e)
	{
		file.close();
		ASSERT_M(false, e.what());
		return false;
	}

	return true;
#else // ENABLE_NLOG
	retrun false;
#endif // ENABLE_NLOG
}

void NLog_Clear()
{
#if ENABLE_NLOG
	if (!nlogInitStatus)
	{
		ASSERT_M(false, "nlogInitStatus = false");
		return;
	}

	for (int i = 0; i < NLOG_ID_MAX_SIZE; i++)
	{
		nlogData[i]->id = (nlog_id_e)i;
		nlogData[i]->logs.clear();
	}
#endif // ENABLE_NLOG
}