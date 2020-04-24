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
    class AesManager : Manager
    {
        private AesProvider aesProvider;
        public RichTextBox Msg { get; set; }
        public RichTextBox Key { get; set; }
        public RichTextBox Iv { get; set; }
        public ComboBox Mode { get; set; }

        public AesManager() : base()
        {
            this.aesProvider = new AesProvider();
        }

        public override CRYPTO_STATUS Generate()
        {
            if (this.HexInput)
            {
                this.aesProvider.Msg = DataConverter.BytesFromHexString(this.Msg.Text);
                this.aesProvider.Key = DataConverter.BytesFromHexString(this.Key.Text);
                this.aesProvider.Iv = DataConverter.BytesFromHexString(this.Iv.Text);
            }
            else
            {
                this.aesProvider.Msg = DataConverter.BytesFromString(this.Msg.Text);
                this.aesProvider.Key = DataConverter.BytesFromString(this.Key.Text);
                this.aesProvider.Iv = DataConverter.BytesFromString(this.Iv.Text);
            }
            this.aesProvider.Type = (AES_TYPE)this.Type;
            AES_MODE aesMode = (AES_MODE)this.Mode.SelectedIndex;
            CRYPTO_STATUS status = CRYPTO_STATUS.CRYPTO_ERROR;

            switch(aesMode)
            {
                case AES_MODE.AES_DECRYPT:
                    status = this.aesProvider.ComputeDecrypt();
                    break;
                case AES_MODE.AES_ENCRYPT:
                    status = this.aesProvider.ComputeEncrypt();
                    break;
                case AES_MODE.AES_KEY_UNWRAP:
                    status = this.aesProvider.ComputeKeyUnwrap();
                    break;
                case AES_MODE.AES_KEY_WRAP:
                    status = this.aesProvider.ComputeKeyWrap();
                    break;
            }


            if (status == CRYPTO_STATUS.CRYPTO_SUCCESS)
            {
                this.SetResult(DataConverter.HexStringFromBytes(this.aesProvider.Hash));
            }
            return status;
        }
    }
}
