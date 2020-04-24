using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace openssl_app.logmanager
{
    static class LogManager
    {
        public static DialogResult ShowMessageBox(string caption, string msg, string debug = "")
        {
            string allMsg = msg;
#if DEBUG
            allMsg += " : " + debug;
#endif // DEBUG
            return MessageBox.Show(allMsg, caption);
        }
    }
}
