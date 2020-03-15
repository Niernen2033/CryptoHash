using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using openssl_app.dllmanager;

namespace openssl_app.algorithms
{
    class ShaProvider
    {
        [DllImport(DllProperties.DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
        private static extern int computeSha(int shaType, byte[] msg, uint msgBytes, out crypto_buffer_t digset);

        private List<byte> m_hash;
        public SHA_TYPE Type { get; set; }
        public List<byte> Msg { get; set; }
        public ReadOnlyCollection<byte> Hash { get { return this.m_hash.AsReadOnly(); } }

        public ShaProvider()
        {
            this.m_hash = new List<byte>();
            this.Type = SHA_TYPE.SHA_256;
            this.Msg = new List<byte>();
        }

        public ShaProvider(SHA_TYPE type)
        {
            this.m_hash = new List<byte>();
            this.Type = type;
            this.Msg = new List<byte>();
        }

        public CRYPTO_STATUS ComputeHash()
        {
            CRYPTO_STATUS result = CRYPTO_STATUS.CRYPTO_ERROR;
            crypto_buffer_t hash = new crypto_buffer_t();
            try
            {
                int computeStatus = computeSha((int)this.Type, this.Msg.ToArray(), (uint)this.Msg.Count, out hash);
                result = (CRYPTO_STATUS)computeStatus;
            }
            catch(Exception e)
            {

            }

            if(result == CRYPTO_STATUS.CRYPTO_SUCCESS)
            {
                this.m_hash = new List<byte>(hash.buffer.Take((int)hash.bytes));
            }

            return result;
        }
    }
}
