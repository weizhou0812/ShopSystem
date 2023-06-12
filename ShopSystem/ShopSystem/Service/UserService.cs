using ShopSystem.Data;
using ShopSystem.Models;
using ShopSystem.IService;

namespace ShopSystem.Service
{
    public class UserService : BaseService<Users>,IUserService
    {
        private readonly ShopDbContext _DbContext;
        public UserService(ShopDbContext db) : base(db)
        {
            this._DbContext = db;
        }

    }
}
