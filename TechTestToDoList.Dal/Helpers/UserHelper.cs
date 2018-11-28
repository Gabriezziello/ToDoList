using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TechTestToDoList.Dal.Helpers
{
    public static class UserHelper
    {
       
        public static string Encrypt(string stringToEncrypt, string username)
        {
            
            string encryptedString = string.Empty;

            try
            {
                byte[] utfdata = UTF8Encoding.UTF8.GetBytes(stringToEncrypt);
                byte[] saltBytes = UTF8Encoding.UTF8.GetBytes("eyyEihG1M4");
                AesManaged aes = new AesManaged();

                // We're using the PBKDF2 standard for password-based key generation
                Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(username, saltBytes);

                // Setting our parameters
                aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
                aes.KeySize = aes.LegalKeySizes[0].MaxSize;
                aes.Key = rfc.GetBytes(aes.KeySize / 8);
                aes.IV = rfc.GetBytes(aes.BlockSize / 8);

                // Encryption
                ICryptoTransform encryptTransf = aes.CreateEncryptor();

                // Output stream, can be also a FileStream
                MemoryStream encryptStream = new MemoryStream();
                CryptoStream encryptor = new CryptoStream(encryptStream, encryptTransf, CryptoStreamMode.Write);
                encryptor.Write(utfdata, 0, utfdata.Length);
                encryptor.Flush();
                encryptor.Close();

                // Showing our encrypted content
                byte[] encryptBytes = encryptStream.ToArray();
                encryptedString = ConvertStringToHex(Convert.ToBase64String(encryptBytes));
            }
            catch(Exception e)
            {
                encryptedString = string.Empty;
            }

            return encryptedString;
        }

        public static string ConvertHexToString(string hexValue)
        {

            string strValue = "";

            while (hexValue.Length > 0)

            {

                strValue += Convert.ToChar(Convert.ToUInt32(hexValue.Substring(0, 2), 16)).ToString();

                hexValue = hexValue.Substring(2, hexValue.Length - 2);

            }

            return strValue;

        }

        private static string ConvertStringToHex(string asciiString)
        {

            string hex = "";

            foreach (char c in asciiString)

            {

                int tmp = c;

                hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));

            }

            return hex;

        }
    }
}
