using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TEAMModelOS.Model.Core;
using TEAMModelOS.SDK.Module.SqlSugar.Configuration.Data;
using TEAMModelOS.SDK.Module.SqlSugar.Interfaces;

namespace TEAMModelOS.Service.Core.Interfaces
{
    public interface ITeamModelUserService :IBusinessService , IBaseServer<TeamModelUser>
    {/// <summary>
     /// 登录
     /// </summary>
     /// <param name="parm"></param>
     /// <returns></returns>
        Task<ApiResult<TeamModelUser>> LoginAsync(TeamModelUser parm);

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        new Task<ApiResult<Page<TeamModelUser>>> GetPagesAsync(PageParm parm);

        /// <summary>
        /// 获得一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<TeamModelUser>> GetByGuidAsync(string parm);

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        new Task<ApiResult<string>> AddAsync(TeamModelUser parm);

        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <returns></returns>
        new Task<ApiResult<string>> DeleteAsync(string parm);

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> ModifyAsync(TeamModelUser parm);
    }
}
