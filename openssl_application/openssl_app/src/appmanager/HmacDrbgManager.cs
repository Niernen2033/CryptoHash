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
    class HmacDrbgManager : Manager
    {
        private HmacDrbgProvider hmacDrbgProvider;
        public CheckBox PredictionResistance { get; set; }
        public RichTextBox PersonalizationString { get; set; }
        public RichTextBox EntropyInput { get; set; }
        public RichTextBox Nonce { get; set; }
        public RichTextBox EntropyInputReseed { get; set; }
        public RichTextBox AdditionalInputReseed { get; set; }
        public RichTextBox AdditionalInput1 { get; set; }
        public RichTextBox AdditionalInput2 { get; set; }
        public RichTextBox EntropyInputPR1 { get; set; }
        public RichTextBox EntropyInputPR2 { get; set; }
        public NumericUpDown ReturnedBytes { get; set; }

        public HmacDrbgManager() : base()
        {
            this.hmacDrbgProvider = new HmacDrbgProvider();
        }

        public override CRYPTO_STATUS Generate()
        {
            if (this.HexInput)
            {
                this.hmacDrbgProvider.PredictionResistance = this.PredictionResistance.Checked;
                this.hmacDrbgProvider.PersonalizationString = DataConverter.BytesFromHexString(this.PersonalizationString.Text).ToList();
                this.hmacDrbgProvider.EntropyInput = DataConverter.BytesFromHexString(this.EntropyInput.Text).ToList();
                this.hmacDrbgProvider.Nonce = DataConverter.BytesFromHexString(this.Nonce.Text).ToList();
                this.hmacDrbgProvider.EntropyInputReseed = DataConverter.BytesFromHexString(this.EntropyInputReseed.Text).ToList();
                this.hmacDrbgProvider.AdditionalInputReseed = DataConverter.BytesFromHexString(this.AdditionalInputReseed.Text).ToList();
                this.hmacDrbgProvider.AdditionalInput1 = DataConverter.BytesFromHexString(this.AdditionalInput1.Text).ToList();
                this.hmacDrbgProvider.AdditionalInput2 = DataConverter.BytesFromHexString(this.AdditionalInput2.Text).ToList();
                this.hmacDrbgProvider.EntropyInputPR1 = DataConverter.BytesFromHexString(this.EntropyInputPR1.Text).ToList();
                this.hmacDrbgProvider.EntropyInputPR2 = DataConverter.BytesFromHexString(this.EntropyInputPR2.Text).ToList();
                this.hmacDrbgProvider.ReturnedBytes = (uint)this.ReturnedBytes.Value;
            }
            else
            {
                this.hmacDrbgProvider.PredictionResistance = this.PredictionResistance.Checked;
                this.hmacDrbgProvider.PersonalizationString = DataConverter.BytesFromString(this.PersonalizationString.Text).ToList();
                this.hmacDrbgProvider.EntropyInput = DataConverter.BytesFromString(this.EntropyInput.Text).ToList();
                this.hmacDrbgProvider.Nonce = DataConverter.BytesFromString(this.Nonce.Text).ToList();
                this.hmacDrbgProvider.EntropyInputReseed = DataConverter.BytesFromString(this.EntropyInputReseed.Text).ToList();
                this.hmacDrbgProvider.AdditionalInputReseed = DataConverter.BytesFromString(this.AdditionalInputReseed.Text).ToList();
                this.hmacDrbgProvider.AdditionalInput1 = DataConverter.BytesFromString(this.AdditionalInput1.Text).ToList();
                this.hmacDrbgProvider.AdditionalInput2 = DataConverter.BytesFromString(this.AdditionalInput2.Text).ToList();
                this.hmacDrbgProvider.EntropyInputPR1 = DataConverter.BytesFromString(this.EntropyInputPR1.Text).ToList();
                this.hmacDrbgProvider.EntropyInputPR2 = DataConverter.BytesFromString(this.EntropyInputPR2.Text).ToList();
                this.hmacDrbgProvider.ReturnedBytes = (uint)this.ReturnedBytes.Value;
            }
            this.hmacDrbgProvider.Type = (SHA_TYPE)this.Type;
            CRYPTO_STATUS status = this.hmacDrbgProvider.GenerateRandomData();
            if (status == CRYPTO_STATUS.CRYPTO_SUCCESS)
            {
                this.SetResult(DataConverter.HexStringFromBytes(this.hmacDrbgProvider.RandomData.ToArray()));
            }
            return status;
        }
    }
}
