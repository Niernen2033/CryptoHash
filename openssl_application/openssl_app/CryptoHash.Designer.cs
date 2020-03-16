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
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl_algorithms = new System.Windows.Forms.TabControl();
            this.tabPage_sha = new System.Windows.Forms.TabPage();
            this.richTextBox_sha_hash = new System.Windows.Forms.RichTextBox();
            this.richTextBox_sha_msg = new System.Windows.Forms.RichTextBox();
            this.tabPage_hkdf = new System.Windows.Forms.TabPage();
            this.tabPage_hmac_sha = new System.Windows.Forms.TabPage();
            this.tabPage_rsa = new System.Windows.Forms.TabPage();
            this.tabPage_aes = new System.Windows.Forms.TabPage();
            this.tabPage_hmac_drbg = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_sha_types = new System.Windows.Forms.ComboBox();
            this.tabControl_algorithms.SuspendLayout();
            this.tabPage_sha.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(520, 367);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(159, 74);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(16, 392);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 46);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            this.tabControl_algorithms.Size = new System.Drawing.Size(776, 352);
            this.tabControl_algorithms.TabIndex = 3;
            // 
            // tabPage_sha
            // 
            this.tabPage_sha.Controls.Add(this.comboBox_sha_types);
            this.tabPage_sha.Controls.Add(this.label1);
            this.tabPage_sha.Controls.Add(this.richTextBox_sha_hash);
            this.tabPage_sha.Controls.Add(this.richTextBox_sha_msg);
            this.tabPage_sha.Location = new System.Drawing.Point(4, 22);
            this.tabPage_sha.Name = "tabPage_sha";
            this.tabPage_sha.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_sha.Size = new System.Drawing.Size(768, 326);
            this.tabPage_sha.TabIndex = 0;
            this.tabPage_sha.Text = "Sha";
            this.tabPage_sha.UseVisualStyleBackColor = true;
            // 
            // richTextBox_sha_hash
            // 
            this.richTextBox_sha_hash.Location = new System.Drawing.Point(6, 153);
            this.richTextBox_sha_hash.Name = "richTextBox_sha_hash";
            this.richTextBox_sha_hash.Size = new System.Drawing.Size(756, 155);
            this.richTextBox_sha_hash.TabIndex = 2;
            this.richTextBox_sha_hash.Text = "";
            // 
            // richTextBox_sha_msg
            // 
            this.richTextBox_sha_msg.Location = new System.Drawing.Point(6, 33);
            this.richTextBox_sha_msg.Name = "richTextBox_sha_msg";
            this.richTextBox_sha_msg.Size = new System.Drawing.Size(756, 113);
            this.richTextBox_sha_msg.TabIndex = 1;
            this.richTextBox_sha_msg.Text = "";
            // 
            // tabPage_hkdf
            // 
            this.tabPage_hkdf.Location = new System.Drawing.Point(4, 22);
            this.tabPage_hkdf.Name = "tabPage_hkdf";
            this.tabPage_hkdf.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_hkdf.Size = new System.Drawing.Size(768, 326);
            this.tabPage_hkdf.TabIndex = 1;
            this.tabPage_hkdf.Text = "Hkdf";
            this.tabPage_hkdf.UseVisualStyleBackColor = true;
            // 
            // tabPage_hmac_sha
            // 
            this.tabPage_hmac_sha.Location = new System.Drawing.Point(4, 22);
            this.tabPage_hmac_sha.Name = "tabPage_hmac_sha";
            this.tabPage_hmac_sha.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_hmac_sha.Size = new System.Drawing.Size(768, 326);
            this.tabPage_hmac_sha.TabIndex = 2;
            this.tabPage_hmac_sha.Text = "Hmac_Sha";
            this.tabPage_hmac_sha.UseVisualStyleBackColor = true;
            // 
            // tabPage_rsa
            // 
            this.tabPage_rsa.Location = new System.Drawing.Point(4, 22);
            this.tabPage_rsa.Name = "tabPage_rsa";
            this.tabPage_rsa.Size = new System.Drawing.Size(768, 326);
            this.tabPage_rsa.TabIndex = 3;
            this.tabPage_rsa.Text = "Rsa";
            this.tabPage_rsa.UseVisualStyleBackColor = true;
            // 
            // tabPage_aes
            // 
            this.tabPage_aes.Location = new System.Drawing.Point(4, 22);
            this.tabPage_aes.Name = "tabPage_aes";
            this.tabPage_aes.Size = new System.Drawing.Size(768, 326);
            this.tabPage_aes.TabIndex = 4;
            this.tabPage_aes.Text = "Aes";
            this.tabPage_aes.UseVisualStyleBackColor = true;
            // 
            // tabPage_hmac_drbg
            // 
            this.tabPage_hmac_drbg.Location = new System.Drawing.Point(4, 22);
            this.tabPage_hmac_drbg.Name = "tabPage_hmac_drbg";
            this.tabPage_hmac_drbg.Size = new System.Drawing.Size(768, 326);
            this.tabPage_hmac_drbg.TabIndex = 5;
            this.tabPage_hmac_drbg.Text = "Hmac_Drbg";
            this.tabPage_hmac_drbg.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "ShaType:";
            // 
            // comboBox_sha_types
            // 
            this.comboBox_sha_types.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_sha_types.FormattingEnabled = true;
            this.comboBox_sha_types.Items.AddRange(new object[] {
            "SHA_224",
            "SHA_256",
            "SHA_384",
            "SHA_512"});
            this.comboBox_sha_types.Location = new System.Drawing.Point(65, 6);
            this.comboBox_sha_types.Name = "comboBox_sha_types";
            this.comboBox_sha_types.Size = new System.Drawing.Size(121, 21);
            this.comboBox_sha_types.TabIndex = 5;
            // 
            // CryptoHash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl_algorithms);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "CryptoHash";
            this.Text = "Form1";
            this.tabControl_algorithms.ResumeLayout(false);
            this.tabPage_sha.ResumeLayout(false);
            this.tabPage_sha.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabControl tabControl_algorithms;
        private System.Windows.Forms.TabPage tabPage_sha;
        private System.Windows.Forms.TabPage tabPage_hkdf;
        private System.Windows.Forms.TabPage tabPage_hmac_sha;
        private System.Windows.Forms.TabPage tabPage_rsa;
        private System.Windows.Forms.TabPage tabPage_aes;
        private System.Windows.Forms.TabPage tabPage_hmac_drbg;
        private System.Windows.Forms.RichTextBox richTextBox_sha_hash;
        private System.Windows.Forms.RichTextBox richTextBox_sha_msg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_sha_types;
    }
}

