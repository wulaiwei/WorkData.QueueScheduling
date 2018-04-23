// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.NancyHost
// 文件名：ByteArrayResponse.cs
// 创建标识：吴来伟 2018-04-08 16:04
// 创建描述：
//
// 修改标识：吴来伟2018-04-08 16:04
// 修改描述：
//  ------------------------------------------------------------------------------

using Nancy;
using System.IO;

namespace WorkData.NancyHost.ResponseHandler
{
    public class ByteArrayResponse : Response
    {
        /// <summary>
        /// Byte array response
        /// </summary>
        /// <param name="body">Byte array to be the body of the response</param>
        /// <param name="contentType">Content type to use</param>
        public ByteArrayResponse(byte[] body, string contentType = null)
        {
            this.ContentType = contentType ?? "application/octet-stream";

            this.Contents = stream =>
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(body);
                }
            };
        }
    }
}