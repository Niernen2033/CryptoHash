using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
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

        [DllImport(DllProperties.DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
        private static extern int computeRsaSign(int shaType,
            byte[] msg, uint msgBytes,
            byte[] e, uint eBytes,
            byte[] n, uint nBytes,
            byte[] d, uint dBytes,
            out crypto_buffer_t digset);

        public SHA_TYPE Type { get; set; }
        public List<byte> Msg { get; set; } // msg
        public List<byte> PublicExponent { get; set; } // e
        public List<byte> PrivateExponent { get; set; } // d
        public List<byte> Modulus { get; set; } // n
        public List<byte> Signature { get; set; } // s

        public RsaProvider()
        {
            this.Type =  SHA_TYPE.SHA_256;
            this.Msg = new List<byte>();
            this.PublicExponent = new List<byte>();
            this.PrivateExponent = new List<byte>();
            this.Modulus = new List<byte>();
            this.Signature = new List<byte>();
        }

        public RsaProvider(SHA_TYPE shaType)
        {
            this.Type = shaType;
            this.Msg = new List<byte>();
            this.PublicExponent = new List<byte>();
            this.PrivateExponent = new List<byte>();
            this.Modulus = new List<byte>();
            this.Signature = new List<byte>();
        }

        public CRYPTO_STATUS Sign()
        {
            CRYPTO_STATUS result = CRYPTO_STATUS.CRYPTO_ERROR;
            crypto_buffer_t signature = new crypto_buffer_t();
            try
            {
                int computeStatus = computeRsaSign((int)this.Type, this.Msg.ToArray(), (uint)this.Msg.Count,
                    this.PublicExponent.ToArray(), (uint)this.PublicExponent.Count,
                    this.Modulus.ToArray(), (uint)this.Modulus.Count,
                    this.PrivateExponent.ToArray(), (uint)this.PrivateExponent.Count,
                    out signature);
                result = (CRYPTO_STATUS)computeStatus;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            if (result == CRYPTO_STATUS.CRYPTO_SUCCESS)
            {
                this.Signature = new List<byte>(signature.buffer.Take((int)signature.bytes));
            }

            return result;
        }

        public CRYPTO_STATUS Verify()
        {
            CRYPTO_STATUS result = CRYPTO_STATUS.CRYPTO_ERROR;
            try
            {
                int computeStatus = computeRsaVerify((int)this.Type, this.Msg.ToArray(), (uint)this.Msg.Count,
                    this.PublicExponent.ToArray(), (uint)this.PublicExponent.Count,
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
