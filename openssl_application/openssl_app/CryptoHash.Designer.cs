﻿namespace openssl_app
{
    partial class CryptoHash
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl_algorithms = new System.Windows.Forms.TabControl();
            this.tabPage_sha = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBox_sha_msg = new System.Windows.Forms.RichTextBox();
            this.tabPage_hkdf = new System.Windows.Forms.TabPage();
            this.richTextBox_hkdf_key = new System.Windows.Forms.RichTextBox();
            this.richTextBox_hkdf_fixedData = new System.Windows.Forms.RichTextBox();
            this.numericUpDown_hkdf_outputKeyBytes = new System.Windows.Forms.NumericUpDown();
            this.tabPage_hmac_sha = new System.Windows.Forms.TabPage();
            this.richTextBox_hmac_sha_key = new System.Windows.Forms.RichTextBox();
            this.richTextBox_hmac_sha_msg = new System.Windows.Forms.RichTextBox();
            this.tabPage_rsa = new System.Windows.Forms.TabPage();
            this.richTextBox_rsa_sig = new System.Windows.Forms.RichTextBox();
            this.richTextBox_rsa_mod = new System.Windows.Forms.RichTextBox();
            this.richTextBox_rsa_exp = new System.Windows.Forms.RichTextBox();
            this.richTextBox_rsa_msg = new System.Windows.Forms.RichTextBox();
            this.tabPage_aes = new System.Windows.Forms.TabPage();
            this.tabPage_hmac_drbg = new System.Windows.Forms.TabPage();
            this.checkBox_hex_input = new System.Windows.Forms.CheckBox();
            this.comboBox_alg_types = new System.Windows.Forms.ComboBox();
            this.richTextBox_alg_result = new System.Windows.Forms.RichTextBox();
            this.richTextBox_aes_msg = new System.Windows.Forms.RichTextBox();
            this.richTextBox_aes_key = new System.Windows.Forms.RichTextBox();
            this.richTextBox_aes_iv = new System.Windows.Forms.RichTextBox();
            this.comboBox_aes_mode = new System.Windows.Forms.ComboBox();
            this.numericUpDown_hmac_drbg_retByt = new System.Windows.Forms.NumericUpDown();
            this.checkBox_hmac_drbg_preRes = new System.Windows.Forms.CheckBox();
            this.richTextBox_hmac_drbg_perStr = new System.Windows.Forms.RichTextBox();
            this.richTextBox_hmac_drbg_entInp = new System.Windows.Forms.RichTextBox();
            this.richTextBox_hmac_drbg_nonce = new System.Windows.Forms.RichTextBox();
            this.richTextBox_hmac_drbg_addInpRes = new System.Windows.Forms.RichTextBox();
            this.richTextBox_hmac_drbg_entInpRes = new System.Windows.Forms.RichTextBox();
            this.richTextBox_hmac_drbg_addInp2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox_hmac_drbg_addInp1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox_hmac_drbg_entInpPR2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox_hmac_drbg_entInpPR1 = new System.Windows.Forms.RichTextBox();
            this.tabControl_algorithms.SuspendLayout();
            this.tabPage_sha.SuspendLayout();
            this.tabPage_hkdf.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_hkdf_outputKeyBytes)).BeginInit();
            this.tabPage_hmac_sha.SuspendLayout();
            this.tabPage_rsa.SuspendLayout();
            this.tabPage_aes.SuspendLayout();
            this.tabPage_hmac_drbg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_hmac_drbg_retByt)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(738, 446);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(159, 33);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl_algorithms
            // 
            this.tabControl_algorithms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl_algorithms.Controls.Add(this.tabPage_sha);
            this.tabControl_algorithms.Controls.Add(this.tabPage_hkdf);
            this.tabControl_algorithms.Controls.Add(this.tabPage_hmac_sha);
            this.tabControl_algorithms.Controls.Add(this.tabPage_rsa);
            this.tabControl_algorithms.Controls.Add(this.tabPage_aes);
            this.tabControl_algorithms.Controls.Add(this.tabPage_hmac_drbg);
            this.tabControl_algorithms.Location = new System.Drawing.Point(12, 9);
            this.tabControl_algorithms.Name = "tabControl_algorithms";
            this.tabControl_algorithms.SelectedIndex = 0;
            this.tabControl_algorithms.Size = new System.Drawing.Size(893, 310);
            this.tabControl_algorithms.TabIndex = 3;
            // 
            // tabPage_sha
            // 
            this.tabPage_sha.Controls.Add(this.label2);
            this.tabPage_sha.Controls.Add(this.richTextBox_sha_msg);
            this.tabPage_sha.Location = new System.Drawing.Point(4, 22);
            this.tabPage_sha.Name = "tabPage_sha";
            this.tabPage_sha.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_sha.Size = new System.Drawing.Size(885, 284);
            this.tabPage_sha.TabIndex = 0;
            this.tabPage_sha.Text = "Sha";
            this.tabPage_sha.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Msg:";
            // 
            // richTextBox_sha_msg
            // 
            this.richTextBox_sha_msg.Location = new System.Drawing.Point(39, 33);
            this.richTextBox_sha_msg.Name = "richTextBox_sha_msg";
            this.richTextBox_sha_msg.Size = new System.Drawing.Size(723, 113);
            this.richTextBox_sha_msg.TabIndex = 1;
            this.richTextBox_sha_msg.Text = "";
            // 
            // tabPage_hkdf
            // 
            this.tabPage_hkdf.Controls.Add(this.richTextBox_hkdf_key);
            this.tabPage_hkdf.Controls.Add(this.richTextBox_hkdf_fixedData);
            this.tabPage_hkdf.Controls.Add(this.numericUpDown_hkdf_outputKeyBytes);
            this.tabPage_hkdf.Location = new System.Drawing.Point(4, 22);
            this.tabPage_hkdf.Name = "tabPage_hkdf";
            this.tabPage_hkdf.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_hkdf.Size = new System.Drawing.Size(885, 284);
            this.tabPage_hkdf.TabIndex = 1;
            this.tabPage_hkdf.Text = "Hkdf";
            this.tabPage_hkdf.UseVisualStyleBackColor = true;
            // 
            // richTextBox_hkdf_key
            // 
            this.richTextBox_hkdf_key.Location = new System.Drawing.Point(76, 119);
            this.richTextBox_hkdf_key.Name = "richTextBox_hkdf_key";
            this.richTextBox_hkdf_key.Size = new System.Drawing.Size(685, 99);
            this.richTextBox_hkdf_key.TabIndex = 9;
            this.richTextBox_hkdf_key.Text = "";
            // 
            // richTextBox_hkdf_fixedData
            // 
            this.richTextBox_hkdf_fixedData.Location = new System.Drawing.Point(76, 45);
            this.richTextBox_hkdf_fixedData.Name = "richTextBox_hkdf_fixedData";
            this.richTextBox_hkdf_fixedData.Size = new System.Drawing.Size(685, 68);
            this.richTextBox_hkdf_fixedData.TabIndex = 8;
            this.richTextBox_hkdf_fixedData.Text = "";
            // 
            // numericUpDown_hkdf_outputKeyBytes
            // 
            this.numericUpDown_hkdf_outputKeyBytes.Location = new System.Drawing.Point(286, 18);
            this.numericUpDown_hkdf_outputKeyBytes.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numericUpDown_hkdf_outputKeyBytes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_hkdf_outputKeyBytes.Name = "numericUpDown_hkdf_outputKeyBytes";
            this.numericUpDown_hkdf_outputKeyBytes.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown_hkdf_outputKeyBytes.TabIndex = 0;
            this.numericUpDown_hkdf_outputKeyBytes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // tabPage_hmac_sha
            // 
            this.tabPage_hmac_sha.Controls.Add(this.richTextBox_hmac_sha_key);
            this.tabPage_hmac_sha.Controls.Add(this.richTextBox_hmac_sha_msg);
            this.tabPage_hmac_sha.Location = new System.Drawing.Point(4, 22);
            this.tabPage_hmac_sha.Name = "tabPage_hmac_sha";
            this.tabPage_hmac_sha.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_hmac_sha.Size = new System.Drawing.Size(885, 284);
            this.tabPage_hmac_sha.TabIndex = 2;
            this.tabPage_hmac_sha.Text = "Hmac_Sha";
            this.tabPage_hmac_sha.UseVisualStyleBackColor = true;
            // 
            // richTextBox_hmac_sha_key
            // 
            this.richTextBox_hmac_sha_key.Location = new System.Drawing.Point(89, 140);
            this.richTextBox_hmac_sha_key.Name = "richTextBox_hmac_sha_key";
            this.richTextBox_hmac_sha_key.Size = new System.Drawing.Size(650, 96);
            this.richTextBox_hmac_sha_key.TabIndex = 1;
            this.richTextBox_hmac_sha_key.Text = "";
            // 
            // richTextBox_hmac_sha_msg
            // 
            this.richTextBox_hmac_sha_msg.Location = new System.Drawing.Point(89, 16);
            this.richTextBox_hmac_sha_msg.Name = "richTextBox_hmac_sha_msg";
            this.richTextBox_hmac_sha_msg.Size = new System.Drawing.Size(650, 96);
            this.richTextBox_hmac_sha_msg.TabIndex = 0;
            this.richTextBox_hmac_sha_msg.Text = "";
            // 
            // tabPage_rsa
            // 
            this.tabPage_rsa.Controls.Add(this.richTextBox_rsa_sig);
            this.tabPage_rsa.Controls.Add(this.richTextBox_rsa_mod);
            this.tabPage_rsa.Controls.Add(this.richTextBox_rsa_exp);
            this.tabPage_rsa.Controls.Add(this.richTextBox_rsa_msg);
            this.tabPage_rsa.Location = new System.Drawing.Point(4, 22);
            this.tabPage_rsa.Name = "tabPage_rsa";
            this.tabPage_rsa.Size = new System.Drawing.Size(885, 284);
            this.tabPage_rsa.TabIndex = 3;
            this.tabPage_rsa.Text = "Rsa";
            this.tabPage_rsa.UseVisualStyleBackColor = true;
            // 
            // richTextBox_rsa_sig
            // 
            this.richTextBox_rsa_sig.Location = new System.Drawing.Point(43, 207);
            this.richTextBox_rsa_sig.Name = "richTextBox_rsa_sig";
            this.richTextBox_rsa_sig.Size = new System.Drawing.Size(784, 55);
            this.richTextBox_rsa_sig.TabIndex = 3;
            this.richTextBox_rsa_sig.Text = "";
            // 
            // richTextBox_rsa_mod
            // 
            this.richTextBox_rsa_mod.Location = new System.Drawing.Point(43, 142);
            this.richTextBox_rsa_mod.Name = "richTextBox_rsa_mod";
            this.richTextBox_rsa_mod.Size = new System.Drawing.Size(784, 58);
            this.richTextBox_rsa_mod.TabIndex = 2;
            this.richTextBox_rsa_mod.Text = "";
            // 
            // richTextBox_rsa_exp
            // 
            this.richTextBox_rsa_exp.Location = new System.Drawing.Point(43, 75);
            this.richTextBox_rsa_exp.Name = "richTextBox_rsa_exp";
            this.richTextBox_rsa_exp.Size = new System.Drawing.Size(784, 60);
            this.richTextBox_rsa_exp.TabIndex = 1;
            this.richTextBox_rsa_exp.Text = "";
            // 
            // richTextBox_rsa_msg
            // 
            this.richTextBox_rsa_msg.Location = new System.Drawing.Point(43, 3);
            this.richTextBox_rsa_msg.Name = "richTextBox_rsa_msg";
            this.richTextBox_rsa_msg.Size = new System.Drawing.Size(784, 66);
            this.richTextBox_rsa_msg.TabIndex = 0;
            this.richTextBox_rsa_msg.Text = "";
            // 
            // tabPage_aes
            // 
            this.tabPage_aes.Controls.Add(this.comboBox_aes_mode);
            this.tabPage_aes.Controls.Add(this.richTextBox_aes_iv);
            this.tabPage_aes.Controls.Add(this.richTextBox_aes_key);
            this.tabPage_aes.Controls.Add(this.richTextBox_aes_msg);
            this.tabPage_aes.Location = new System.Drawing.Point(4, 22);
            this.tabPage_aes.Name = "tabPage_aes";
            this.tabPage_aes.Size = new System.Drawing.Size(885, 284);
            this.tabPage_aes.TabIndex = 4;
            this.tabPage_aes.Text = "Aes";
            this.tabPage_aes.UseVisualStyleBackColor = true;
            // 
            // tabPage_hmac_drbg
            // 
            this.tabPage_hmac_drbg.Controls.Add(this.richTextBox_hmac_drbg_entInpPR2);
            this.tabPage_hmac_drbg.Controls.Add(this.richTextBox_hmac_drbg_entInpPR1);
            this.tabPage_hmac_drbg.Controls.Add(this.richTextBox_hmac_drbg_addInp2);
            this.tabPage_hmac_drbg.Controls.Add(this.richTextBox_hmac_drbg_addInp1);
            this.tabPage_hmac_drbg.Controls.Add(this.richTextBox_hmac_drbg_addInpRes);
            this.tabPage_hmac_drbg.Controls.Add(this.richTextBox_hmac_drbg_entInpRes);
            this.tabPage_hmac_drbg.Controls.Add(this.richTextBox_hmac_drbg_nonce);
            this.tabPage_hmac_drbg.Controls.Add(this.richTextBox_hmac_drbg_entInp);
            this.tabPage_hmac_drbg.Controls.Add(this.richTextBox_hmac_drbg_perStr);
            this.tabPage_hmac_drbg.Controls.Add(this.checkBox_hmac_drbg_preRes);
            this.tabPage_hmac_drbg.Controls.Add(this.numericUpDown_hmac_drbg_retByt);
            this.tabPage_hmac_drbg.Location = new System.Drawing.Point(4, 22);
            this.tabPage_hmac_drbg.Name = "tabPage_hmac_drbg";
            this.tabPage_hmac_drbg.Size = new System.Drawing.Size(885, 284);
            this.tabPage_hmac_drbg.TabIndex = 5;
            this.tabPage_hmac_drbg.Text = "Hmac_Drbg";
            this.tabPage_hmac_drbg.UseVisualStyleBackColor = true;
            // 
            // checkBox_hex_input
            // 
            this.checkBox_hex_input.AutoSize = true;
            this.checkBox_hex_input.Location = new System.Drawing.Point(16, 321);
            this.checkBox_hex_input.Name = "checkBox_hex_input";
            this.checkBox_hex_input.Size = new System.Drawing.Size(69, 17);
            this.checkBox_hex_input.TabIndex = 4;
            this.checkBox_hex_input.Text = "HexInput";
            this.checkBox_hex_input.UseVisualStyleBackColor = true;
            // 
            // comboBox_alg_types
            // 
            this.comboBox_alg_types.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_alg_types.FormattingEnabled = true;
            this.comboBox_alg_types.Location = new System.Drawing.Point(105, 319);
            this.comboBox_alg_types.Name = "comboBox_alg_types";
            this.comboBox_alg_types.Size = new System.Drawing.Size(121, 21);
            this.comboBox_alg_types.TabIndex = 7;
            // 
            // richTextBox_alg_result
            // 
            this.richTextBox_alg_result.Location = new System.Drawing.Point(12, 346);
            this.richTextBox_alg_result.Name = "richTextBox_alg_result";
            this.richTextBox_alg_result.Size = new System.Drawing.Size(885, 96);
            this.richTextBox_alg_result.TabIndex = 5;
            this.richTextBox_alg_result.Text = "";
            // 
            // richTextBox_aes_msg
            // 
            this.richTextBox_aes_msg.Location = new System.Drawing.Point(110, 12);
            this.richTextBox_aes_msg.Name = "richTextBox_aes_msg";
            this.richTextBox_aes_msg.Size = new System.Drawing.Size(518, 96);
            this.richTextBox_aes_msg.TabIndex = 0;
            this.richTextBox_aes_msg.Text = "";
            // 
            // richTextBox_aes_key
            // 
            this.richTextBox_aes_key.Location = new System.Drawing.Point(110, 114);
            this.richTextBox_aes_key.Name = "richTextBox_aes_key";
            this.richTextBox_aes_key.Size = new System.Drawing.Size(518, 77);
            this.richTextBox_aes_key.TabIndex = 1;
            this.richTextBox_aes_key.Text = "";
            // 
            // richTextBox_aes_iv
            // 
            this.richTextBox_aes_iv.Location = new System.Drawing.Point(110, 197);
            this.richTextBox_aes_iv.Name = "richTextBox_aes_iv";
            this.richTextBox_aes_iv.Size = new System.Drawing.Size(518, 66);
            this.richTextBox_aes_iv.TabIndex = 2;
            this.richTextBox_aes_iv.Text = "";
            // 
            // comboBox_aes_mode
            // 
            this.comboBox_aes_mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_aes_mode.FormattingEnabled = true;
            this.comboBox_aes_mode.Location = new System.Drawing.Point(722, 12);
            this.comboBox_aes_mode.Name = "comboBox_aes_mode";
            this.comboBox_aes_mode.Size = new System.Drawing.Size(148, 21);
            this.comboBox_aes_mode.TabIndex = 8;
            // 
            // numericUpDown_hmac_drbg_retByt
            // 
            this.numericUpDown_hmac_drbg_retByt.Location = new System.Drawing.Point(751, 16);
            this.numericUpDown_hmac_drbg_retByt.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numericUpDown_hmac_drbg_retByt.Name = "numericUpDown_hmac_drbg_retByt";
            this.numericUpDown_hmac_drbg_retByt.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown_hmac_drbg_retByt.TabIndex = 0;
            // 
            // checkBox_hmac_drbg_preRes
            // 
            this.checkBox_hmac_drbg_preRes.AutoSize = true;
            this.checkBox_hmac_drbg_preRes.Location = new System.Drawing.Point(751, 43);
            this.checkBox_hmac_drbg_preRes.Name = "checkBox_hmac_drbg_preRes";
            this.checkBox_hmac_drbg_preRes.Size = new System.Drawing.Size(126, 17);
            this.checkBox_hmac_drbg_preRes.TabIndex = 1;
            this.checkBox_hmac_drbg_preRes.Text = "PredictionResistance";
            this.checkBox_hmac_drbg_preRes.UseVisualStyleBackColor = true;
            // 
            // richTextBox_hmac_drbg_perStr
            // 
            this.richTextBox_hmac_drbg_perStr.Location = new System.Drawing.Point(31, 15);
            this.richTextBox_hmac_drbg_perStr.Name = "richTextBox_hmac_drbg_perStr";
            this.richTextBox_hmac_drbg_perStr.Size = new System.Drawing.Size(417, 33);
            this.richTextBox_hmac_drbg_perStr.TabIndex = 2;
            this.richTextBox_hmac_drbg_perStr.Text = "";
            // 
            // richTextBox_hmac_drbg_entInp
            // 
            this.richTextBox_hmac_drbg_entInp.Location = new System.Drawing.Point(31, 54);
            this.richTextBox_hmac_drbg_entInp.Name = "richTextBox_hmac_drbg_entInp";
            this.richTextBox_hmac_drbg_entInp.Size = new System.Drawing.Size(417, 33);
            this.richTextBox_hmac_drbg_entInp.TabIndex = 3;
            this.richTextBox_hmac_drbg_entInp.Text = "";
            // 
            // richTextBox_hmac_drbg_nonce
            // 
            this.richTextBox_hmac_drbg_nonce.Location = new System.Drawing.Point(31, 93);
            this.richTextBox_hmac_drbg_nonce.Name = "richTextBox_hmac_drbg_nonce";
            this.richTextBox_hmac_drbg_nonce.Size = new System.Drawing.Size(417, 33);
            this.richTextBox_hmac_drbg_nonce.TabIndex = 4;
            this.richTextBox_hmac_drbg_nonce.Text = "";
            // 
            // richTextBox_hmac_drbg_addInpRes
            // 
            this.richTextBox_hmac_drbg_addInpRes.Location = new System.Drawing.Point(482, 132);
            this.richTextBox_hmac_drbg_addInpRes.Name = "richTextBox_hmac_drbg_addInpRes";
            this.richTextBox_hmac_drbg_addInpRes.Size = new System.Drawing.Size(395, 33);
            this.richTextBox_hmac_drbg_addInpRes.TabIndex = 6;
            this.richTextBox_hmac_drbg_addInpRes.Text = "";
            // 
            // richTextBox_hmac_drbg_entInpRes
            // 
            this.richTextBox_hmac_drbg_entInpRes.Location = new System.Drawing.Point(31, 132);
            this.richTextBox_hmac_drbg_entInpRes.Name = "richTextBox_hmac_drbg_entInpRes";
            this.richTextBox_hmac_drbg_entInpRes.Size = new System.Drawing.Size(417, 33);
            this.richTextBox_hmac_drbg_entInpRes.TabIndex = 5;
            this.richTextBox_hmac_drbg_entInpRes.Text = "";
            // 
            // richTextBox_hmac_drbg_addInp2
            // 
            this.richTextBox_hmac_drbg_addInp2.Location = new System.Drawing.Point(482, 171);
            this.richTextBox_hmac_drbg_addInp2.Name = "richTextBox_hmac_drbg_addInp2";
            this.richTextBox_hmac_drbg_addInp2.Size = new System.Drawing.Size(395, 33);
            this.richTextBox_hmac_drbg_addInp2.TabIndex = 8;
            this.richTextBox_hmac_drbg_addInp2.Text = "";
            // 
            // richTextBox_hmac_drbg_addInp1
            // 
            this.richTextBox_hmac_drbg_addInp1.Location = new System.Drawing.Point(31, 171);
            this.richTextBox_hmac_drbg_addInp1.Name = "richTextBox_hmac_drbg_addInp1";
            this.richTextBox_hmac_drbg_addInp1.Size = new System.Drawing.Size(417, 33);
            this.richTextBox_hmac_drbg_addInp1.TabIndex = 7;
            this.richTextBox_hmac_drbg_addInp1.Text = "";
            // 
            // richTextBox_hmac_drbg_entInpPR2
            // 
            this.richTextBox_hmac_drbg_entInpPR2.Location = new System.Drawing.Point(482, 210);
            this.richTextBox_hmac_drbg_entInpPR2.Name = "richTextBox_hmac_drbg_entInpPR2";
            this.richTextBox_hmac_drbg_entInpPR2.Size = new System.Drawing.Size(395, 33);
            this.richTextBox_hmac_drbg_entInpPR2.TabIndex = 10;
            this.richTextBox_hmac_drbg_entInpPR2.Text = "";
            // 
            // richTextBox_hmac_drbg_entInpPR1
            // 
            this.richTextBox_hmac_drbg_entInpPR1.Location = new System.Drawing.Point(31, 210);
            this.richTextBox_hmac_drbg_entInpPR1.Name = "richTextBox_hmac_drbg_entInpPR1";
            this.richTextBox_hmac_drbg_entInpPR1.Size = new System.Drawing.Size(417, 33);
            this.richTextBox_hmac_drbg_entInpPR1.TabIndex = 9;
            this.richTextBox_hmac_drbg_entInpPR1.Text = "";
            // 
            // CryptoHash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 491);
            this.Controls.Add(this.richTextBox_alg_result);
            this.Controls.Add(this.checkBox_hex_input);
            this.Controls.Add(this.comboBox_alg_types);
            this.Controls.Add(this.tabControl_algorithms);
            this.Controls.Add(this.button1);
            this.Name = "CryptoHash";
            this.Text = "CryptoHash";
            this.tabControl_algorithms.ResumeLayout(false);
            this.tabPage_sha.ResumeLayout(false);
            this.tabPage_sha.PerformLayout();
            this.tabPage_hkdf.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_hkdf_outputKeyBytes)).EndInit();
            this.tabPage_hmac_sha.ResumeLayout(false);
            this.tabPage_rsa.ResumeLayout(false);
            this.tabPage_aes.ResumeLayout(false);
            this.tabPage_hmac_drbg.ResumeLayout(false);
            this.tabPage_hmac_drbg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_hmac_drbg_retByt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl_algorithms;
        private System.Windows.Forms.TabPage tabPage_sha;
        private System.Windows.Forms.TabPage tabPage_hkdf;
        private System.Windows.Forms.TabPage tabPage_hmac_sha;
        private System.Windows.Forms.TabPage tabPage_rsa;
        private System.Windows.Forms.TabPage tabPage_aes;
        private System.Windows.Forms.TabPage tabPage_hmac_drbg;
        private System.Windows.Forms.RichTextBox richTextBox_sha_msg;
        private System.Windows.Forms.CheckBox checkBox_hex_input;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_alg_types;
        private System.Windows.Forms.NumericUpDown numericUpDown_hkdf_outputKeyBytes;
        private System.Windows.Forms.RichTextBox richTextBox_hkdf_key;
        private System.Windows.Forms.RichTextBox richTextBox_hkdf_fixedData;
        private System.Windows.Forms.RichTextBox richTextBox_alg_result;
        private System.Windows.Forms.RichTextBox richTextBox_hmac_sha_key;
        private System.Windows.Forms.RichTextBox richTextBox_hmac_sha_msg;
        private System.Windows.Forms.RichTextBox richTextBox_rsa_sig;
        private System.Windows.Forms.RichTextBox richTextBox_rsa_mod;
        private System.Windows.Forms.RichTextBox richTextBox_rsa_exp;
        private System.Windows.Forms.RichTextBox richTextBox_rsa_msg;
        private System.Windows.Forms.RichTextBox richTextBox_aes_iv;
        private System.Windows.Forms.RichTextBox richTextBox_aes_key;
        private System.Windows.Forms.RichTextBox richTextBox_aes_msg;
        private System.Windows.Forms.ComboBox comboBox_aes_mode;
        private System.Windows.Forms.NumericUpDown numericUpDown_hmac_drbg_retByt;
        private System.Windows.Forms.CheckBox checkBox_hmac_drbg_preRes;
        private System.Windows.Forms.RichTextBox richTextBox_hmac_drbg_addInpRes;
        private System.Windows.Forms.RichTextBox richTextBox_hmac_drbg_entInpRes;
        private System.Windows.Forms.RichTextBox richTextBox_hmac_drbg_nonce;
        private System.Windows.Forms.RichTextBox richTextBox_hmac_drbg_entInp;
        private System.Windows.Forms.RichTextBox richTextBox_hmac_drbg_perStr;
        private System.Windows.Forms.RichTextBox richTextBox_hmac_drbg_entInpPR2;
        private System.Windows.Forms.RichTextBox richTextBox_hmac_drbg_entInpPR1;
        private System.Windows.Forms.RichTextBox richTextBox_hmac_drbg_addInp2;
        private System.Windows.Forms.RichTextBox richTextBox_hmac_drbg_addInp1;
    }
}

