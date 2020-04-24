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
        public RichTextBox PublicExponent { get; set; }
        public RichTextBox PrivateExponent { get; set; }
        public RichTextBox Modulus { get; set; }
        public RichTextBox Signature { get; set; }
        public ComboBox Mode { get; set; }

        public RsaManager() : base()
        {
            this.rsaProvider = new RsaProvider();
        }

        public override CRYPTO_STATUS Generate()
        {
            if (this.HexInput)
            {
                this.rsaProvider.Msg = DataConverter.BytesFromHexString(this.Msg.Text);
                this.rsaProvider.PublicExponent = DataConverter.BytesFromHexString(this.PublicExponent.Text);
                this.rsaProvider.PrivateExponent = DataConverter.BytesFromHexString(this.PrivateExponent.Text);
                this.rsaProvider.Modulus = DataConverter.BytesFromHexString(this.Modulus.Text);
                this.rsaProvider.Signature = DataConverter.BytesFromHexString(this.Signature.Text);
            }
            else
            {
                this.rsaProvider.Msg = DataConverter.BytesFromString(this.Msg.Text);
                this.rsaProvider.PublicExponent = DataConverter.BytesFromString(this.PublicExponent.Text);
                this.rsaProvider.PrivateExponent = DataConverter.BytesFromString(this.PrivateExponent.Text);
                this.rsaProvider.Modulus = DataConverter.BytesFromString(this.Modulus.Text);
                this.rsaProvider.Signature = DataConverter.BytesFromString(this.Signature.Text);
            }
            this.rsaProvider.Type = (SHA_TYPE)this.Type;
            RSA_MODE rsaMode = (RSA_MODE)this.Mode.SelectedIndex;
            CRYPTO_STATUS status = CRYPTO_STATUS.CRYPTO_ERROR;
            if (rsaMode == RSA_MODE.RSA_VERIFY)
            {
                status = this.rsaProvider.Verify();
                if (status == CRYPTO_STATUS.CRYPTO_SUCCESS)
                {
                    this.SetResult("TRUE");
                }
                else
                {
                    this.SetResult("FALSE");
                }
            }
            else
            {
                status = this.rsaProvider.Sign();
                if (status == CRYPTO_STATUS.CRYPTO_SUCCESS)
                {
                    this.SetResult(DataConverter.HexStringFromBytes(this.rsaProvider.Signature));
                }
            }
            return status;
        }
    }
}
