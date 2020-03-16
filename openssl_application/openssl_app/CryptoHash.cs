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

        public CryptoHash()
        {
            InitializeComponent();
            InitializeManagers();
            this.crypto_manager = sha_manager;

            this.tabControl_algorithms.TabIndexChanged += TabControl_algorithms_TabIndexChanged;
        }

        private void TabControl_algorithms_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void InitializeManagers()
        {
            this.InitializeShaManager();
        }

        private void InitializeShaManager()
        {
            this.sha_manager = new ShaManager();
            this.sha_manager.Hash = this.richTextBox_sha_hash;
            this.sha_manager.Msg = this.richTextBox_sha_msg;
            this.sha_manager.Type = this.comboBox_sha_types;
            this.sha_manager.Type.SelectedIndex = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.crypto_manager.Generate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = @"C:\Users\Michal\Desktop\nlog.txt";
            nlogDumpAll(path, (uint)path.Length);
        }
    }
}
