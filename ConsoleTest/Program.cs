using Newtonsoft.Json;
using ServiceStack.Text;
using System;
using WorkData.Service.Application.Impls;
using WorkData.Service.Core.Entity;

namespace ConsoleTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            for (var i = 0; i < 1; i++)
            {
                var message = new Message
                {
                    DomainService = i % 2 == 1 ? typeof(UpdateDimainService) : typeof(InsertDomainService),
                    ExpireTime = DateTime.Now.AddMinutes(1),
                    Key = Guid.NewGuid().ToString(),
                    MessageType = i % 2 == 1 ? MessageType.定时回调 : MessageType.直接执行,
                    Successed = false
                };
                var res = "http://10.130.0.196:8011/api/message/add".
                    PostStringToUrl(JsonConvert.SerializeObject(message), WebRequestExtensions.Json);
            }
            Console.ReadKey();
        }
    }
}