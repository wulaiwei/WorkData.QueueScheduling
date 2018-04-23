// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.NancyHost
// 文件名：WorkDataNancyResponseExtend.cs
// 创建标识：吴来伟 2018-03-28 11:20
// 创建描述：
//
// 修改标识：吴来伟2018-03-28 11:20
// 修改描述：
//  ------------------------------------------------------------------------------

using Nancy;
using WorkData.Util.Common.ResponseExtensions;

namespace WorkData.NancyHost.ResponseHandler
{
    /// <summary>
    /// WorkDataNancyResponseExtend
    /// </summary>
    public static class WorkDataNancyResponseExtend
    {
        /// <summary>
        /// AsSuccess
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Response AsSuccessJson<T>(this IResponseFormatter response, T data) where T : class
        {
            var serverResponse = ResponseProvider.Success(data);
            return response.AsJson(serverResponse);
        }

        /// <summary>
        /// AsError
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Response AsErrorJson<T>(this IResponseFormatter response, T data, string message) where T : class
        {
            var serverResponse = ResponseProvider.Error(data, message);
            return response.AsJson(serverResponse);
        }

        /// <summary>
        /// AsError
        /// </summary>
        /// <param name="response"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Response AsErrorJson(this IResponseFormatter response, string message)
        {
            var serverResponse = ResponseProvider.Error(default(BaseResponseEmpty), message);
            return response.AsJson(serverResponse);
        }

        /// <summary>
        /// FromByteArray
        /// </summary>
        /// <param name="formatter"></param>
        /// <param name="body"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static Response FromByteArray(this IResponseFormatter formatter, byte[] body, string contentType = null)
        {
            return new ByteArrayResponse(body, contentType);
        }
    }
}