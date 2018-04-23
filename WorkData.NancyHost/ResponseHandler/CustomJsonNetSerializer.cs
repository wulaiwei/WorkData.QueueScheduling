// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.NancyHost
// 文件名：CustomJsonNetSerializer.cs
// 创建标识：吴来伟 2018-03-27 15:09
// 创建描述：
//
// 修改标识：吴来伟2018-03-27 15:09
// 修改描述：
//  ------------------------------------------------------------------------------

using Nancy;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;

namespace WorkData.NancyHost.ResponseHandler
{
    /// <summary>
    /// 重写序列化 Newtonsoft.Json
    /// </summary>
    public sealed class CustomJsonNetSerializer : JsonSerializer, ISerializer
    {
        public CustomJsonNetSerializer()
        {
            ContractResolver = new DefaultContractResolver();
            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
            DateFormatString = "yyyy-MM-dd HH:mm:ss";
            Formatting = Formatting.None;
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full;
            //忽略空项
            //NullValueHandling = NullValueHandling.Ignore;
        }

        public bool CanSerialize(string contentType)
        {
            return contentType.Equals("application/json", StringComparison.OrdinalIgnoreCase);
        }

        public void Serialize<TModel>(string contentType, TModel model, Stream outputStream)
        {
            using (var streamWriter = new StreamWriter(outputStream))
            {
                using (var jsonWriter = new JsonTextWriter(streamWriter))
                {
                    Serialize(jsonWriter, model);
                }
            }
        }

        public IEnumerable<string> Extensions { get { yield return "json"; } }
    }
}