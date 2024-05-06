namespace SecurityCasus
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class EncryptionHelper
    {
        private const string EncryptionKey = "GeheimeSleutel"; 

        public static string EncryptData(string data)
        {
            byte[] encryptedBytes;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey);
                aesAlg.IV = aesAlg.Key.Take(16).ToArray(); 

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(data);
                        }
                        encryptedBytes = msEncrypt.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encryptedBytes);
        }

        public static string DecryptData(string encryptedData)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedData);
            string decryptedData;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey);
                aesAlg.IV = aesAlg.Key.Take(16).ToArray(); 

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            decryptedData = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return decryptedData;
        }
    }
}
