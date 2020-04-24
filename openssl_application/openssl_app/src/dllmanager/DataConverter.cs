using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using openssl_app.logmanager;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace openssl_app.dllmanager
{
    static class DataConverter
    {
        public static string HexStringFromBytes(byte[] bytes)
        {
            return HexStringFromBytes(bytes, (uint)bytes.Length);
        }

        public static string StringFromBytes(byte[] bytes)
        {
            string result;
            try
            {
                result = Encoding.ASCII.GetString(bytes);
            }
            catch (Exception exc)
            {
                LogManager.ShowMessageBox("Error", "Wrong format", exc.Message);
                result = string.Empty;
            }
            return result;
        }

        public static byte[] BytesFromString(string normalString)
        {
            byte[] result;
            try
            {
                result = Encoding.ASCII.GetBytes(normalString);
            }
            catch (Exception exc)
            {
                LogManager.ShowMessageBox("Error", "Wrong format", exc.Message);
                result = null;
            }
            return result;
        }

        public static string HexStringFromBytes(byte[] bytes, uint bytesSize)
        {
            byte[] tempBytes = new byte[bytesSize];
            for (int i = 0; i < bytesSize; i++)
            {
                tempBytes[i] = bytes[i];
            }

            string result;
            try
            {
                SoapHexBinary shb = new SoapHexBinary(tempBytes);
                result = shb.ToString().ToLower();
            }
            catch (Exception exc)
            {
                LogManager.ShowMessageBox("Error", "Wrong format", exc.Message);
                result = string.Empty;
            }
            return result;
        }

        public static byte[] BytesFromHexString(string hexString)
        {
            if((hexString.Length % 2) != 0)
            {
                hexString = "0" + hexString;
            }

            byte[] result;
            try
            {
                SoapHexBinary shb = SoapHexBinary.Parse(hexString);
                result = shb.Value;
            }
            catch(Exception exc)
            {
                LogManager.ShowMessageBox("Error", "Wrong format", exc.Message);
                result = null;
            }
            return result;
        }
    }
}
