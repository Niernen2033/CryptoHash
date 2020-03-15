using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using openssl_app.dllmanager;
using System.Collections.ObjectModel;

namespace openssl_app.algorithms
{
    class HkdfProvider
    {
        [DllImport(DllProperties.DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
        private static extern int computeHkdf(int shaType,
            byte[] key, uint keyBytes,
            byte[] fixedData, uint fixedDataBytes,
            uint outputKeyBytes, out crypto_buffer_t digset);

        private List<byte> m_hash;
        public SHA_TYPE Type { get; set; }
        public List<byte> Key { get; set; }
        public List<byte> FixedData { get; set; }
        public uint OutputKeyBytes { get; set; }
        public ReadOnlyCollection<byte> Hash { get { return this.m_hash.AsReadOnly(); } }

        public HkdfProvider()
        {
            this.m_hash = new List<byte>();
            this.Type = SHA_TYPE.SHA_256;
            this.Key = new List<byte>();
            this.FixedData = new List<byte>();
            this.OutputKeyBytes = 0;
        }

        public HkdfProvider(SHA_TYPE type)
        {
            this.m_hash = new List<byte>();
            this.Type = type;
            this.Key = new List<byte>();
            this.FixedData = new List<byte>();
            this.OutputKeyBytes = 0;
        }

        public CRYPTO_STATUS ComputeHash()
        {
            CRYPTO_STATUS result = CRYPTO_STATUS.CRYPTO_ERROR;
            crypto_buffer_t hash = new crypto_buffer_t();
            try
            {
                int computeStatus = computeHkdf((int)this.Type, this.Key.ToArray(), (uint)this.Key.Count,
                    this.FixedData.ToArray(), (uint)this.FixedData.Count, this.OutputKeyBytes, out hash);
                result = (CRYPTO_STATUS)computeStatus;
            }
            catch (Exception e)
            {

            }

            if (result == CRYPTO_STATUS.CRYPTO_SUCCESS)
            {
                this.m_hash = new List<byte>(hash.buffer.Take((int)hash.bytes));
            }

            return result;
        }
    }
}
