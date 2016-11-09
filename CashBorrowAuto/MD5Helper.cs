using System;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApplication1
{
    public class MD5Helper
    {

        /// <summary>
        /// MD5加密，返回MD5 16位或32位加密后的字符串，默认返回32位。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Encrypt(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            return BitConverter.ToString(result).Replace("-", "").ToLower();
        }

    }
}
