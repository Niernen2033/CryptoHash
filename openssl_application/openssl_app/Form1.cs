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

namespace openssl_app
{
    public partial class Form1 : Form
    {
        [DllImport(DllProperties.DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
        private static extern int nlogDumpAll(string filePath, uint filePathLen);
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HmacDrbgProvider provider = new HmacDrbgProvider();
            provider.PredictionResistance = true;
            provider.ReturnedBytes = 1024 / 8;
            provider.EntropyInput = new List<byte>(DataConverter.BytesFromHexString("9969e54b4703ff31785b879a7e5c0eae0d3e309559e9fe96b0676d49d591ea4d"));
            provider.Nonce = new List<byte>(DataConverter.BytesFromHexString("07d20d46d064757d3023cac2376127ab"));
            provider.EntropyInputPR1 = new List<byte>(DataConverter.BytesFromHexString("c60f2999100f738c10f74792676a3fc4a262d13721798046e29a295181569f54"));
            provider.EntropyInputPR2 = new List<byte>(DataConverter.BytesFromHexString("c11d4524c9071bd3096015fcf7bc24a607f22fa065c937658a2a77a8699089f4"));

            this.listBox1.Items.Add(provider.GenerateRandomData());
            this.listBox1.Items.Add(provider.RandomData.Count);
            this.listBox1.Items.Add(DataConverter.HexStringFromBytes(provider.RandomData.ToArray()));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = @"C:\Users\Michal\Desktop\nlog.txt";
            nlogDumpAll(path, (uint)path.Length);
        }
    }
}
