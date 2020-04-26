using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using openssl_app.dllmanager;
using openssl_app.logmanager;

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
        public byte[] Msg { get; set; } // msg
        public byte[] PublicExponent { get; set; } // e
        public byte[] PrivateExponent { get; set; } // d
        public byte[] Modulus { get; set; } // n
        public byte[] Signature { get; set; } // s

        public RsaProvider()
        {
            this.Type =  SHA_TYPE.SHA_256;
            this.Msg = null;
            this.PublicExponent = null;
            this.PrivateExponent = null;
            this.Modulus = null;
            this.Signature = null;
        }

        public RsaProvider(SHA_TYPE shaType)
        {
            this.Type = shaType;
            this.Msg = null;
            this.PublicExponent = null;
            this.PrivateExponent = null;
            this.Modulus = null;
            this.Signature = null;
        }

        public CRYPTO_STATUS Sign()
        {
            if (this.Msg == null || this.PublicExponent == null || this.PrivateExponent == null ||
                this.Modulus == null)
            {
                return CRYPTO_STATUS.CRYPTO_NULL_PTR_ERROR;
            }

            CRYPTO_STATUS result = CRYPTO_STATUS.CRYPTO_ERROR;
            crypto_buffer_t signature = new crypto_buffer_t();
            try
            {
                int computeStatus = computeRsaSign((int)this.Type, this.Msg, (uint)this.Msg.Length,
                    this.PublicExponent, (uint)this.PublicExponent.Length,
                    this.Modulus, (uint)this.Modulus.Length,
                    this.PrivateExponent, (uint)this.PrivateExponent.Length,
                    out signature);
                result = (CRYPTO_STATUS)computeStatus;
            }
            catch (Exception exc)
            {
                LogManager.ShowMessageBox("Error", "RsaSign failed", exc.Message);
            }

            if (result == CRYPTO_STATUS.CRYPTO_SUCCESS)
            {
                this.Signature = signature.buffer.Take((int)signature.bytes).ToArray();
            }

            return result;
        }

        public CRYPTO_STATUS Verify()
        {
            if (this.Msg == null || this.PublicExponent == null || this.Modulus == null || 
                this.Signature == null)
            {
                return CRYPTO_STATUS.CRYPTO_NULL_PTR_ERROR;
            }

            CRYPTO_STATUS result = CRYPTO_STATUS.CRYPTO_ERROR;
            try
            {
                int computeStatus = computeRsaVerify((int)this.Type, this.Msg, (uint)this.Msg.Length,
                    this.PublicExponent, (uint)this.PublicExponent.Length,
                    this.Modulus, (uint)this.Modulus.Length,
                    this.Signature, (uint)this.Signature.Length);
                result = (CRYPTO_STATUS)computeStatus;
            }
            catch (Exception exc)
            {
                LogManager.ShowMessageBox("Error", "RsaVerify failed", exc.Message);
            }

            return result;
        }
    }
}
