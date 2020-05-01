using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ShoppingCMS_V002.OtherClasses
{
    public class Encryption
    {

        public string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public byte[] AesEncrypt(byte[] bytesToEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes;
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            using (var memoryStream = new MemoryStream())
            {
                using (var aes = new RijndaelManaged())
                {
                    aes.KeySize = 256;
                    aes.BlockSize = 128;
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    aes.Key = key.GetBytes(aes.KeySize / 8);
                    aes.IV = key.GetBytes(aes.BlockSize / 8);
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.ANSIX923;
                    using (var cs = new CryptoStream(memoryStream,
                        aes.CreateEncryptor(),
                        CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToEncrypted, 0, bytesToEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = memoryStream.ToArray();
                }
            }
            return encryptedBytes;
        }


        public byte[] AesDecrypt(byte[] bytesToDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes;
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            using (var memoryStream = new MemoryStream())
            {
                using (var aes = new RijndaelManaged())
                {
                    aes.KeySize = 256;
                    aes.BlockSize = 128;
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    aes.Key = key.GetBytes(aes.KeySize / 8);
                    aes.IV = key.GetBytes(aes.BlockSize / 8);

                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.ANSIX923;
                    using (var cs = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToDecrypted, 0, bytesToDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = memoryStream.ToArray();
                }
            }
            return decryptedBytes;
        }

        public string EncryptText(string input, string password)
        {
            // بررسی آرگومان ها
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));
            // تبدیل متن ورودی به آرایه ای از بایت
            var bytesToEncrypted = Encoding.UTF8.GetBytes(input);
            // تبدیل پسورد به آرایه ای از بایت
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            // Hash کردن بایت های پسورد
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            // رمزگذاری بایت های متن ورودی بر اساس پسورد با استفاده از متد AesEcrypt
            var bytesEncrypted = AesEncrypt(bytesToEncrypted, passwordBytes);
            // تبدیل خروجی متد AesEcrypt به رشته 
            var result = Convert.ToBase64String(bytesEncrypted);
            // بازگردانی متن رمزگذاری شده
            return result;
        }


        public string DecryptText(string input, string password)
        {
            // بررسی آرگومان ها
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));
            // تبدیل متن ورودی به آرایه ای از بایت ها
            var bytesToDecrypted = Convert.FromBase64String(input);
            // تبدیل پسورد به آرایه ای از بایت هد
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            // تبدیل بایت های پسورد به Hash
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            // رمزگشایی بایت های متن رمزگذاری شده بر اساس پسورد با استفاده از متد AesDecrypt
            var bytesDecrypted = AesDecrypt(bytesToDecrypted, passwordBytes);
            // تبدیل خروجی متد AesDecrypt به رشته 
            var result = Encoding.UTF8.GetString(bytesDecrypted);
            // بازگردانی متن رمزگشایی شده
            return result;
        }



    }
}