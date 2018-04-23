// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Service.Domain
// 文件名：UserBaseService.cs
// 创建标识：吴来伟 2018-03-29 14:51
// 创建描述：
//
// 修改标识：吴来伟2018-03-29 16:34
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using Dapper;
using System;
using System.Linq;
using WorkData.Service.Domain.UserBases.Dtos;
using WorkData.Util.Common.Encryptions;

#endregion

namespace WorkData.Service.Domain.UserBases.Services
{
    public class UserBaseService : IUserBaseService
    {
        /// <summary>
        ///     AddUserBase
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public UserBase AddUserBase(UserBase entity)
        {
            using (var db = DbContext.CreateInstance())
            {
                #region 重构数据

                var salt = Encrypt.GetCheckCode(5);
                entity.Salt = salt;
                entity.Password = DesEncrypt.Encrypt(entity.Password, entity.Salt);
                entity.Id = Guid.NewGuid().ToString();
                entity.CreateTime = DateTime.Now;

                #endregion

                db.Insert(entity);
                return entity;
            }
        }

        /// <summary>
        ///     GetUserBase
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public UserBase GetUserBase(string key)
        {
            using (var db = DbContext.CreateInstance())
            {
                var entity = db.GetList<UserBase>
                    (Predicates.Field<UserBase>(x => x.UserName, Operator.Eq, key))
                    .AsList()
                    .FirstOrDefault();
                return entity;
            }
        }

        public UserBase VerificationUserBase(VerificationUserBaseInputDto input)
        {
            throw new NotImplementedException();
        }
    }
}