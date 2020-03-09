#include "NLog.h"
#include "assert.h"
#include "crypto_utils.h"
#include <iostream>
#include <fstream>

#define _SILENCE_EXPERIMENTAL_FILESYSTEM_DEPRECATION_WARNING
#ifdef __cpp_lib_filesystem
#include <filesystem>
using std::filesystem::path;
#elif __cpp_lib_experimental_filesystem
#include <experimental/filesystem>
using std::experimental::filesystem::path;
#else 
#error No filesystem support
#endif

static nlog_status_t nlogStatus;

static bool appendLineToFile(std::string filepath, std::string line);

void NLog_Init()
{
	nlogStatus.state = 0;
	nlogStatus.filePath = "nlog.txt";
	nlogStatus.raiseAssertOnFail = false;
}

void NLog_SetState(uint8_t state, bool raiseAssertOnFail, std::string filePath = "nlog.txt")
{
	nlogStatus.state = state;
	nlogStatus.filePath = filePath;
	nlogStatus.raiseAssertOnFail = raiseAssertOnFail;
}

void NLog_Add(uint8_t logId, const char* file, int line, std::string msg)
{
	// E = NLOG_ID_ERROR
	// D = NLOG_ID_DEGUB
	// I = NLOG_ID_INFO
	// 0 0 0 0 0 1 1 1 << logId
	//           ^ ^ ^
	//           E D I
	//*****************************
	// 1 << NLOG_ID_INFO
	// 2 << NLOG_ID_DEGUB
	// 3 << NLOG_ID_INFO + NLOG_ID_DEGUB
	// 4 << NLOG_ID_ERROR
	// 5 << NLOG_ID_ERROR + NLOG_ID_INFO
	// 6 << NLOG_ID_ERROR + NLOG_ID_DEGUB
	// 7 << NLOG_ID_ERROR + NLOG_ID_DEGUB + NLOG_ID_INFO

	if ((nlogStatus.state & logId) != 0)
	{
		std::string log = path(file).filename().string() + "(" + std::to_string(line) + "): " + msg;
		if (!appendLineToFile(nlogStatus.filePath, log))
		{
			nlogStatus.state = 0;
		}
	}
}

static bool appendLineToFile(std::string filepath, std::string line)
{
	std::ofstream file;
	file.exceptions(std::ios::failbit | std::ios::badbit);
	try
	{
		file.open(filepath, std::ios::out | std::ios::app);
		if(file)
		{
			file << line << std::endl;
		}
		file.close();
	}
	catch (std::exception const& e)
	{
		file.close();
		if (nlogStatus.raiseAssertOnFail)
		{
			ASSERT_M(false, e.what());
		}
		return false;
	}

	return true;
}