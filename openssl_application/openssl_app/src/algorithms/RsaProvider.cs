using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using openssl_app.dllmanager;

namespace openssl_app.algorithms
{
    class RsaProvider
    {
        [DllImport(DllProperties.DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
        private static extern int computeRsaVerify(int shaType, 
            byte[] msg, uint msgBytes,
            byte[] e, uint eBytes,
            byte[] n, uint nBytes,
            byte[] s, uint sBytes);

        public SHA_TYPE Type { get; set; }
        public List<byte> Msg { get; set; }
        public List<byte> Exponent { get; set; }
        public List<byte> Modulus { get; set; }
        public List<byte> Signature { get; set; }

        public RsaProvider()
        {
            this.Type =  SHA_TYPE.SHA_256;
            this.Msg = new List<byte>();
            this.Exponent = new List<byte>();
            this.Modulus = new List<byte>();
            this.Signature = new List<byte>();
        }

        public RsaProvider(SHA_TYPE shaType)
        {
            this.Type = shaType;
            this.Msg = new List<byte>();
            this.Exponent = new List<byte>();
            this.Modulus = new List<byte>();
            this.Signature = new List<byte>();
        }

        public CRYPTO_STATUS Verify()
        {
            CRYPTO_STATUS result = CRYPTO_STATUS.CRYPTO_ERROR;
            try
            {
                int computeStatus = computeRsaVerify((int)this.Type, this.Msg.ToArray(), (uint)this.Msg.Count,
                    this.Exponent.ToArray(), (uint)this.Exponent.Count,
                    this.Modulus.ToArray(), (uint)this.Modulus.Count,
                    this.Signature.ToArray(), (uint)this.Signature.Count);
                result = (CRYPTO_STATUS)computeStatus;
            }
            catch (Exception e)
            {

            }

            return result;
        }
    }
}
