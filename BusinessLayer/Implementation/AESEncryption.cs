using Ecommerce.BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Ecommerce.BusinessLayer.Implementation
{
    public class AESEncryption:IEncryption
    {
        private static AESEncryption aesEncryption;
        private static byte[] encryptionKey;
        private AESEncryption()
        {
            // Private constructor to prevent instantiation from outside   
            encryptionKey = null;
        }

        public static AESEncryption CreateEncryptionKey()
        {
            if(aesEncryption == null)
            {
                try
                {
                    aesEncryption = new AESEncryption();
                    using (Aes aes = Aes.Create())
                    {
                        aes.GenerateKey();
                        encryptionKey =  aes.Key;
                    }
                }
                catch(Exception ex) { 

                }
            }

            return aesEncryption;
        }


        public byte[] DecryptFile(byte[] filedata)
        {

            using (Aes aes = Aes.Create())
            {
                aes.Key = encryptionKey;

                // Get the IV from the encrypted data
                byte[] iv = new byte[aes.IV.Length];
                Array.Copy(filedata, 0, iv, 0, iv.Length);
                aes.IV = iv;

                byte[] decrypted;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        // Write the encrypted data (excluding the IV) to the CryptoStream
                        cryptoStream.Write(filedata, iv.Length, filedata.Length - iv.Length);
                        cryptoStream.FlushFinalBlock();
                    }

                    decrypted = memoryStream.ToArray();
                }

                return decrypted;
            }
        }

        public byte[] EncryptFile(byte[] fileData)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = encryptionKey;
                aes.GenerateIV();

                byte[] encrypted;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    memoryStream.Write(aes.IV, 0, aes.IV.Length);

                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(fileData, 0, fileData.Length);
                        cryptoStream.FlushFinalBlock();
                    }

                    encrypted = memoryStream.ToArray();
                }

                return encrypted;
            }
        }


    }
}