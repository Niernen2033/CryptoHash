using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using openssl_app.dllmanager;
using openssl_app.logmanager;

namespace openssl_app.algorithms
{
    class ShaProvider
    {
        [DllImport(DllProperties.DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
        private static extern int computeSha(int shaType, byte[] msg, uint msgBytes, out crypto_buffer_t digset);

        public byte[] Hash { get; private set; }
        public SHA_TYPE Type { get; set; }
        public byte[] Msg { get; set; }

        public ShaProvider()
        {
            this.Hash = null;
            this.Type = SHA_TYPE.SHA_256;
            this.Msg = null;
        }

        public ShaProvider(SHA_TYPE type)
        {
            this.Hash = null;
            this.Type = type;
            this.Msg = null;
        }

        public CRYPTO_STATUS ComputeHash()
        {
            CRYPTO_STATUS result = CRYPTO_STATUS.CRYPTO_ERROR;
            crypto_buffer_t hash = new crypto_buffer_t();
            try
            {
                int computeStatus = computeSha((int)this.Type, this.Msg, (uint)this.Msg.Length, out hash);
                result = (CRYPTO_STATUS)computeStatus;
            }
            catch(Exception exc)
            {
                LogManager.ShowMessageBox("Error", "ShaHash failed", exc.Message);
            }

            if(result == CRYPTO_STATUS.CRYPTO_SUCCESS)
            {
                this.Hash = hash.buffer.Take((int)hash.bytes).ToArray();
            }

            return result;
        }
    }
}
