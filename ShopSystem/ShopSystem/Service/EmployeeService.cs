using ShopSystem.Data;
using ShopSystem.IService;
using ShopSystem.Models;

namespace ShopSystem.Service
{
    public class EmployeeService : BaseService<Employees>, IEmployeeService
    {
        private readonly ShopDbContext _DbContext;
        public  EmployeeService(ShopDbContext db):base(db)
        {
            this._DbContext = db;
        }
    }
}
