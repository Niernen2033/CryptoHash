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
    class HmacShaManager : Manager
    {
        private HmacShaProvider hmacShaProvider;
        public RichTextBox Msg { get; set; }
        public RichTextBox Key { get; set; }

        public HmacShaManager() : base()
        {
            this.hmacShaProvider = new HmacShaProvider();
        }

        public override CRYPTO_STATUS Generate()
        {
            if (this.HexInput)
            {
                this.hmacShaProvider.Msg = DataConverter.BytesFromHexString(this.Msg.Text);
                this.hmacShaProvider.Key = DataConverter.BytesFromHexString(this.Key.Text);
            }
            else
            {
                this.hmacShaProvider.Msg = DataConverter.BytesFromString(this.Msg.Text);
                this.hmacShaProvider.Key = DataConverter.BytesFromString(this.Key.Text);
            }
            this.hmacShaProvider.Type = (SHA_TYPE)this.Type;
            CRYPTO_STATUS status = this.hmacShaProvider.ComputeHash();
            if (status == CRYPTO_STATUS.CRYPTO_SUCCESS)
            {
                this.SetResult(DataConverter.HexStringFromBytes(this.hmacShaProvider.Hash));
            }
            return status;
        }
    }
}
