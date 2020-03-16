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
        HMAC_DRBG_MANAGER,
        AES_MANAGER,
        HMAC_SHA_MANAGER,
        RSA_MANAGER
    }

    abstract class Manager
    {
        public MANAGER_ALG_TYPE ManagerType { get; private set; }
        public abstract CRYPTO_STATUS Generate(int subTarget = 0);

        protected Manager(MANAGER_ALG_TYPE algType)
        {
            this.ManagerType = algType;
        }
    }
}
