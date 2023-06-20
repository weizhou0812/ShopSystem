using ShopSystem.Data;
using ShopSystem.IService;
using ShopSystem.Models;

namespace ShopSystem.Service
{
    public class ProductService:BaseService<Products>,IProductService
    {
        private readonly ShopDbContext _DbContext;
        public ProductService(ShopDbContext db) : base(db)
        {
            this._DbContext = db;
        }
    }
}
