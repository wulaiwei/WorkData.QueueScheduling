// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.NancyHost
// 文件名：CustomStatusCode.cs
// 创建标识：吴来伟 2018-03-28 13:52
// 创建描述：
//
// 修改标识：吴来伟2018-03-28 14:11
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using Nancy;
using Nancy.ErrorHandling;
using Nancy.ViewEngines;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace WorkData.NancyHost.ResponseHandler
{
    public class CustomStatusCode : IStatusCodeHandler
    {
        private static readonly string _defaultError = "/Pages/error.html";

        private readonly HttpStatusCode[] _supportedStatusCodes =
            { HttpStatusCode.Forbidden, HttpStatusCode.NotFound,HttpStatusCode.InternalServerError };

        private readonly IViewRenderer _viewRenderer;
        private readonly IDictionary<HttpStatusCode, string> _errorPages;

        public CustomStatusCode(IViewRenderer viewRenderer)
        {
            _errorPages = new Dictionary<HttpStatusCode, string>
            {
                {HttpStatusCode.Forbidden, "/Pages/403.html"},
                {HttpStatusCode.NotFound, "/Pages/404.html"},
                {HttpStatusCode.InternalServerError, "/Pages/error.html"},
            };
            _viewRenderer = viewRenderer;
        }

        /// <summary>
        /// HandlesStatusCode
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return context.Response.ContentType == "text/html"
                && _supportedStatusCodes.Any(s => s == statusCode);
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            Response response;
            switch (statusCode)
            {
                case HttpStatusCode.Forbidden:
                    response = _viewRenderer.RenderView(context,
                        _errorPages.FirstOrDefault(x => x.Key == HttpStatusCode.Forbidden).Value);
                    break;

                case HttpStatusCode.NotFound:
                    response = _viewRenderer.RenderView(context,
                        _errorPages.FirstOrDefault(x => x.Key == HttpStatusCode.NotFound).Value);
                    break;

                default:
                    response = _viewRenderer.RenderView(context, _defaultError);
                    break;
            }

            response.StatusCode = statusCode;
            context.Response = response;
        }
    }
}