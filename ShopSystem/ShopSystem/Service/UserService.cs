using ShopSystem.Data;
using ShopSystem.Models;
using ShopSystem.IService;
using ShopSystem.ViewModels;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace ShopSystem.Service
{
    public class UserService : BaseService<Users>,IUserService
    {
        private readonly ShopDbContext _DbContext;
        private readonly AuthSettings _authSettings;
        public UserService(IOptions<AuthSettings> authSetting, ShopDbContext db) : base(db)
        {
            _authSettings= authSetting.Value;
            this._DbContext = db;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            //通过账号密码验证用户
            var user = _DbContext.Set<Users>().Where(u=>u.UName==model.UName&&u.UPassword==model.UPassword).ToList().SingleOrDefault();
            if (user == null)
            {
                return null;
            }

            //创建令牌
            var token = GenerateJwtToken(user).ToString();

            //返回用户信息包括令牌
            return new AuthenticateResponse(user,token);
        }

        private object GenerateJwtToken(Users user)
        {
            byte[] key=Encoding.ASCII.GetBytes(_authSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //获取或设置身份信息
                Subject=new ClaimsIdentity (
                    new[]
                    {
                        new Claim("sub",user.UNumber.ToString()),
                        new Claim("name",user.UName)
                    }),
                //过期时间
                Expires=DateTime.UtcNow.AddDays(1),
                //证书签名
                SigningCredentials=new SigningCredentials
                (
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                 )
            };
            //创建token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public Users GetById(int id)
        {
            return _DbContext.Set<Users>().Where(u=>u.UNumber==id).ToList().FirstOrDefault();
        }
    }
}
