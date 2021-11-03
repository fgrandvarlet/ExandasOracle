using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ExandasOracle.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class CryptoUtil
    {
        const int _ITERATION_COUNT = 2000;

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// nombre d'itérations minimal recommandé = 1000
        /// </remarks>
        /// <param name="password"></param>
        /// <param name="iterations"></param>
        /// <returns></returns>
        private static List<byte[]> GenerateAlgorithmInputs(string password, int iterations)
        {
            byte[] key;
            byte[] iv;

            var result = new List<byte[]>();

            var rfcDb = new Rfc2898DeriveBytes(
                password,
                Encoding.UTF8.GetBytes(password),
                iterations
            );

            key = rfcDb.GetBytes(16);
            iv = rfcDb.GetBytes(16);

            result.Add(key);
            result.Add(iv);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clearText"></param>
        /// <param name="password"></param>
        /// <param name="iterations"></param>
        /// <returns></returns>
        public static string EncryptString(string clearText, string password, int iterations)
        {
            // place le texte à chiffrer dans un tableau d'octets
            byte[] plainText = Encoding.UTF8.GetBytes(clearText);

            List<byte[]> list = GenerateAlgorithmInputs(password, iterations);

            byte[] key = list[0];
            byte[] iv = list[1];

            var rijndael = new RijndaelManaged();
            // définit le mode utilisé
            rijndael.Mode = CipherMode.CBC;

            // crée le chiffreur AES - Rijndael
            ICryptoTransform aesEncryptor = rijndael.CreateEncryptor(key, iv);

            var ms = new MemoryStream();

            // écrit les données chiffrées dans le MemoryStream
            var cs = new CryptoStream(ms, aesEncryptor, CryptoStreamMode.Write);
            cs.Write(plainText, 0, plainText.Length);
            cs.FlushFinalBlock();

            // place les données chiffrées dans un tableau d'octet
            byte[] cipherBytes = ms.ToArray();

            ms.Close();
            cs.Close();

            // place les données chiffrées dans une chaine encodée en Base64
            return Convert.ToBase64String(cipherBytes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="password"></param>
        /// <param name="iterations"></param>
        /// <returns></returns>
        public static string DecryptString(string cipherText, string password, int iterations)
        {
            try
            {
                // place le texte à déchiffrer dans un tableau d'octets
                byte[] cipheredData = Convert.FromBase64String(cipherText);

                List<byte[]> list = GenerateAlgorithmInputs(password, iterations);

                byte[] key = list[0];
                byte[] iv = list[1];

                var rijndael = new RijndaelManaged();
                // définit le mode utilisé
                rijndael.Mode = CipherMode.CBC;

                // écrit les données déchiffrées dans le MemoryStream
                ICryptoTransform decryptor = rijndael.CreateDecryptor(key, iv);
                var ms = new MemoryStream(cipheredData);
                var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);

                // place les données déchiffrées dans un tableau d'octets
                var plainTextData = new byte[cipheredData.Length];

                int decryptedByteCount = cs.Read(plainTextData, 0, plainTextData.Length);

                ms.Close();
                cs.Close();

                return Encoding.UTF8.GetString(plainTextData, 0, decryptedByteCount);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clearText"></param>
        /// <param name="passPhrase"></param>
        /// <returns></returns>
        public static string EncryptIdentifier(string clearText, string passPhrase)
        {
            return EncryptString(clearText, passPhrase, _ITERATION_COUNT);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="passPhrase"></param>
        /// <returns></returns>
        public static string DecryptIdentifier(string cipherText, string passPhrase)
        {
            return DecryptString(cipherText, passPhrase, _ITERATION_COUNT);
        }

    }
}
