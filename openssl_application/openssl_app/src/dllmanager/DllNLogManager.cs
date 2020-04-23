using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace openssl_app.dllmanager
{
    static class DllNLogManager
    {
        [DllImport(DllProperties.DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
        private static extern int nlogDump(byte dumpId, string filePath, uint filePathLen);

        [DllImport(DllProperties.DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
        private static extern int nlogDumpAll(string filePath, uint filePathLen);

        public static bool DllNlogDump(NLOG_ID nlogId, string filePath)
        {
            int internalStatus = -1;
            switch(nlogId)
            {
                case NLOG_ID.NLOG_ID_DEGUB:
                case NLOG_ID.NLOG_ID_ERROR:
                case NLOG_ID.NLOG_ID_INFO:
                    internalStatus = nlogDump((byte)nlogId, filePath, (uint)filePath.Length);
                    break;
                case NLOG_ID.NLOG_ID_ALL:
                    internalStatus = nlogDumpAll(filePath, (uint)filePath.Length);
                    break;
                default:
                    return false;
            }
            if(internalStatus != 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
