// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.NancyHost
// 文件名：MessageModule.cs
// 创建标识：吴来伟 2018-04-20 10:30
// 创建描述：
//
// 修改标识：吴来伟2018-04-20 12:15
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using Nancy.ModelBinding;
using Polly;
using System;
using WorkData.NancyHost.Cache;
using WorkData.NancyHost.Infrastructure.NancyModuleExtends;
using WorkData.NancyHost.Models.Messages;
using WorkData.NancyHost.ResponseHandler;
using WorkData.Service.Domain.Messges;
using WorkData.Service.Domain.Messges.Services;
using WorkData.Util.Common.ExceptionExtensions;
using WorkData.Util.Redis.Entity;

#endregion

namespace WorkData.NancyHost.Modules.ApiModules
{
    /// <summary>
    ///     MessageModule
    /// </summary>
    public class MessageModule : ApiNancyModule
    {
        public MessageModule(IMessageService messageService, ICacheManager cacheManager)
        {
            //新增消息
            Post["/message/add"] = x =>
            {
                try
                {
                    var insertMessage = this.Bind<InsertMessageRequest>();
                    if (insertMessage == null)
                        return Response.AsErrorJson("非法访问！");
                    insertMessage.Id = Guid.NewGuid().ToString();

                    //需设置为Config
                    var retryPolicy =
                        Policy
                            .Handle<Exception>()
                            .Retry(3, (ex, count) => { });

                    retryPolicy
                        .Execute(() =>
                        {
                            var message = new Message
                            {
                                Id = insertMessage.Id,
                                Key = insertMessage.Key,
                                DomainService = insertMessage.DomainService,
                                ExpireTime = insertMessage.ExpireTime,
                                MessageType = insertMessage.MessageType,
                                RequestJson = insertMessage.RequestJson
                            };
                            messageService.AddMessage(message);
                            var redisQueue = new RedisQueue<InsertMessageRequest>
                            {
                                Key = "QueueScheduling",
                                Entity = insertMessage
                            };
                            cacheManager.AddQueue(redisQueue);
                        });
                    return Response.AsSuccessJson("添加成功！");
                }
                catch (Exception e)
                {
                    return Response.AsErrorJson($"执行异常，错误原因为: ({e.Message})");
                }
            };

            //获取消息列表
            Get["/message/query"] = arg =>
            {
                var list = messageService.Query();
                return Response.AsSuccessJson(list);
            };

            //更新数据
            Post["/message/update"] = arg =>
            {
                try
                {
                    var request = this.Bind<UpdateMessageStatusRequest>();
                    messageService.UpdateMessage(request.Id, request.Successed, request.Content);

                    return Response.AsSuccessJson("操作成功");
                }
                catch (Exception e)
                {
                    throw new UserFriendlyException(e.Message);
                }
            };
        }
    }
}