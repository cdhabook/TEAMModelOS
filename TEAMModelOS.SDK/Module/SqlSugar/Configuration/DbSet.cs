using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Module.SqlSugar.Configuration
{
    /// <summary>
    /// 扩展ORM
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DbSet<T> : SimpleClient<T> where T : class, new()
    {
        public DbSet(SqlSugarClient context) : base(context)
        {

        }
        /// <summary>
        /// 扩展假删除功能
        /// </summary>
        /// <typeparam name="DbModel"></typeparam>
        /// <param name="dbModel"></param>
        /// <returns></returns>
        public bool FalseDelete<DbModel>(DbModel dbModel) where DbModel : BaseDbModel, new()
        {
            return this.Context.Updateable<DbModel>(dbModel).UpdateColumns(it => new DbModel() { IsDel = true }).ExecuteCommand() > 0;
        }
        /// <summary>
        /// 扩展假删除功能
        /// </summary>
        /// <typeparam name="DbModel"></typeparam>
        /// <param name="dbModel"></param>
        /// <returns></returns>
        public bool FalseDelete<DbModel>(DbModel[] dbModels) where DbModel : BaseDbModel, new()
        {
            return this.Context.Updateable<DbModel>(dbModels).UpdateColumns(it => new DbModel() { IsDel = true }).ExecuteCommand() > 0;
        }
    }
}
