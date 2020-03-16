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
        public RichTextBox Hash { get; set; }
        public ComboBox Type { get; set; }

        public ShaManager() : base(MANAGER_ALG_TYPE.SHA_MANAGER)
        {
            this.shaProvider = new ShaProvider();
        }

        public override CRYPTO_STATUS Generate(int subTarget)
        {
            this.shaProvider.Msg = DataConverter.BytesFromString(this.Msg.Text).ToList();
            this.shaProvider.Type = (SHA_TYPE)this.Type.SelectedIndex;
            CRYPTO_STATUS status = this.shaProvider.ComputeHash();
            if(status == CRYPTO_STATUS.CRYPTO_SUCCESS)
            {
                this.Hash.Text = DataConverter.HexStringFromBytes(this.shaProvider.Hash.ToArray());
            }
            return status;
        }
    }
}
