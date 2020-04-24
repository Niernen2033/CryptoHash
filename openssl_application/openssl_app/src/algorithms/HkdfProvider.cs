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
    class HkdfProvider
    {
        [DllImport(DllProperties.DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
        private static extern int computeHkdf(int shaType,
            byte[] key, uint keyBytes,
            byte[] fixedData, uint fixedDataBytes,
            uint outputKeyBytes, out crypto_buffer_t digset);

        public byte[] Hash { get; private set; }
        public SHA_TYPE Type { get; set; }
        public byte[] Key { get; set; }
        public byte[] FixedData { get; set; }
        public uint OutputKeyBytes { get; set; }

        public HkdfProvider()
        {
            this.Hash = null;
            this.Type = SHA_TYPE.SHA_256;
            this.Key = null;
            this.FixedData = null;
            this.OutputKeyBytes = 0;
        }

        public HkdfProvider(SHA_TYPE type)
        {
            this.Hash = null;
            this.Type = type;
            this.Key = null;
            this.FixedData = null;
            this.OutputKeyBytes = 0;
        }

        public CRYPTO_STATUS ComputeHash()
        {
            CRYPTO_STATUS result = CRYPTO_STATUS.CRYPTO_ERROR;
            crypto_buffer_t hash = new crypto_buffer_t();
            try
            {
                int computeStatus = computeHkdf((int)this.Type, this.Key, (uint)this.Key.Length,
                    this.FixedData, (uint)this.FixedData.Length, this.OutputKeyBytes, out hash);
                result = (CRYPTO_STATUS)computeStatus;
            }
            catch (Exception exc)
            {
                LogManager.ShowMessageBox("Error", "HkdfHash failed", exc.Message);
            }

            if (result == CRYPTO_STATUS.CRYPTO_SUCCESS)
            {
                this.Hash = hash.buffer.Take((int)hash.bytes).ToArray();
            }

            return result;
        }
    }
}
