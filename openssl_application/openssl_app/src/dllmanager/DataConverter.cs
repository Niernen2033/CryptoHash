using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace openssl_app.dllmanager
{
    static class DataConverter
    {
        public static string HexStringFromBytes(byte[] bytes)
        {
            return HexStringFromBytes(bytes, (uint)bytes.Length);
        }

        public static byte[] BytesFromString(string normalString)
        {
            return Encoding.ASCII.GetBytes(normalString);
        }

        public static string HexStringFromBytes(byte[] bytes, uint bytesSize)
        {
            byte[] tempBytes = new byte[bytesSize];
            for (int i = 0; i < bytesSize; i++)
            {
                tempBytes[i] = bytes[i];
            }
            SoapHexBinary shb = new SoapHexBinary(tempBytes);
            return shb.ToString().ToLower();
        }

        public static byte[] BytesFromHexString(string hexString)
        {
            if((hexString.Length % 2) != 0)
            {
                hexString = "0" + hexString;
            }
            SoapHexBinary shb = SoapHexBinary.Parse(hexString);
            return shb.Value;
        }
    }
}
