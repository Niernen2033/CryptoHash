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
    class ShaManager : Manager
    {
        private ShaProvider shaProvider;
        public RichTextBox Msg { get; set; }

        public ShaManager() : base()
        {
            this.shaProvider = new ShaProvider();
        }

        public override CRYPTO_STATUS Generate()
        {
            if(this.HexInput)
            {
                this.shaProvider.Msg = DataConverter.BytesFromHexString(this.Msg.Text).ToList();
            }
            else
            {
                this.shaProvider.Msg = DataConverter.BytesFromString(this.Msg.Text).ToList();
            }
            this.shaProvider.Type = (SHA_TYPE)this.Type;
            CRYPTO_STATUS status = this.shaProvider.ComputeHash();
            if(status == CRYPTO_STATUS.CRYPTO_SUCCESS)
            {
                this.SetResult(DataConverter.HexStringFromBytes(this.shaProvider.Hash.ToArray()));
            }
            return status;
        }
    }
}
