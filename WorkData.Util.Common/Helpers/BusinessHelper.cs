// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Util.Common
// 文件名：BusinessHelper.cs
// 创建标识：吴来伟 2018-03-29 15:33
// 创建描述：
//
// 修改标识：吴来伟2018-03-29 15:33
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using System;
using System.Globalization;
using System.Text.RegularExpressions;

#endregion

namespace WorkData.Util.Common.Helpers
{
    public class BusinessHelper
    {
        /// <summary>
        ///     生成指定字符串
        /// </summary>
        /// <param name="array"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Combination(long[] array, string key)
        {
            return string.Join(key, array);
        }

        /// <summary>
        ///     拆分数组
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static long[] BreakUpStrLong(string str, char key)
        {
            var strArray = str.Split(new[] { key }, StringSplitOptions.RemoveEmptyEntries);
            return Array.ConvertAll(strArray, long.Parse);
        }

        /// <summary>
        ///     拆分数组
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int[] BreakUpStr(string str, char key)
        {
            var strArray = str.Split(new[] { key }, StringSplitOptions.RemoveEmptyEntries);
            return Array.ConvertAll(strArray, int.Parse);
        }

        /// <summary>
        ///     拆分数组
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string[] BreakUpOptions(string str, char key)
        {
            var strArray = str.Split(new[] { key }, StringSplitOptions.RemoveEmptyEntries);
            return strArray;
        }

        #region 生成指定长度的字符串

        /// <summary>
        ///     生成指定长度的字符串,即生成strLong个str字符串
        /// </summary>
        /// <param name="strLong">生成的长度</param>
        /// <param name="str">以str生成字符串</param>
        /// <returns></returns>
        public static string StringOfChar(int strLong, string str)
        {
            var returnStr = "";
            for (var i = 0; i < strLong; i++)
            {
                returnStr += str;
            }

            return returnStr;
        }

        //_seed
        private static volatile int _seed = 1;

        /// <summary>
        ///     GetSequence
        /// </summary>
        /// <param name="sequenceLength"></param>
        /// <returns></returns>
        private static string GetSequence(int sequenceLength)
        {
            _seed = (++_seed < 1000) ? _seed : 1;
            return _seed.ToString(CultureInfo.InvariantCulture).PadLeft(sequenceLength, '0');
        }

        /// <summary>
        ///     获取Trans
        /// </summary>
        /// <returns></returns>
        public static string CreateTransId(int sequenceLength = 0)
        {
            return $"{DateTime.Now:yyyyMMddHHmmss}{GetSequence(sequenceLength)}";
        }

        #endregion 生成指定长度的字符串

        /// <summary>
        ///     生成流水号
        /// </summary>
        /// <returns></returns>
        public static string GenerateSerialNumber(string prefixStr, int length, DateTime createTime)
        {
            //流水域长度：

            string BuildTemplete(string prefix, int seedLength) =>
                $"{prefix}{createTime:yyyyMMddHHmmss}{GetSequence(seedLength)}";

            var paymentSerialNumber = "";
            paymentSerialNumber = BuildTemplete(prefixStr, length);

            return paymentSerialNumber;
        }

        /// <summary>
        ///     手机号处理
        /// </summary>
        /// <param name="phoneNo"></param>
        /// <returns></returns>
        /// <remarks>手机号：18583706963 处理返回：185****6963</remarks>
        public static string PhoneProcess(string phoneNo)
        {
            if (string.IsNullOrWhiteSpace(phoneNo))
                return "";
            if (phoneNo.Length == 11)
            {
                var re = new Regex("(\\d{3})(\\d{4})(\\d{4})", RegexOptions.None);
                phoneNo = re.Replace(phoneNo, "$1****$3");
            }
            else
            {
                if (phoneNo.Length >= 4)
                {
                    var subStr = phoneNo.Substring(3, phoneNo.Length - 3);
                    phoneNo = phoneNo.Replace(subStr, "****");
                }
            }

            return phoneNo;
        }
    }
}