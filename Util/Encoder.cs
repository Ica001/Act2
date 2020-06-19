
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CainAbel.Util
{
    public static class Encoder
    {
        public static string Md5_encrypt(string text)
        {
            using (MD5CryptoServiceProvider md5=new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(text));
                return Convert.ToBase64String(data);
            }

        }
    }
}
