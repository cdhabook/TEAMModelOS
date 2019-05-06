using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TEAMModelOS.SDK.Helper.Security.Md5Hash
{
    public  class Md5Hash
    {
        #region MD5加密字符串处理
        /// <summary>
        /// MD5加密字符串处理
        /// </summary>
        /// <param name="half">加密是16位还是32位；如果为true为16位</param>
        /// <param name="input">待加密码字符串</param>
        /// <returns></returns>
        public static string Encrypt(string input, bool half)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                var strResult = BitConverter.ToString(result);
                strResult = strResult.Replace("-", "");
                if (half)//16位MD5加密（取32位加密的9~25字符）
                {
                    strResult = strResult?.Substring(8, 16);
                }
                return strResult;
            }
        }
        #endregion

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="strPwd">加密的字符串</param>
        /// <returns></returns>
        public static string Encrypt(string strPwd)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.Default.GetBytes(strPwd);
            byte[] result = md5.ComputeHash(data);
            string ret = "";
            for (int i = 0; i < result.Length; i++)
                ret += result[i].ToString("x").PadLeft(2, '0');
            return ret;
        }

        public static string GetMd5String(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] data = Encoding.UTF8.GetBytes(str);
            byte[] enData = md5.ComputeHash(data);
            return GetbyteToString(enData);
        }

        public static string GetMD5String(string str, HashAlgorithm hash)
        {

            byte[] data = Encoding.UTF8.GetBytes(str);
            byte[] data2 = hash.ComputeHash(data);
            return GetbyteToString(data2);
        }

        public static string GetMD5FromFile(string path)
        {
            MD5 md5 = MD5.Create();
            if (!File.Exists(path))
            {
                return "";
            }
            FileStream stream = File.OpenRead(path);
            byte[] data2 = md5.ComputeHash(stream);
            return GetbyteToString(data2);
            //return BitConverter.ToString(data2).Replace("-", "").ToLower();
        }
        private static string GetbyteToString(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
