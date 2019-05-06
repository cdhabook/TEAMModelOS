using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TEAMModelOS.Model.Core;
using TEAMModelOS.SDK.Module.SqlSugar.Configuration.Data;
using TEAMModelOS.Service.Core.Interfaces;

namespace TEAMModelOS.Api.Core
{
    [Route("api/user")]
    [ApiController]
    public class TeamModelUserController : Controller
    {
        private readonly ITeamModelUserService _teamModelUserService;
        public TeamModelUserController(ITeamModelUserService teamModelUserService)
        {
            _teamModelUserService = teamModelUserService;
        }


        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("getpages")]
        public async Task<JsonResult> GetPages(PageParm parm)
        {
            var res = await _teamModelUserService.GetPagesAsync(parm);
            return Json(new { code = 0, msg = "success", count = res.data.TotalItems, data = res.data.Items });
        }

        /// <summary>
        /// 获得字典栏目Tree列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ApiResult<string>> AddAdmin(TeamModelUser parm)
        {
            return await _teamModelUserService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<ApiResult<string>> DeleteAdmin(string parm)
        {
            return await _teamModelUserService.DeleteAsync(parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<ApiResult<string>> EditAdmin(TeamModelUser parm)
        {
            return await _teamModelUserService.ModifyAsync(parm);
        }
    }
}
