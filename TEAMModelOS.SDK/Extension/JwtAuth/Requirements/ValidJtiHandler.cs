using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TEAMModelOS.SDK.Extension.JwtAuth.Models;
using TEAMModelOS.SDK.Module.AzureTable.Interfaces;

namespace TEAMModelOS.SDK.Extension.JwtAuth.Requirements
{
    public class ValidJtiHandler : AuthorizationHandler<ValidJtiRequirement>
    {
        private IAzureTableDBRepository _azureTableDBRepository;

        public ValidJtiHandler(IAzureTableDBRepository azureTableDBRepository)
        {
            _azureTableDBRepository = azureTableDBRepository;
        }
        protected override  Task HandleRequirementAsync(AuthorizationHandlerContext context, ValidJtiRequirement requirement)
        {
            // 检查 Jti 是否存在
            var jti = context.User.FindFirst("jti")?.Value;
            if (jti == null)
            {
                context.Fail(); // 显式的声明验证失败
                return Task.CompletedTask;
            }

            // 检查 jti 是否在黑名单
            // 使用Redis  对于连续访问的token 进行限制
            JwtBlackRecord record =  _azureTableDBRepository.FindOneByKey<JwtBlackRecord>("Jti",jti).Result;
            if (record != null  && !string.IsNullOrEmpty(record.RowKey))
            {
                context.Fail();
            }
            else
            {
                context.Succeed(requirement); // 显式的声明验证成功
            }
            return Task.CompletedTask;

        }
    }
}
