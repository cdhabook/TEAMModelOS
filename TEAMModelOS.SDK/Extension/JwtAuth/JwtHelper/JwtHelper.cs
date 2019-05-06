using TEAMModelOS.SDK.Extension.JwtAuth.Models;
using IdentityModel;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TEAMModelOS.SDK.Helper.Common.DateTimeHelper;
using TEAMModelOS.SDK.Context.Configuration;
using System.Security.Cryptography;
using TEAMModelOS.SDK.Helper.Security.RSACrypt;

namespace TEAMModelOS.SDK.Extension.JwtAuth.JwtHelper
{
    public class JwtHelper
    {
        /// <summary>
        /// 颁发JWT Token
        /// </summary>
        /// <param name="claimModel"></param>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public static JwtResponse IssueJWT(ClaimModel claimModel, JwtSetting setting)
        {
           // JwtClient jwtClient = null;

            JwtClient jwtClient= setting.JwtClient.Where(x => x.Name.Equals(claimModel.Scope)).First();
            //foreach (JwtClient client in setting.JwtClient) {
            //    if (claimModel.Scope.Equals(client.Name)) {
            //        jwtClient = client;
            //        break; 
            //    }
            //}
            List<Claim> claims = new List<Claim>();
            var dateTime = DateTimeHelper.ConvertToTimeStamp10(DateTime.Now);
            claims.AddRange(claimModel.Claims);
            claims.Add(new Claim(JwtClaimTypes.IssuedAt, dateTime + "", ClaimValueTypes.Integer64));
            claims.Add(new Claim(JwtClaimTypes.NotBefore, dateTime + "", ClaimValueTypes.Integer64));
            claims.Add(new Claim(JwtClaimTypes.Expiration, dateTime + jwtClient.Exp + "", ClaimValueTypes.Integer64));
            claims.Add(new Claim(JwtClaimTypes.Audience, setting.Audience));
            claims.Add(new Claim(JwtClaimTypes.Issuer, setting.Issuer));
            claims.Add(new Claim(JwtClaimTypes.Scope, claimModel.Scope));
            claims.Add(new Claim(JwtClaimTypes.JwtId, Guid.NewGuid().ToString()));
           //claims.AddRange(claimModel.Roles.Select(s=>new Claim(JwtClaimTypes.Role, s)));
            //claims.AddRange(claimModel.Claims.Select(s => new Claim(ClaimTypes.Role, s)));
            string path = BaseConfigModel.ContentRootPath;
            RSACryptoServiceProvider provider = RsaHelper.LoadCertificateFile(path + "/JwtRsaFile/private.pem");
            RsaSecurityKey rsaSecurity = new RsaSecurityKey(provider);
            var creds =new SigningCredentials(rsaSecurity, SecurityAlgorithms.RsaSha256);

            var jwt = new JwtSecurityToken(
                claims:claims,
                signingCredentials:creds
                );
            var jwtHandler = new JwtSecurityTokenHandler();
            return new JwtResponse {
                Access_token = jwtHandler.WriteToken(jwt),
                Scope = claimModel.Scope
            };
        }
        /// <summary>
        /// 解析jwt
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static ClaimModel SerializeJWT(string jwtStr)
        {

            ///https://www.cnblogs.com/JacZhu/p/6837676.html#Update2.0  刷新     用户的 Token 在过期时间之内根本无法手动设置失效，随之而来的还有重放攻击等等问题


            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            object role = new object(); ;
            jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
          
            //var tm = new TokenModelJWT
            //{
            //    Uid = (jwtToken.Id).ObjToInt(),
            //    Role = role != null ? role.ObjToString() : "",
            //};



           // var jwtHandler = new JwtSecurityTokenHandler();
           // JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            ClaimModel claimModel = new ClaimModel();
            //object role = new object();
           // claimModel.Claim = jwtToken.Claims.ToDictionary(claim => claim.Type, claim => claim.Value);
            Dictionary<string, object> claimDict = new Dictionary<string, object>();
            foreach (Claim claim in jwtToken.Claims)
            {
                claimDict.TryAdd(claim.Type, claim.Value);
            }
            claimDict[ClaimTypes.Role] = role;
            claimModel.Claim = claimDict;
            claimModel.Claims = jwtToken.Claims.ToList();
            jwtToken.Payload.TryGetValue(JwtClaimTypes.Role, out role);
            if(role!=null)claimModel.Roles=role.ToString().Split(",").ToList();
            return claimModel;
        }
    }
}
