using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CSharp;
using System.Web.Script.Serialization;

namespace _70_483
{
  
    public class Program
    {
        public static void EncryptSomeText(string data)
        {
            var original = data;
            using (SymmetricAlgorithm symmetricAlgorithm = new AesManaged())
            {
                var encrypted = Encrypt(symmetricAlgorithm, original);
                var decrypted = Decrypt(symmetricAlgorithm, encrypted);

                Console.WriteLine("Original: {0}",original);
                Console.WriteLine("Encrypted: {0}",Encoding.UTF8.GetString(encrypted));
                Console.WriteLine("Decrypted: {0}", decrypted);
            }
        }

        private static string Decrypt(SymmetricAlgorithm aesAlg, byte[] cipherText)
        {
            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            using (var msDecryption = new MemoryStream(cipherText))
            {
                using (var csDecrypt = new CryptoStream(msDecryption,decryptor,CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        public static byte[] Encrypt(SymmetricAlgorithm aesAlg, string plainText)
        {
            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using (var msEncryption = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncryption,encryptor,CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return msEncryption.ToArray();
                }
            }
        }

        private static void Main(string[] args)
        {
            var rsa = new RSACryptoServiceProvider();
            var publicKeyXml = rsa.ToXmlString(false);
            var privateKeyXml = rsa.ToXmlString(true);
            var ByteConverter = new UnicodeEncoding();
            var dataToEncrypt = ByteConverter.GetBytes("My secret data");
            byte[] encryptedData;
            using (var RSA = new RSACryptoServiceProvider())
            {
                RSA.FromXmlString(publicKeyXml);
                encryptedData = RSA.Encrypt(dataToEncrypt,false);
            }

            byte[] decryptedData;
            using (var RSA = new RSACryptoServiceProvider())
            {
                RSA.FromXmlString(privateKeyXml);
                decryptedData = RSA.Decrypt(encryptedData, false);
            }

            var decryptedString = ByteConverter.GetString(decryptedData);
            Console.WriteLine(decryptedString);

            //EncryptSomeText("My secret data");
        }

    }
}
