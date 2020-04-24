using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using openssl_app.dllmanager;
using openssl_app.logmanager;

namespace openssl_app.algorithms
{
    class HmacShaProvider
    {
        [DllImport(DllProperties.DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
        private static extern int computeHmacSha(int shaType,
            byte[] msg, uint msgBytes,
            byte[] key, uint keyBytes, out crypto_buffer_t digset);

        public byte[] Hash { get; private set; }
        public SHA_TYPE Type { get; set; }
        public byte[] Msg { get; set; }
        public byte[] Key { get; set; }

        public HmacShaProvider()
        {
            this.Hash = null;
            this.Type = SHA_TYPE.SHA_256;
            this.Msg = null;
            this.Key = null;
        }

        public HmacShaProvider(SHA_TYPE type)
        {
            this.Hash = null;
            this.Type = type;
            this.Msg = null;
            this.Key = null;
        }

        public CRYPTO_STATUS ComputeHash()
        {
            CRYPTO_STATUS result = CRYPTO_STATUS.CRYPTO_ERROR;
            crypto_buffer_t hash = new crypto_buffer_t();
            try
            {
                int computeStatus = computeHmacSha((int)this.Type, 
                    this.Msg, (uint)this.Msg.Length,
                    this.Key, (uint)this.Key.Length, out hash);
                result = (CRYPTO_STATUS)computeStatus;
            }
            catch (Exception exc)
            {
                LogManager.ShowMessageBox("Error", "HmacShaHash failed", exc.Message);
            }

            if (result == CRYPTO_STATUS.CRYPTO_SUCCESS)
            {
                this.Hash = hash.buffer.Take((int)hash.bytes).ToArray();
            }

            return result;
        }
    }
}
