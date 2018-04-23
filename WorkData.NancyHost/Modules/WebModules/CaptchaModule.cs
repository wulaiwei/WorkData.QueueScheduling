// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.NancyHost
// 文件名：CaptchaModule.cs
// 创建标识：吴来伟 2018-04-08 14:58
// 创建描述：
//
// 修改标识：吴来伟2018-04-08 14:58
// 修改描述：
//  ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.SessionState;
using WorkData.NancyHost.Infrastructure.NancyModuleExtends;
using WorkData.NancyHost.ResponseHandler;
using WorkData.Util.Common.VerificationCode;

namespace WorkData.NancyHost.Modules.WebModules
{
    public class CaptchaModule : WebNancyModule
    {
        //注意：这里是构造函数
        public CaptchaModule()
        {
            //获取验证码
            Get["/captcha/get"] = p =>
            {
                var stream = Captcha.CreateCaptcha(HttpContext.Current.Session);
                var data = Response.FromByteArray(stream.ToArray(), @"image/png");
                return data;
            };
        }
    }

    public class Captcha
    {
        private static readonly char[] CharArray =
        {
            '2', '3', '4', '5', '6', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
            'H', 'J', 'K', 'M', 'N', 'P', 'R', 'S', 'U', 'W', 'X', 'Y'
        };

        private static readonly List<Color> ColorList = new List<Color>
        {
            Color.Red,
            Color.Orange,
            Color.Brown,
            Color.Green,
            Color.DarkCyan,
            Color.Blue,
            Color.Purple
        };

        /// <summary>
        ///     验证码值
        /// </summary>
        public string CaptchaValue { get; set; }

        /// <summary>
        ///     创建验证码
        /// </summary>
        /// <returns></returns>
        public static MemoryStream CreateCaptcha(HttpSessionState session)
        {
            var validateCode = CreateRandomCode(4);
            session["ValidateCode"] = validateCode;
            return CreateImage(validateCode);
        }

        /// <summary>
        ///     生成随机数
        /// </summary>
        private static int GetRandomInteger(int min, int max)
        {
            var tick = Guid.NewGuid().GetHashCode();
            var seed = (int)(tick & 0xffffffffL) | tick >> 32;
            var random = new Random(seed);
            return random.Next(min, max);
        }

        /// <summary>
        ///     验证验证码
        /// </summary>
        /// <param name="session"></param>
        /// <param name="validateCode"></param>
        /// <returns></returns>
        public static CaptchaValidateResult VerifyCaptcha(HttpSessionStateBase session, string validateCode)
        {
            var result = new CaptchaValidateResult { VerifyResult = false };
            if (session == null || session["ValidateCode"] == null || validateCode == null) return result;
            var holdCode = session["ValidateCode"].ToString().Trim();
            validateCode = validateCode.ToUpper().Trim();
            result.VerifyResult = holdCode.Equals(validateCode);

            return result;
        }

        /// <summary>
        ///     生成验证码字符串
        /// </summary>
        /// <param name="length">位数</param>
        /// <returns>验证码字符串</returns>
        private static string CreateRandomCode(int length)
        {
            var randomCode = new List<char>(4);
            do
            {
                var pos = CryptoRandomGenerator.Instance.Next(CharArray.Length);
                var code = CharArray[pos];
                if (randomCode.Contains(code))
                {
                    continue;
                }
                randomCode.Add(code);
            } while (randomCode.Count < length);

            return string.Join("", randomCode);
        }

        #region 产生波形滤镜效果

        /// <summary>
        ///     波形滤镜
        /// </summary>
        /// <param name="originBmp">原图片</param>
        /// <param name="distortion">如果扭曲则选择为True</param>
        /// <param name="waveform">波形类别 1=正弦波 2=余弦波</param>
        /// <param name="extenValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>
        /// <param name="beginPhase">波形的起始相位，取值区间[0-2PI)</param>
        /// <returns></returns>
        public static MemoryStream WaveFilter(Bitmap originBmp, bool distortion, int waveform, double extenValue, double beginPhase)
        {
            //相位限制
            const double phaseLimit = Math.PI * 2;
            using (var destBmp = new Bitmap(originBmp.Width, originBmp.Height))
            {
                // 将位图背景填充为白色
                using (var graph = Graphics.FromImage(destBmp))
                {
                    graph.Clear(Color.White);
                    var dBaseAxisLen = distortion ? destBmp.Height : (double)destBmp.Width;
                    for (var i = 0; i < destBmp.Width; i++)
                    {
                        for (var j = 0; j < destBmp.Height; j++)
                        {
                            var destX = (distortion ? (phaseLimit * j) / dBaseAxisLen : (phaseLimit * i) / dBaseAxisLen) + beginPhase;
                            var destY = waveform == 1 ? Math.Sin(destX) : Math.Cos(destX);

                            // 取得当前点的颜色
                            var oldX = distortion ? i + (int)(destY * extenValue) : i;
                            var oldY = distortion ? j : j + (int)(destY * extenValue);

                            var color = originBmp.GetPixel(i, j);
                            if (oldX >= 0 && oldX < destBmp.Width && oldY >= 0 && oldY < destBmp.Height)
                            {
                                destBmp.SetPixel(oldX, oldY, color);
                            }
                        }
                    }

                    graph.DrawRectangle(new Pen(Color.LightGray), 0, 0, destBmp.Width - 1, destBmp.Height - 1);

                    var imageStream = new MemoryStream();
                    destBmp.Save(imageStream, ImageFormat.Bmp);

                    return imageStream;
                }
            }
        }

        #endregion 产生波形滤镜效果

        /// <summary>
        ///     创建验证码图片
        /// </summary>
        /// <param name="checkCode"></param>
        /// <returns></returns>
        private static MemoryStream CreateImage(string checkCode)
        {
            using (var image = new Bitmap(80, 30))
            {
                using (var graph = Graphics.FromImage(image))
                {
                    graph.Clear(Color.White); // 填充文字

                    var font = new Font("Arial", 16, (FontStyle.Italic), GraphicsUnit.Point); // 字体
                    for (var i = 0; i < checkCode.Length; i++)
                    {
                        var str = Convert.ToString(checkCode[i]);
                        var colorIndex = CryptoRandomGenerator.Instance.Next(0, 7);
                        Brush brush = new SolidBrush(ColorList[colorIndex]); // 字体颜色
                        graph.DrawString(str, font, brush, 5 + i * 16, 1);
                    }

                    #region 随机正弦/余弦 波形滤镜

                    var distortion = CryptoRandomGenerator.Instance.Next(0, 1) == 0;
                    var waveFrom = CryptoRandomGenerator.Instance.Next(1, 2);
                    var extenValue = CryptoRandomGenerator.Instance.Next(3, 5);
                    var beginPhase = CryptoRandomGenerator.Instance.Next(3, 6);

                    return WaveFilter(image, distortion, waveFrom, extenValue, beginPhase);

                    #endregion 随机正弦/余弦 波形滤镜
                }
            }
        }

        /// <summary>
        ///     图灵测试结果
        /// </summary>
        public struct CaptchaValidateResult
        {
            public bool VerifyResult { get; set; }
        }
    }
}