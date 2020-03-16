using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using openssl_app.dllmanager;
using System.Windows.Forms;

namespace openssl_app.appmanager
{
    enum MANAGER_ALG_TYPE
    {
        SHA_MANAGER = 0,
        HKDF_MANAGER,
        HMAC_SHA_MANAGER,
        RSA_MANAGER,
        AES_MANAGER,
        HMAC_DRBG_MANAGER,
    }

    abstract class Manager
    {
        public bool HexInput { get; set; }
        public string Result { get; private set; }
        public int Mode { get; set; }
        public abstract CRYPTO_STATUS Generate(int subTarget = 0);

        protected Manager()
        {
            this.HexInput = false;
            this.Result = string.Empty;
            this.Mode = 0;
        }

        protected void SetResult(string result)
        {
            this.Result = result;
        }
    }
}
