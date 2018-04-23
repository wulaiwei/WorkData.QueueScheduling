// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Util.Common
// 文件名：CryptoRandomGenerator.cs
// 创建标识：吴来伟 2018-04-08 14:53
// 创建描述：
//
// 修改标识：吴来伟2018-04-08 14:53
// 修改描述：
//  ------------------------------------------------------------------------------

using System;
using System.Security.Cryptography;

namespace WorkData.Util.Common.VerificationCode
{
    public class CryptoRandomGenerator : RandomNumberGenerator
    {
        private static RandomNumberGenerator _rng;
        private static CryptoRandomGenerator _instance;
        private static readonly object SyncHelper = new object();

        public CryptoRandomGenerator()
        {
            _rng = Create();
        }

        public static CryptoRandomGenerator Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (SyncHelper)
                {
                    _instance = new CryptoRandomGenerator();
                }

                return _instance;
            }
        }

        public override void GetBytes(byte[] data)
        {
            _rng.GetBytes(data);
        }

        public override void GetNonZeroBytes(byte[] data)
        {
            _rng.GetNonZeroBytes(data);
        }

        /// <summary>
        ///     返回一个在 0.0-1.0之间的随机数
        /// </summary>
        /// <returns></returns>
        public double NextDouble()
        {
            var bytes = new byte[4];
            _rng.GetBytes(bytes);
            return (double)BitConverter.ToUInt32(bytes, 0) / uint.MaxValue;
        }

        /// <summary>
        ///     返回一个指定范围内的随机数
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public int Next(int minValue, int maxValue)
        {
            return (int)Math.Round(NextDouble() * (maxValue - minValue - 1)) + minValue;
        }

        /// <summary>
        ///     返回一个小于maxValue的非负随机数
        /// </summary>
        /// <param name="maxValue">maxValue 必须大于或等于 0</param>
        /// <returns></returns>
        public int Next(int maxValue)
        {
            return Next(0, maxValue);
        }

        /// <summary>
        ///     返回一个非负的随机数
        /// </summary>
        /// <returns></returns>
        public int Next()
        {
            return Next(0, int.MaxValue);
        }
    }
}