using ShopSystem.Models;
using ShopSystem.ViewModels;

namespace ShopSystem.IService
{
    public interface IUserService:IBaseService<Users>
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        Users GetById(int id);
    }
}
