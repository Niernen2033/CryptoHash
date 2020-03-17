using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using openssl_app.dllmanager;
using openssl_app.algorithms;

namespace openssl_app.appmanager
{
    class RsaManager : Manager
    {
        private RsaProvider rsaProvider;
        public RichTextBox Msg { get; set; }
        public RichTextBox Exponent { get; set; }
        public RichTextBox Modulus { get; set; }
        public RichTextBox Signature { get; set; }

        public RsaManager() : base()
        {
            this.rsaProvider = new RsaProvider();
        }

        public override CRYPTO_STATUS Generate()
        {
            if (this.HexInput)
            {
                this.rsaProvider.Msg = DataConverter.BytesFromHexString(this.Msg.Text).ToList();
                this.rsaProvider.Exponent = DataConverter.BytesFromHexString(this.Exponent.Text).ToList();
                this.rsaProvider.Modulus = DataConverter.BytesFromHexString(this.Modulus.Text).ToList();
                this.rsaProvider.Signature = DataConverter.BytesFromHexString(this.Signature.Text).ToList();
            }
            else
            {
                this.rsaProvider.Msg = DataConverter.BytesFromString(this.Msg.Text).ToList();
                this.rsaProvider.Exponent = DataConverter.BytesFromString(this.Exponent.Text).ToList();
                this.rsaProvider.Modulus = DataConverter.BytesFromString(this.Modulus.Text).ToList();
                this.rsaProvider.Signature = DataConverter.BytesFromString(this.Signature.Text).ToList();
            }
            this.rsaProvider.Type = (SHA_TYPE)this.Type;
            CRYPTO_STATUS status = this.rsaProvider.Verify();
            if (status == CRYPTO_STATUS.CRYPTO_SUCCESS)
            {
                this.SetResult("TRUE");
            }
            else
            {
                this.SetResult("FALSE");
            }
            return status;
        }
    }
}
