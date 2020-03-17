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
    class HkdfManager : Manager
    {
        private HkdfProvider hkdfProvider;
        public RichTextBox Key { get; set; }
        public RichTextBox FixedData { get; set; }
        public NumericUpDown OutputKeyBytes { get; set; }

        public HkdfManager() : base()
        {
            this.hkdfProvider = new HkdfProvider();
        }

        public override CRYPTO_STATUS Generate()
        {
            if(this.HexInput)
            {
                this.hkdfProvider.FixedData = DataConverter.BytesFromHexString(this.FixedData.Text).ToList();
                this.hkdfProvider.Key = DataConverter.BytesFromHexString(this.Key.Text).ToList();
            }
            else
            {
                this.hkdfProvider.FixedData = DataConverter.BytesFromString(this.FixedData.Text).ToList();
                this.hkdfProvider.Key = DataConverter.BytesFromString(this.Key.Text).ToList();
            }
            this.hkdfProvider.OutputKeyBytes = (uint)this.OutputKeyBytes.Value;
            this.hkdfProvider.Type = (SHA_TYPE)this.Type;
            CRYPTO_STATUS status = this.hkdfProvider.ComputeHash();
            if (status == CRYPTO_STATUS.CRYPTO_SUCCESS)
            {
                this.SetResult(DataConverter.HexStringFromBytes(this.hkdfProvider.Hash.ToArray()));
            }
            return status;
        }
    }
}
