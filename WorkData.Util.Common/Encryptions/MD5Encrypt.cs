// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Util.Common
// 文件名：MD5Encrypt.cs
// 创建标识：吴来伟 2018-03-29 15:30
// 创建描述：
//
// 修改标识：吴来伟2018-03-29 15:30
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using System.Security.Cryptography;
using System.Text;

#endregion

namespace WorkData.Util.Common.Encryptions
{
    public class Md5Encrypt
    {
        #region 加密算法

        /// <summary>
        ///     MD5码加密算法
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Md5Hash(string password)
        {
            var md5 = MD5.Create();
            var sourceBytes = Encoding.UTF8.GetBytes(password);
            var resultBytes = md5.ComputeHash(sourceBytes);
            var buffer = new StringBuilder(resultBytes.Length);
            foreach (var b in resultBytes)
            {
                buffer.Append(b.ToString("x"));
            }
            return buffer.ToString();
        }

        #endregion 加密算法
    }
}