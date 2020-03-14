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
    std::vector<std::string> logs;
} nlog_data_t;

bool NLog_Init();
bool NLog_Cleanup();
void NLog_Add(nlog_id_e logId, const char* file, int line, std::string msg);
bool NLog_Dump(nlog_id_e logId, std::string filePath);
void NLog_Clear();

#define NLOG_ID_MAX_SIZE        3

#define NLog_Error(str)         NLog_Add(NLOG_ID_ERROR, __FILE__, __LINE__, str)
#define NLog_Info(str)          NLog_Add(NLOG_ID_INFO, __FILE__, __LINE__, str)
#define NLog_Debug(str)         NLog_Add(NLOG_ID_DEGUB, __FILE__, __LINE__, str)