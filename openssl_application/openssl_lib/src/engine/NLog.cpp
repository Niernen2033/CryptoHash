#include <version.h>
#include <NLog.h>
#include <e_assert.h>
#include <crypto_utils.h>
#include <iostream>
#include <iomanip>
#include <sstream>
#include <ctime>
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
static std::unique_ptr< std::vector<nlog_data_t> > nlogData;
static bool nlogInitStatus = false;


static std::string NLog_GetTimestamp();
#endif // ENABLE_NLOG

bool NLog_Init()
{
#if ENABLE_NLOG
	nlogInitStatus = false;
	nlogData = std::make_unique< std::vector<nlog_data_t> >();
	if (nlogData == nullptr)
	{
		ASSERT_M(false, "nlogInitStatus = false");
		return false;
	}
	nlogData->clear();
	nlogInitStatus = true;
#endif // ENABLE_NLOG
	return true;
}

bool NLog_Cleanup()
{
#if ENABLE_NLOG
	if (nlogInitStatus)
	{
		nlogData->clear();
		nlogData.reset(nullptr);
		nlogInitStatus = false;
	}
#endif // ENABLE_NLOG
	return true;
}

#if ENABLE_NLOG
static std::string NLog_GetTimestamp()
{
	std::stringstream timestamp;
	tm gmtm;
	std::time_t time_now = std::time(nullptr);
	localtime_s(&gmtm, &time_now);
	//"%y-%m-%d %OH:%OM:%OS"
	timestamp << std::put_time(&gmtm, "%OH:%OM:%OS");
	return timestamp.str();
}
#endif // ENABLE_NLOG


void NLog_Add(nlog_id_e logId, std::string tag, const char* file, int line, std::string msg)
{
#if ENABLE_NLOG
	nlog_data_t logData;
	std::string log = tag + "[" + NLog_GetTimestamp() + "] " + path(file).filename().string() + "(" + std::to_string(line) + "): " + msg;

	logData.id = logId;
	logData.log = log;
	nlogData->push_back(logData);
#endif // ENABLE_NLOG
}

bool NLog_DumpAll(std::string filePath)
{
#if ENABLE_NLOG
	std::ofstream file;
	file.exceptions(std::ios::failbit | std::ios::badbit);
	try
	{
		file.open(filePath, std::ios::out | std::ios::app);
		for (int i = 0; i < nlogData->size(); i++)
		{
			if (file)
			{
				file << nlogData->at(i).log << std::endl;
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

bool NLog_Dump(nlog_id_e logId, std::string filePath)
{
#if ENABLE_NLOG
	std::ofstream file;
	file.exceptions(std::ios::failbit | std::ios::badbit);
	try
	{
		file.open(filePath, std::ios::out | std::ios::app);
		for (int i = 0; i < nlogData->size(); i++)
		{
			if (file)
			{
				if (nlogData->at(i).id == logId)
				{
					file << nlogData->at(i).log << std::endl;
				}
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
	nlogData->clear();
#endif // ENABLE_NLOG
}