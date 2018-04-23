// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.NancyHost
// 文件名：UpdateMessageStatusRequest.cs
// 创建标识：吴来伟 2018-04-20 14:22
// 创建描述：
//
// 修改标识：吴来伟2018-04-20 14:22
// 修改描述：
//  ------------------------------------------------------------------------------
namespace WorkData.NancyHost.Models.Messages
{
    public class UpdateMessageStatusRequest
    {
        public string Id { get; set; }

        public bool Successed { get; set; }

        public string Content { get; set; }
    }
}