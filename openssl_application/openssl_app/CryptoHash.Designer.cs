namespace openssl_app
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
            this.comboBox_aes_mode = new System.Windows.Forms.ComboBox();
            this.richTextBox_aes_iv = new System.Windows.Forms.RichTextBox();
            this.richTextBox_aes_key = new System.Windows.Forms.RichTextBox();
            this.richTextBox_aes_msg = new System.Windows.Forms.RichTextBox();
            this.tabPage_hmac_drbg = new System.Windows.Forms.TabPage();
            this.richTextBox_hmac_drbg_entInpPR2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox_hmac_drbg_entInpPR1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox_hmac_drbg_addInp2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox_hmac_drbg_addInp1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox_hmac_drbg_addInpRes = new System.Windows.Forms.RichTextBox();
            this.richTextBox_hmac_drbg_entInpRes = new System.Windows.Forms.RichTextBox();
            this.richTextBox_hmac_drbg_nonce = new System.Windows.Forms.RichTextBox();
            this.richTextBox_hmac_drbg_entInp = new System.Windows.Forms.RichTextBox();
            this.richTextBox_hmac_drbg_perStr = new System.Windows.Forms.RichTextBox();
            this.checkBox_hmac_drbg_preRes = new System.Windows.Forms.CheckBox();
            this.numericUpDown_hmac_drbg_retByt = new System.Windows.Forms.NumericUpDown();
            this.checkBox_hex_input = new System.Windows.Forms.CheckBox();
            this.comboBox_alg_types = new System.Windows.Forms.ComboBox();
            this.richTextBox_alg_result = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
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
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(738, 446);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(159, 33);
            this.button1.TabIndex = 0;
            this.button1.Text = "Calculate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl_algorithms
            // 
            this.tabControl_algorithms.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
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
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(17, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Msg:";
            // 
            // richTextBox_sha_msg
            // 
            this.richTextBox_sha_msg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_sha_msg.Location = new System.Drawing.Point(61, 17);
            this.richTextBox_sha_msg.Name = "richTextBox_sha_msg";
            this.richTextBox_sha_msg.Size = new System.Drawing.Size(803, 248);
            this.richTextBox_sha_msg.TabIndex = 1;
            this.richTextBox_sha_msg.Text = "";
            // 
            // tabPage_hkdf
            // 
            this.tabPage_hkdf.Controls.Add(this.label4);
            this.tabPage_hkdf.Controls.Add(this.label3);
            this.tabPage_hkdf.Controls.Add(this.label1);
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
            this.richTextBox_hkdf_key.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_hkdf_key.Location = new System.Drawing.Point(89, 166);
            this.richTextBox_hkdf_key.Name = "richTextBox_hkdf_key";
            this.richTextBox_hkdf_key.Size = new System.Drawing.Size(781, 99);
            this.richTextBox_hkdf_key.TabIndex = 9;
            this.richTextBox_hkdf_key.Text = "";
            // 
            // richTextBox_hkdf_fixedData
            // 
            this.richTextBox_hkdf_fixedData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_hkdf_fixedData.Location = new System.Drawing.Point(89, 48);
            this.richTextBox_hkdf_fixedData.Name = "richTextBox_hkdf_fixedData";
            this.richTextBox_hkdf_fixedData.Size = new System.Drawing.Size(781, 112);
            this.richTextBox_hkdf_fixedData.TabIndex = 8;
            this.richTextBox_hkdf_fixedData.Text = "";
            // 
            // numericUpDown_hkdf_outputKeyBytes
            // 
            this.numericUpDown_hkdf_outputKeyBytes.Location = new System.Drawing.Point(122, 17);
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
            this.tabPage_hmac_sha.Controls.Add(this.label6);
            this.tabPage_hmac_sha.Controls.Add(this.label5);
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
            this.richTextBox_hmac_sha_key.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_hmac_sha_key.Location = new System.Drawing.Point(55, 142);
            this.richTextBox_hmac_sha_key.Name = "richTextBox_hmac_sha_key";
            this.richTextBox_hmac_sha_key.Size = new System.Drawing.Size(815, 136);
            this.richTextBox_hmac_sha_key.TabIndex = 1;
            this.richTextBox_hmac_sha_key.Text = "";
            // 
            // richTextBox_hmac_sha_msg
            // 
            this.richTextBox_hmac_sha_msg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_hmac_sha_msg.Location = new System.Drawing.Point(55, 6);
            this.richTextBox_hmac_sha_msg.Name = "richTextBox_hmac_sha_msg";
            this.richTextBox_hmac_sha_msg.Size = new System.Drawing.Size(815, 130);
            this.richTextBox_hmac_sha_msg.TabIndex = 0;
            this.richTextBox_hmac_sha_msg.Text = "";
            // 
            // tabPage_rsa
            // 
            this.tabPage_rsa.Controls.Add(this.label10);
            this.tabPage_rsa.Controls.Add(this.label9);
            this.tabPage_rsa.Controls.Add(this.label8);
            this.tabPage_rsa.Controls.Add(this.label7);
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
            this.richTextBox_rsa_sig.Location = new System.Drawing.Point(77, 206);
            this.richTextBox_rsa_sig.Name = "richTextBox_rsa_sig";
            this.richTextBox_rsa_sig.Size = new System.Drawing.Size(796, 63);
            this.richTextBox_rsa_sig.TabIndex = 3;
            this.richTextBox_rsa_sig.Text = "";
            // 
            // richTextBox_rsa_mod
            // 
            this.richTextBox_rsa_mod.Location = new System.Drawing.Point(77, 142);
            this.richTextBox_rsa_mod.Name = "richTextBox_rsa_mod";
            this.richTextBox_rsa_mod.Size = new System.Drawing.Size(796, 58);
            this.richTextBox_rsa_mod.TabIndex = 2;
            this.richTextBox_rsa_mod.Text = "";
            // 
            // richTextBox_rsa_exp
            // 
            this.richTextBox_rsa_exp.Location = new System.Drawing.Point(77, 75);
            this.richTextBox_rsa_exp.Name = "richTextBox_rsa_exp";
            this.richTextBox_rsa_exp.Size = new System.Drawing.Size(796, 60);
            this.richTextBox_rsa_exp.TabIndex = 1;
            this.richTextBox_rsa_exp.Text = "";
            // 
            // richTextBox_rsa_msg
            // 
            this.richTextBox_rsa_msg.Location = new System.Drawing.Point(77, 3);
            this.richTextBox_rsa_msg.Name = "richTextBox_rsa_msg";
            this.richTextBox_rsa_msg.Size = new System.Drawing.Size(796, 63);
            this.richTextBox_rsa_msg.TabIndex = 0;
            this.richTextBox_rsa_msg.Text = "";
            // 
            // tabPage_aes
            // 
            this.tabPage_aes.Controls.Add(this.label14);
            this.tabPage_aes.Controls.Add(this.label13);
            this.tabPage_aes.Controls.Add(this.label12);
            this.tabPage_aes.Controls.Add(this.label11);
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
            // comboBox_aes_mode
            // 
            this.comboBox_aes_mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_aes_mode.FormattingEnabled = true;
            this.comboBox_aes_mode.Location = new System.Drawing.Point(68, 11);
            this.comboBox_aes_mode.Name = "comboBox_aes_mode";
            this.comboBox_aes_mode.Size = new System.Drawing.Size(148, 21);
            this.comboBox_aes_mode.TabIndex = 8;
            // 
            // richTextBox_aes_iv
            // 
            this.richTextBox_aes_iv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_aes_iv.Location = new System.Drawing.Point(68, 210);
            this.richTextBox_aes_iv.Name = "richTextBox_aes_iv";
            this.richTextBox_aes_iv.Size = new System.Drawing.Size(796, 62);
            this.richTextBox_aes_iv.TabIndex = 2;
            this.richTextBox_aes_iv.Text = "";
            // 
            // richTextBox_aes_key
            // 
            this.richTextBox_aes_key.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_aes_key.Location = new System.Drawing.Point(68, 139);
            this.richTextBox_aes_key.Name = "richTextBox_aes_key";
            this.richTextBox_aes_key.Size = new System.Drawing.Size(796, 61);
            this.richTextBox_aes_key.TabIndex = 1;
            this.richTextBox_aes_key.Text = "";
            // 
            // richTextBox_aes_msg
            // 
            this.richTextBox_aes_msg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_aes_msg.Location = new System.Drawing.Point(68, 44);
            this.richTextBox_aes_msg.Name = "richTextBox_aes_msg";
            this.richTextBox_aes_msg.Size = new System.Drawing.Size(796, 89);
            this.richTextBox_aes_msg.TabIndex = 0;
            this.richTextBox_aes_msg.Text = "";
            // 
            // tabPage_hmac_drbg
            // 
            this.tabPage_hmac_drbg.Controls.Add(this.label24);
            this.tabPage_hmac_drbg.Controls.Add(this.label23);
            this.tabPage_hmac_drbg.Controls.Add(this.label22);
            this.tabPage_hmac_drbg.Controls.Add(this.label21);
            this.tabPage_hmac_drbg.Controls.Add(this.label20);
            this.tabPage_hmac_drbg.Controls.Add(this.label19);
            this.tabPage_hmac_drbg.Controls.Add(this.label18);
            this.tabPage_hmac_drbg.Controls.Add(this.label17);
            this.tabPage_hmac_drbg.Controls.Add(this.label16);
            this.tabPage_hmac_drbg.Controls.Add(this.label15);
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
            // richTextBox_hmac_drbg_entInpPR2
            // 
            this.richTextBox_hmac_drbg_entInpPR2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_hmac_drbg_entInpPR2.Location = new System.Drawing.Point(517, 210);
            this.richTextBox_hmac_drbg_entInpPR2.Name = "richTextBox_hmac_drbg_entInpPR2";
            this.richTextBox_hmac_drbg_entInpPR2.Size = new System.Drawing.Size(360, 33);
            this.richTextBox_hmac_drbg_entInpPR2.TabIndex = 10;
            this.richTextBox_hmac_drbg_entInpPR2.Text = "";
            // 
            // richTextBox_hmac_drbg_entInpPR1
            // 
            this.richTextBox_hmac_drbg_entInpPR1.Location = new System.Drawing.Point(86, 210);
            this.richTextBox_hmac_drbg_entInpPR1.Name = "richTextBox_hmac_drbg_entInpPR1";
            this.richTextBox_hmac_drbg_entInpPR1.Size = new System.Drawing.Size(330, 33);
            this.richTextBox_hmac_drbg_entInpPR1.TabIndex = 9;
            this.richTextBox_hmac_drbg_entInpPR1.Text = "";
            // 
            // richTextBox_hmac_drbg_addInp2
            // 
            this.richTextBox_hmac_drbg_addInp2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_hmac_drbg_addInp2.Location = new System.Drawing.Point(517, 171);
            this.richTextBox_hmac_drbg_addInp2.Name = "richTextBox_hmac_drbg_addInp2";
            this.richTextBox_hmac_drbg_addInp2.Size = new System.Drawing.Size(360, 33);
            this.richTextBox_hmac_drbg_addInp2.TabIndex = 8;
            this.richTextBox_hmac_drbg_addInp2.Text = "";
            // 
            // richTextBox_hmac_drbg_addInp1
            // 
            this.richTextBox_hmac_drbg_addInp1.Location = new System.Drawing.Point(86, 171);
            this.richTextBox_hmac_drbg_addInp1.Name = "richTextBox_hmac_drbg_addInp1";
            this.richTextBox_hmac_drbg_addInp1.Size = new System.Drawing.Size(330, 33);
            this.richTextBox_hmac_drbg_addInp1.TabIndex = 7;
            this.richTextBox_hmac_drbg_addInp1.Text = "";
            // 
            // richTextBox_hmac_drbg_addInpRes
            // 
            this.richTextBox_hmac_drbg_addInpRes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_hmac_drbg_addInpRes.Location = new System.Drawing.Point(517, 132);
            this.richTextBox_hmac_drbg_addInpRes.Name = "richTextBox_hmac_drbg_addInpRes";
            this.richTextBox_hmac_drbg_addInpRes.Size = new System.Drawing.Size(360, 33);
            this.richTextBox_hmac_drbg_addInpRes.TabIndex = 6;
            this.richTextBox_hmac_drbg_addInpRes.Text = "";
            // 
            // richTextBox_hmac_drbg_entInpRes
            // 
            this.richTextBox_hmac_drbg_entInpRes.Location = new System.Drawing.Point(86, 132);
            this.richTextBox_hmac_drbg_entInpRes.Name = "richTextBox_hmac_drbg_entInpRes";
            this.richTextBox_hmac_drbg_entInpRes.Size = new System.Drawing.Size(330, 33);
            this.richTextBox_hmac_drbg_entInpRes.TabIndex = 5;
            this.richTextBox_hmac_drbg_entInpRes.Text = "";
            // 
            // richTextBox_hmac_drbg_nonce
            // 
            this.richTextBox_hmac_drbg_nonce.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_hmac_drbg_nonce.Location = new System.Drawing.Point(86, 93);
            this.richTextBox_hmac_drbg_nonce.Name = "richTextBox_hmac_drbg_nonce";
            this.richTextBox_hmac_drbg_nonce.Size = new System.Drawing.Size(791, 33);
            this.richTextBox_hmac_drbg_nonce.TabIndex = 4;
            this.richTextBox_hmac_drbg_nonce.Text = "";
            // 
            // richTextBox_hmac_drbg_entInp
            // 
            this.richTextBox_hmac_drbg_entInp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_hmac_drbg_entInp.Location = new System.Drawing.Point(86, 54);
            this.richTextBox_hmac_drbg_entInp.Name = "richTextBox_hmac_drbg_entInp";
            this.richTextBox_hmac_drbg_entInp.Size = new System.Drawing.Size(611, 33);
            this.richTextBox_hmac_drbg_entInp.TabIndex = 3;
            this.richTextBox_hmac_drbg_entInp.Text = "";
            // 
            // richTextBox_hmac_drbg_perStr
            // 
            this.richTextBox_hmac_drbg_perStr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_hmac_drbg_perStr.Location = new System.Drawing.Point(86, 14);
            this.richTextBox_hmac_drbg_perStr.Name = "richTextBox_hmac_drbg_perStr";
            this.richTextBox_hmac_drbg_perStr.Size = new System.Drawing.Size(611, 33);
            this.richTextBox_hmac_drbg_perStr.TabIndex = 2;
            this.richTextBox_hmac_drbg_perStr.Text = "";
            // 
            // checkBox_hmac_drbg_preRes
            // 
            this.checkBox_hmac_drbg_preRes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_hmac_drbg_preRes.AutoSize = true;
            this.checkBox_hmac_drbg_preRes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkBox_hmac_drbg_preRes.Location = new System.Drawing.Point(715, 63);
            this.checkBox_hmac_drbg_preRes.Name = "checkBox_hmac_drbg_preRes";
            this.checkBox_hmac_drbg_preRes.Size = new System.Drawing.Size(162, 19);
            this.checkBox_hmac_drbg_preRes.TabIndex = 1;
            this.checkBox_hmac_drbg_preRes.Text = "PredictionResistance";
            this.checkBox_hmac_drbg_preRes.UseVisualStyleBackColor = true;
            // 
            // numericUpDown_hmac_drbg_retByt
            // 
            this.numericUpDown_hmac_drbg_retByt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown_hmac_drbg_retByt.Location = new System.Drawing.Point(715, 29);
            this.numericUpDown_hmac_drbg_retByt.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numericUpDown_hmac_drbg_retByt.Name = "numericUpDown_hmac_drbg_retByt";
            this.numericUpDown_hmac_drbg_retByt.Size = new System.Drawing.Size(162, 20);
            this.numericUpDown_hmac_drbg_retByt.TabIndex = 0;
            // 
            // checkBox_hex_input
            // 
            this.checkBox_hex_input.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.comboBox_alg_types.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_alg_types.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_alg_types.FormattingEnabled = true;
            this.comboBox_alg_types.Location = new System.Drawing.Point(105, 319);
            this.comboBox_alg_types.Name = "comboBox_alg_types";
            this.comboBox_alg_types.Size = new System.Drawing.Size(121, 21);
            this.comboBox_alg_types.TabIndex = 7;
            // 
            // richTextBox_alg_result
            // 
            this.richTextBox_alg_result.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_alg_result.Location = new System.Drawing.Point(12, 346);
            this.richTextBox_alg_result.Name = "richTextBox_alg_result";
            this.richTextBox_alg_result.Size = new System.Drawing.Size(885, 94);
            this.richTextBox_alg_result.TabIndex = 5;
            this.richTextBox_alg_result.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "OutputKeyBytes:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(6, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "FixedData:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(24, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "Key:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(7, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 15);
            this.label5.TabIndex = 2;
            this.label5.Text = "Msg:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(11, 198);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 15);
            this.label6.TabIndex = 3;
            this.label6.Text = "Key:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(13, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 15);
            this.label7.TabIndex = 4;
            this.label7.Text = "Msg:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(3, 100);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 15);
            this.label8.TabIndex = 5;
            this.label8.Text = "Exponent:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.Location = new System.Drawing.Point(3, 166);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 15);
            this.label9.TabIndex = 6;
            this.label9.Text = "Modulus:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label10.Location = new System.Drawing.Point(3, 232);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 15);
            this.label10.TabIndex = 7;
            this.label10.Text = "Signature:";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label11.Location = new System.Drawing.Point(18, 77);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 15);
            this.label11.TabIndex = 9;
            this.label11.Text = "Msg:";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label12.Location = new System.Drawing.Point(18, 160);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 15);
            this.label12.TabIndex = 10;
            this.label12.Text = "Key:";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label13.Location = new System.Drawing.Point(18, 234);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(21, 15);
            this.label13.TabIndex = 11;
            this.label13.Text = "Iv:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label14.Location = new System.Drawing.Point(15, 12);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 15);
            this.label14.TabIndex = 12;
            this.label14.Text = "Mode:";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label15.Location = new System.Drawing.Point(18, 22);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 15);
            this.label15.TabIndex = 11;
            this.label15.Text = "PerStr:";
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label16.Location = new System.Drawing.Point(17, 64);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(52, 15);
            this.label16.TabIndex = 12;
            this.label16.Text = "EntInp:";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label17.Location = new System.Drawing.Point(14, 102);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(52, 15);
            this.label17.TabIndex = 13;
            this.label17.Text = "Nonce:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label18.Location = new System.Drawing.Point(3, 142);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(77, 15);
            this.label18.TabIndex = 14;
            this.label18.Text = "EntInpRes:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label19.Location = new System.Drawing.Point(14, 182);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(63, 15);
            this.label19.TabIndex = 15;
            this.label19.Text = "AddInp1:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label20.Location = new System.Drawing.Point(3, 218);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(79, 15);
            this.label20.TabIndex = 16;
            this.label20.Text = "EntInpPR1:";
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label21.Location = new System.Drawing.Point(431, 142);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(80, 15);
            this.label21.TabIndex = 17;
            this.label21.Text = "AddInpRes:";
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label22.Location = new System.Drawing.Point(440, 182);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(63, 15);
            this.label22.TabIndex = 18;
            this.label22.Text = "AddInp2:";
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label23.Location = new System.Drawing.Point(432, 218);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(79, 15);
            this.label23.TabIndex = 19;
            this.label23.Text = "EntInpPR2:";
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label24.Location = new System.Drawing.Point(712, 9);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(104, 15);
            this.label24.TabIndex = 20;
            this.label24.Text = "ReturnedBytes:";
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
            this.tabPage_hkdf.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_hkdf_outputKeyBytes)).EndInit();
            this.tabPage_hmac_sha.ResumeLayout(false);
            this.tabPage_hmac_sha.PerformLayout();
            this.tabPage_rsa.ResumeLayout(false);
            this.tabPage_rsa.PerformLayout();
            this.tabPage_aes.ResumeLayout(false);
            this.tabPage_aes.PerformLayout();
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
    }
}

