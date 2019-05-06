using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using TEAMModelOS.SDK.Context.Configuration;

namespace TEAMModelOS.SDK.Module.SqlSugar.Configuration
{
    public class DbContext
    {
        public DbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = BaseConfigModel.Configuration["DbConnection:MySqlConnectionString"],
                DbType = DbType.MySql,
                IsAutoCloseConnection = true
            });
            //调式代码 用来打印SQL 
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                string s = sql;
                //Console.WriteLine(sql + "\r\n" +
                //    Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                //Console.WriteLine();
            };
        }
        public SqlSugarClient Db;//用来处理事务多表查询和复杂的操作
        public DbSet<DbModel> GetDb<DbModel>() where DbModel : class, new()
        {
            return new DbSet<DbModel>(Db);
        }

    }
}
