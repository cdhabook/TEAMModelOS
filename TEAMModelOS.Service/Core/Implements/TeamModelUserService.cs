using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TEAMModelOS.Model.Core;
using TEAMModelOS.SDK.Module.SqlSugar.Configuration;
using TEAMModelOS.SDK.Module.SqlSugar.Configuration.Data;
using TEAMModelOS.SDK.Module.SqlSugar.Implements;
using TEAMModelOS.Service.Core.Interfaces;

namespace TEAMModelOS.Service.Core.Implements
{
    public class TeamModelUserService : BaseServer<TeamModelUser>, ITeamModelUserService
    {
        public Task<ApiResult<TeamModelUser>> GetByGuidAsync(string parm)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<TeamModelUser>> LoginAsync(TeamModelUser parm)
        {
            throw new NotImplementedException();
        }
        public DbSet<TeamModelUser> TeamModelUserDB => new DbSet<TeamModelUser>(Db);
        public async Task<ApiResult<string>> ModifyAsync(TeamModelUser parm)
        {
            var res = new ApiResult<string>
            {
                statusCode = 200
            };
            try
            {
                //修改，判断用户是否和其它的重复
                var isExisteName = TeamModelUserDB.IsAny(m => m.TeamModelId == parm.TeamModelId && m.RowKey != parm.RowKey);
                if (isExisteName)
                {
                    res.message = "用户名已存在，请更换~";
                    res.statusCode = (int)ApiEnum.ParameterError;
                    return await Task.Run(() => res);
                }

                //parm.LoginPwd = DES3Encrypt.EncryptString(parm.LoginPwd);
                //if (!string.IsNullOrEmpty(parm.DepartmentGuid))
                //{
                //    // 说明有父级  根据父级，查询对应的模型
                //    var model = SysOrganizeDb.GetById(parm.DepartmentGuid);
                //    parm.DepartmentGuidList = model.ParentGuidList;
                //}
                //var dbres = Db.Updateable<TeamModelUser>().UpdateColumns(m => new TeamModelUser()
                //{
                //    LoginName = parm.LoginName,
                //    LoginPwd = parm.LoginPwd,
                //    DepartmentName = parm.DepartmentName,
                //    DepartmentGuid = parm.DepartmentGuid,
                //    DepartmentGuidList = parm.DepartmentGuidList,
                //    TrueName = parm.TrueName,
                //    Number = parm.Number,
                //    Sex = parm.Sex,
                //    Mobile = parm.Mobile,
                //    Email = parm.Email,
                //    Status = parm.Status
                //}).Where(m => m.Guid == parm.Guid).ExecuteCommand();
                //if (dbres > 1)
                //{
                //    res.statusCode = (int)ApiEnum.Error;
                //    res.message = "更新失败！";
                //}
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
            }
            return await Task.Run(() => res);
        }
    }
}
