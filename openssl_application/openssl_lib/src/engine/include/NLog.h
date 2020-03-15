#pragma once

#include <memory>
#include <string>
#include <vector>

typedef enum nlog_id_e
{
    NLOG_ID_ERROR = 0,
    NLOG_ID_INFO,
    NLOG_ID_DEGUB,
} nlog_id_e;

typedef struct nlog_data_t
{
    nlog_id_e id;
    std::string log;
} nlog_data_t;

bool NLog_Init();
bool NLog_Cleanup();
void NLog_Add(nlog_id_e logId, std::string tag, const char* file, int line, std::string msg);
bool NLog_Dump(nlog_id_e logId, std::string filePath);
bool NLog_DumpAll(std::string filePath);
void NLog_Clear();

#define NLog_Error(str)         NLog_Add(NLOG_ID_ERROR, "ERROR", __FILE__, __LINE__, str)
#define NLog_Info(str)          NLog_Add(NLOG_ID_INFO, "INFO", __FILE__, __LINE__, str)
#define NLog_Debug(str)         NLog_Add(NLOG_ID_DEGUB, "DEBUG", __FILE__, __LINE__, str)