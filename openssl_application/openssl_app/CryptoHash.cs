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

        public CryptoHash()
        {
            this.InitializeComponent();
            this.InitializeManagers();
            this.LoadModes(true);
            this.crypto_manager = sha_manager;
            this.crypto_manager.Mode = 1;
            this.comboBox_alg_types.SelectedIndex = 1;

            this.tabControl_algorithms.SelectedIndexChanged += TabControl_algorithms_SelectedIndexChanged;
            this.checkBox_hex_input.CheckedChanged += CheckBox_hex_input_CheckedChanged;
            this.comboBox_alg_types.SelectedIndexChanged += ComboBox_alg_types_SelectedIndexChanged;
        }

        private void ComboBox_alg_types_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.crypto_manager.Mode = this.comboBox_alg_types.SelectedIndex;
        }

        private void TabControl_algorithms_SelectedIndexChanged(object sender, EventArgs e)
        {
            MANAGER_ALG_TYPE algType = (MANAGER_ALG_TYPE)this.tabControl_algorithms.SelectedIndex;
            switch (algType)
            {
                case MANAGER_ALG_TYPE.AES_MANAGER:
                    this.LoadModes(false);
                    this.comboBox_alg_types.SelectedIndex = 2;
                    break;
                case MANAGER_ALG_TYPE.HKDF_MANAGER:
                    this.crypto_manager = this.hkdf_manager;
                    this.LoadModes(true);
                    this.comboBox_alg_types.SelectedIndex = 1;
                    break;
                case MANAGER_ALG_TYPE.HMAC_DRBG_MANAGER:
                    this.LoadModes(true);
                    this.comboBox_alg_types.SelectedIndex = 1;
                    break;
                case MANAGER_ALG_TYPE.HMAC_SHA_MANAGER:
                    this.crypto_manager = this.hmac_sha_manager;
                    this.LoadModes(true);
                    this.comboBox_alg_types.SelectedIndex = 1;
                    break;
                case MANAGER_ALG_TYPE.RSA_MANAGER:
                    this.crypto_manager = this.rsa_manager;
                    this.LoadModes(true);
                    this.comboBox_alg_types.SelectedIndex = 1;
                    break;
                case MANAGER_ALG_TYPE.SHA_MANAGER:
                    this.crypto_manager = this.sha_manager;
                    this.LoadModes(true);
                    this.comboBox_alg_types.SelectedIndex = 1;
                    break;
            }
            this.crypto_manager.Mode = this.comboBox_alg_types.SelectedIndex;
        }

        private void CheckBox_hex_input_CheckedChanged(object sender, EventArgs e)
        {
            if(this.checkBox_hex_input.Checked)
            {
                this.crypto_manager.HexInput = true;
            }
            else
            {
                this.crypto_manager.HexInput = false;
            }
        }

        private void LoadModes(bool shaModes)
        {
            string[] modes = null;
            if (shaModes == true)
            {
                modes = Enum.GetNames(typeof(SHA_TYPE));
            }
            else
            {
                modes = Enum.GetNames(typeof(AES_TYPE));
            }

            this.comboBox_alg_types.Items.Clear();
            for (int i = 0; i < modes.Length; i++)
            {
                this.comboBox_alg_types.Items.Add(modes[i]);
            }
        }

        private void InitializeManagers()
        {
            this.InitializeShaManager();
            this.InitializeHkdfManager();
            this.InitializeHmacShaManager();
            this.InitializeRsaManager();
        }

        private void InitializeRsaManager()
        {
            this.rsa_manager = new RsaManager();
            this.rsa_manager.Exponent = this.richTextBox_rsa_exp;
            this.rsa_manager.Msg = this.richTextBox_rsa_msg;
            this.rsa_manager.Modulus = this.richTextBox_rsa_mod;
            this.rsa_manager.Signature = this.richTextBox_rsa_sig;
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
