// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Service.Domain
// 文件名：MessageService.cs
// 创建标识：吴来伟 2018-04-20 11:10
// 创建描述：
//
// 修改标识：吴来伟2018-04-20 14:07
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using Dapper;
using System.Collections.Generic;
using WorkData.Util.Common.ExceptionExtensions;

#endregion

namespace WorkData.Service.Domain.Messges.Services
{
    public class MessageService : IMessageService
    {
        /// <summary>
        ///     新增消息
        /// </summary>
        /// <param name="entity"></param>
        public void AddMessage(Message entity)
        {
            using (var db = DbContext.CreateInstance())
            {
                db.Insert(entity);
            }
        }

        /// <summary>
        ///     获取要处理的数据
        /// </summary>
        /// <returns></returns>
        public List<Message> Query()
        {
            using (var db = DbContext.CreateInstance())
            {
                var predicate = Predicates.Group(GroupOperator.And,
                    Predicates.Field<Message>(x => x.IsExecute, Operator.Eq, false));

                var results = db.GetList<Message>(predicate).AsList();
                return results;
            }
        }

        /// <summary>
        /// 更新消息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="successed"></param>
        public void UpdateMessage(string id, bool successed, string content)
        {
            using (var db = DbContext.CreateInstance())
            {
                var messge = db.Get<Message>(id);
                if (messge == null)
                    throw new UserFriendlyException("数据查询失败");
                messge.IsExecute = true;
                messge.Successed = successed;

                if (successed)
                {
                    messge.SuccessMeseage = content;
                }
                else
                {
                    messge.ErrorMessage = content;
                }

                db.Update(messge);
            }
        }
    }
}