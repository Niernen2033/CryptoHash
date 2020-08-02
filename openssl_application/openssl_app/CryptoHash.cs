using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using openssl_app.dllmanager;
using openssl_app.algorithms;
using openssl_app.appmanager;
using openssl_app.logmanager;

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
            this.InitializeDebugComponents();
            this.InitializeManagers();
            this.LoadTypesAndModes(MANAGER_ALG_TYPE.SHA_MANAGER);
            this.crypto_manager = sha_manager;
            this.crypto_manager.Type = 1;

            this.tabControl_algorithms.SelectedIndexChanged += TabControl_algorithms_SelectedIndexChanged;
            this.checkBox_hex_input.CheckedChanged += CheckBox_hex_input_CheckedChanged;
            this.comboBox_alg_types.SelectedIndexChanged += ComboBox_alg_types_SelectedIndexChanged;
        }

        private void FormFile_DragDrop(object sender, DragEventArgs e)
        {
            string[] allfilesNames = e.Data.GetData(DataFormats.FileDrop) as string[];
            if(allfilesNames == null || allfilesNames.Length > 1)
            {
                return;
            }
            string filePath = allfilesNames[0];

            byte[] fileBytes = this.LoadFileBytes(filePath);
            if(fileBytes != null)
            {
                RichTextBox tmpData = sender as RichTextBox;
                this.crypto_manager.HexInput = true;
                tmpData.Text = DataConverter.HexStringFromBytes(fileBytes);
            }
        }

        private void InitializeDebugComponents()
        {
            this.comboBox_nlog_id.Items.AddRange(Enum.GetNames(typeof(NLOG_ID)));
            this.comboBox_nlog_id.SelectedIndex = 3;
#if !DEBUG
            this.groupBox_nlog_debug.Visible = false;
#endif // !DEBUG
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

            this.richTextBox_aes_msg.AllowDrop = true;
            this.richTextBox_aes_msg.DragDrop += FormFile_DragDrop;
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

            this.richTextBox_hmac_drbg_entInpRes.AllowDrop = true;
            this.richTextBox_hmac_drbg_entInpRes.DragDrop += FormFile_DragDrop;
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

            this.richTextBox_rsa_msg.AllowDrop = true;
            this.richTextBox_rsa_mod.AllowDrop = true;
            this.richTextBox_rsa_sig.AllowDrop = true;
            this.richTextBox_rsa_msg.DragDrop += FormFile_DragDrop;
            this.richTextBox_rsa_mod.DragDrop += FormFile_DragDrop;
            this.richTextBox_rsa_sig.DragDrop += FormFile_DragDrop;
        }

        private void InitializeHmacShaManager()
        {
            this.hmac_sha_manager = new HmacShaManager();
            this.hmac_sha_manager.Key = this.richTextBox_hmac_sha_key;
            this.hmac_sha_manager.Msg = this.richTextBox_hmac_sha_msg;

            this.richTextBox_hmac_sha_msg.AllowDrop = true;
            this.richTextBox_hmac_sha_msg.DragDrop += FormFile_DragDrop;
        }

        private void InitializeShaManager()
        {
            this.sha_manager = new ShaManager();
            this.sha_manager.Msg = this.richTextBox_sha_msg;

            this.richTextBox_sha_msg.AllowDrop = true;
            this.richTextBox_sha_msg.DragDrop += FormFile_DragDrop;
        }

        private void InitializeHkdfManager()
        {
            this.hkdf_manager = new HkdfManager();
            this.hkdf_manager.Key = this.richTextBox_hkdf_key;
            this.hkdf_manager.OutputKeyBytes = this.numericUpDown_hkdf_outputKeyBytes;
            this.hkdf_manager.FixedData = this.richTextBox_hkdf_fixedData;

            this.richTextBox_hkdf_fixedData.AllowDrop = true;
            this.richTextBox_hkdf_fixedData.DragDrop += FormFile_DragDrop;
        }

        private void button_calculate_Click(object sender, EventArgs e)
        {
            this.button_calculate.Enabled = false;
            this.crypto_manager.Generate();
            this.richTextBox_alg_result.Text = this.crypto_manager.Result;
            this.button_calculate.Enabled = true;
        }

        private void button_nlog_dump_Click(object sender, EventArgs e)
        {
#if DEBUG
            bool dumpStatus = false;
            DialogResult dialogResult = DialogResult.None;
            string filePath = string.Empty;

            if (this.checkBox_nlog_default_path.Checked == false)
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    dialogResult = openFileDialog.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        filePath = openFileDialog.FileName;
                        if (!File.Exists(filePath))
                        {
                            LogManager.ShowMessageBox("Error", "File doesnt exist");
                            return;
                        }
                    }
                }
            }
            else
            {
                filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/nlog.txt";
            }

            try
            {
                NLOG_ID nlogId = (NLOG_ID)(this.comboBox_nlog_id.SelectedIndex + 1);
                dumpStatus = DllNLogManager.DllNlogDump(nlogId, filePath);
            }
            catch (Exception exc)
            {
                LogManager.ShowMessageBox("Error", "DllNlogDump failed", exc.Message);
                return;
            }

            if ((dumpStatus == false) && (dialogResult == DialogResult.OK))
            {
                LogManager.ShowMessageBox("Error", "DllNlogDump failed");
            }

#endif // DEBUG
        }

        private byte[] LoadFileBytes(string filePath)
        {
            if (!File.Exists(filePath))
            {
                LogManager.ShowMessageBox("Error", "File doesnt exist");
                return null;
            }

            byte[] result = null;
            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
                {
                    result = reader.ReadBytes((int)reader.BaseStream.Length);
                }
            }
            catch (Exception exc)
            {
                LogManager.ShowMessageBox("Error", "File doesnt exist", exc.Message);
                return null;
            }

            return result;
        }

        private void button_load_file_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            using(OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                DialogResult dialogResult = openFileDialog.ShowDialog();
                if(dialogResult == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                }
                else
                {
                    return;
                }
            }

            byte[] fileBytes = this.LoadFileBytes(filePath);
            if (fileBytes != null)
            {
                Clipboard.SetText(DataConverter.HexStringFromBytes(fileBytes));
            }
        }

    }
}
