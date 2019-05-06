using TEAMModelOS.SDK.Extension.JwtAuth.Models;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;
using TEAMModelOS.SDK.Context.Configuration;
using TEAMModelOS.SDK.Helper.Security.RSACrypt;
using TEAMModelOS.SDK.Extension.JwtAuth.Requirements;

namespace TEAMModelOS.SDK.Extension.JwtAuth
{
    public static class JwtAuthExtension
    {
        public static void JwtAuth(this IServiceCollection services , IConfigurationSection configuration)
        {
            services.Configure<JwtSetting>(configuration);
            // var creds = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecurityKey"]), SecurityAlgorithms.RsaSha256Signature);
            //var creds = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecurityKey"]));
            string path = BaseConfigModel.ContentRootPath;
            RsaSecurityKey creds = new RsaSecurityKey(RsaHelper.LoadCertificateFile(path + "/JwtRsaFile/private.pem"));
            //RsaSecurityKey creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecurityKey"])), SecurityAlgorithms.RsaSha256Signature);
            // 令牌验证参数
            var tokenValidationParameters = new TokenValidationParameters
            {
                NameClaimType = JwtClaimTypes.Name,
                RoleClaimType = JwtClaimTypes.Role,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = creds,
                ValidateIssuer = true,
                ValidIssuer = configuration["Issuer"],//发行人
                ValidateAudience = true,
                ValidAudience = configuration["Audience"],//订阅人
                                                           // 是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                ValidateLifetime = true,
                //允许的服务器时间偏移量
                ClockSkew = TimeSpan.Zero,
                //是否要求Token的Claims中必须包含Expires
                RequireExpirationTime = true,
            };
            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = tokenValidationParameters;
                o.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        // 如果过期，则把<是否过期>添加到，返回头信息中
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    },
                    //Url中添加access_token=[token]，直接在浏览器中访问
                    OnMessageReceived = context => {
                        context.Token = context.Request.Query["access_token"];
                        return Task.CompletedTask;
                    },
                    //URL未授权调用
                    OnChallenge = context => {
                        return Task.CompletedTask;
                    },
                    //在Token验证通过后调用
                    OnTokenValidated = context => {
                        //编写业务
                        return Task.CompletedTask;
                    },

                };
            });
            //自定义授权
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Admin", policy => policy.RequireRole("Admin,Root,SchoolAdmin,Teacher").Build());
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .AddRequirements(new ValidJtiRequirement()) // 添加上面的验证要求
                    .Build());
            });
            // 注册验证要求的处理器，可通过这种方式对同一种要求添加多种验证
            services.AddSingleton<IAuthorizationHandler, ValidJtiHandler>();
        }
        
    }
}
