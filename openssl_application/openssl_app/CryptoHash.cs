using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using openssl_app.dllmanager;
using openssl_app.algorithms;
using openssl_app.appmanager;

namespace openssl_app
{
    public partial class CryptoHash : Form
    {
        [DllImport(DllProperties.DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
        private static extern int nlogDumpAll(string filePath, uint filePathLen);

        private Manager crypto_manager;
        private ShaManager sha_manager;
        private HkdfManager hkdf_manager;
        private HmacShaManager hmac_sha_manager;
        private RsaManager rsa_manager;
        private AesManager aes_manager;
        private HmacDrbgManager hmacDrbg_manager;

        public CryptoHash()
        {
            this.InitializeComponent();
            this.InitializeManagers();
            this.LoadTypesAndModes(MANAGER_ALG_TYPE.SHA_MANAGER);
            this.crypto_manager = sha_manager;
            this.crypto_manager.Type = 1;

            this.tabControl_algorithms.SelectedIndexChanged += TabControl_algorithms_SelectedIndexChanged;
            this.checkBox_hex_input.CheckedChanged += CheckBox_hex_input_CheckedChanged;
            this.comboBox_alg_types.SelectedIndexChanged += ComboBox_alg_types_SelectedIndexChanged;
        }

        private void ComboBox_alg_types_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.crypto_manager.Type = this.comboBox_alg_types.SelectedIndex;
        }

        private void TabControl_algorithms_SelectedIndexChanged(object sender, EventArgs e)
        {
            MANAGER_ALG_TYPE algType = (MANAGER_ALG_TYPE)this.tabControl_algorithms.SelectedIndex;
            this.LoadTypesAndModes(algType);
            switch (algType)
            {
                case MANAGER_ALG_TYPE.AES_MANAGER:
                    this.crypto_manager = this.aes_manager;
                    break;
                case MANAGER_ALG_TYPE.HKDF_MANAGER:
                    this.crypto_manager = this.hkdf_manager;
                    break;
                case MANAGER_ALG_TYPE.HMAC_DRBG_MANAGER:
                    this.crypto_manager = this.hmacDrbg_manager;
                    break;
                case MANAGER_ALG_TYPE.HMAC_SHA_MANAGER:
                    this.crypto_manager = this.hmac_sha_manager;
                    break;
                case MANAGER_ALG_TYPE.RSA_MANAGER:
                    this.crypto_manager = this.rsa_manager;
                    break;
                case MANAGER_ALG_TYPE.SHA_MANAGER:
                    this.crypto_manager = this.sha_manager;
                    break;
            }
            this.crypto_manager.Type = this.comboBox_alg_types.SelectedIndex;
        }

        private void CheckBox_hex_input_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox_hex_input.Checked)
            {
                this.crypto_manager.HexInput = true;
            }
            else
            {
                this.crypto_manager.HexInput = false;
            }
        }

        private void LoadTypesAndModes(MANAGER_ALG_TYPE algType)
        {
            this.comboBox_alg_types.Items.Clear();
            this.comboBox_aes_mode.Items.Clear();
            this.comboBox_rsa_mode.Items.Clear();
            switch (algType)
            {
                case MANAGER_ALG_TYPE.AES_MANAGER:
                    this.comboBox_alg_types.Items.AddRange(Enum.GetNames(typeof(AES_TYPE)));
                    this.comboBox_alg_types.SelectedIndex = 2;
                    this.comboBox_aes_mode.Items.AddRange(Enum.GetNames(typeof(AES_MODE)));
                    this.comboBox_aes_mode.SelectedIndex = 0;
                    break;
                case MANAGER_ALG_TYPE.SHA_MANAGER:
                case MANAGER_ALG_TYPE.HKDF_MANAGER:
                case MANAGER_ALG_TYPE.HMAC_DRBG_MANAGER:
                case MANAGER_ALG_TYPE.HMAC_SHA_MANAGER:
                case MANAGER_ALG_TYPE.RSA_MANAGER:
                    this.comboBox_alg_types.Items.AddRange(Enum.GetNames(typeof(SHA_TYPE)));
                    this.comboBox_alg_types.SelectedIndex = 1;
                    if (algType == MANAGER_ALG_TYPE.RSA_MANAGER)
                    {
                        this.comboBox_rsa_mode.Items.AddRange(Enum.GetNames(typeof(RSA_MODE)));
                        this.comboBox_rsa_mode.SelectedIndex = 0;
                    }
                    break;
            }
        }

        private void InitializeManagers()
        {
            this.InitializeShaManager();
            this.InitializeHkdfManager();
            this.InitializeHmacShaManager();
            this.InitializeRsaManager();
            this.InitializeAesManager();
            this.InitializeHmacDrbgManager();
        }

        private void InitializeAesManager()
        {
            this.aes_manager = new AesManager();
            this.aes_manager.Key = this.richTextBox_aes_key;
            this.aes_manager.Iv = this.richTextBox_aes_iv;
            this.aes_manager.Msg = this.richTextBox_aes_msg;
            this.aes_manager.Mode = this.comboBox_aes_mode;
        }

        private void InitializeHmacDrbgManager()
        {
            this.hmacDrbg_manager = new HmacDrbgManager();
            this.hmacDrbg_manager.PredictionResistance = this.checkBox_hmac_drbg_preRes;
            this.hmacDrbg_manager.PersonalizationString = this.richTextBox_hmac_drbg_perStr;
            this.hmacDrbg_manager.EntropyInput = this.richTextBox_hmac_drbg_entInp;
            this.hmacDrbg_manager.Nonce = this.richTextBox_hmac_drbg_nonce;
            this.hmacDrbg_manager.EntropyInputReseed = this.richTextBox_hmac_drbg_entInpRes;
            this.hmacDrbg_manager.AdditionalInputReseed = this.richTextBox_hmac_drbg_addInpRes;
            this.hmacDrbg_manager.AdditionalInput1 = this.richTextBox_hmac_drbg_addInp1;
            this.hmacDrbg_manager.AdditionalInput2 = this.richTextBox_hmac_drbg_addInp2;
            this.hmacDrbg_manager.EntropyInputPR1 = this.richTextBox_hmac_drbg_entInpPR1;
            this.hmacDrbg_manager.EntropyInputPR2 = this.richTextBox_hmac_drbg_entInpPR2;
            this.hmacDrbg_manager.ReturnedBytes = this.numericUpDown_hmac_drbg_retByt;
        }

        private void InitializeRsaManager()
        {
            this.rsa_manager = new RsaManager();
            this.rsa_manager.PublicExponent = this.richTextBox_rsa_public_exp;
            this.rsa_manager.PrivateExponent = this.richTextBox_rsa_private_exp;
            this.rsa_manager.Msg = this.richTextBox_rsa_msg;
            this.rsa_manager.Modulus = this.richTextBox_rsa_mod;
            this.rsa_manager.Signature = this.richTextBox_rsa_sig;
            this.rsa_manager.Mode = this.comboBox_rsa_mode;
        }

        private void InitializeHmacShaManager()
        {
            this.hmac_sha_manager = new HmacShaManager();
            this.hmac_sha_manager.Key = this.richTextBox_hmac_sha_key;
            this.hmac_sha_manager.Msg = this.richTextBox_hmac_sha_msg;
        }

        private void InitializeShaManager()
        {
            this.sha_manager = new ShaManager();
            this.sha_manager.Msg = this.richTextBox_sha_msg;
        }

        private void InitializeHkdfManager()
        {
            this.hkdf_manager = new HkdfManager();
            this.hkdf_manager.Key = this.richTextBox_hkdf_key;
            this.hkdf_manager.OutputKeyBytes = this.numericUpDown_hkdf_outputKeyBytes;
            this.hkdf_manager.FixedData = this.richTextBox_hkdf_fixedData;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.crypto_manager.Generate();
            this.richTextBox_alg_result.Text = this.crypto_manager.Result;
        }
    }
}
