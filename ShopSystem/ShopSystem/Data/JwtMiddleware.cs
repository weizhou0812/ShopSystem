using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using ShopSystem.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ShopSystem.Data
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly AuthSettings _authSettings;
        public JwtMiddleware(RequestDelegate requestDelegate,IOptions<AuthSettings>  authSettings)
        {
            _requestDelegate = requestDelegate;
            _authSettings = authSettings.Value;
        }

        //验证从发送方标头提取令牌
        public async Task Invoke(HttpContext context,IUserService service)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                //验证
                AttachUserToContext(context,service,token);
            }
            //调用下一个中间件
            await _requestDelegate(context);
        }
        //如果验证通过，就获取用户数据
        private void AttachUserToContext(HttpContext context, IUserService service, string token)
        {
            //验证
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key=Encoding .ASCII.GetBytes(_authSettings.Secret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience=false,
                ClockSkew=TimeSpan.Zero,
            },out var validatedToken);
            //转换成JwtSecurityToken
            var jwtToken = (JwtSecurityToken)validatedToken;
            //获取用户ID
            var userId = int.Parse(jwtToken.Claims.First(c => c.Type == "sub").Value);
            context.Items["User"]=service.GetById(userId);
        }
    }
}
