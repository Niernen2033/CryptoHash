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

        public AesManager() : base()
        {
            this.aesProvider = new AesProvider();
        }

        public override CRYPTO_STATUS Generate(int subTarget)
        {
            if (this.HexInput)
            {
                this.aesProvider.Msg = DataConverter.BytesFromHexString(this.Msg.Text).ToList();
            }
            else
            {
                this.aesProvider.Msg = DataConverter.BytesFromString(this.Msg.Text).ToList();
            }
            this.aesProvider.Type = (AES_TYPE)this.Mode;
            CRYPTO_STATUS status = this.aesProvider.ComputeDecrypt();
            if (status == CRYPTO_STATUS.CRYPTO_SUCCESS)
            {
                this.SetResult(DataConverter.HexStringFromBytes(this.aesProvider.Hash.ToArray()));
            }
            return status;
        }
    }
}
