#pragma once

#include <memory>
#include <string>

typedef struct nlog_status_t
{
    uint8_t state;
    std::string filePath;
    bool raiseAssertOnFail;
} nlog_status_t;

typedef enum nlog_log_e
{
    NLOG_ID_ERROR = 0,
    NLOG_ID_INFO,
    NLOG_ID_DEGUB,
} nlog_id_e;

void NLog_Init();
void NLog_SetState(uint8_t state, bool raiseAssertOnFail, std::string filePath);
void NLog_Add(uint8_t logId, const char* file, int line, std::string msg);

#define NLog_Error(str)         NLog_Add(NLOG_ID_ERROR, __FILE__, __LINE__, str)
#define NLog_Info(str)          NLog_Add(NLOG_ID_INFO, __FILE__, __LINE__, str)
#define NLog_Debug(str)         NLog_Add(NLOG_ID_DEGUB, __FILE__, __LINE__, str)