
namespace TEAMModelOS.SDK.Helper.Security.BCryptHelper
{
    /// <summary>
    /// BCrypt散列加密算法
    /// </summary>
    public class BCryptHelper
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string Ecrypt(string code)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(code);
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="code"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static bool Verify(string code, string encode)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(code, encode);
        }
    }
}
