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

namespace openssl_app
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct crypto_buffer_t
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2048)]
        public byte[] buffer;
        public uint bytes;
    }

    public partial class Form1 : Form
    {
        [DllImport(@"D:\Repos\CryptoHash\openssl_application\openssl_app\openssl_library\x86\openssl.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int nlogDump(int logId, string filePath, uint filePathLen);
        [DllImport(@"D:\Repos\CryptoHash\openssl_application\openssl_app\openssl_library\x86\openssl.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int computeSha(int shaType, byte[] msg, uint msgBytes, out crypto_buffer_t digset);

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] msg = new byte[] { 1, 2, 3 };
            crypto_buffer_t result;
            int status = computeSha(1, msg, (uint)msg.Length, out result);
            this.listBox1.Items.Add(status);
            this.listBox1.Items.Add(result.bytes);
            string path = @"C:\Users\Michal\Desktop\nlog.txt";
            nlogDump(0, path, (uint)path.Length);
        }
    }
}
