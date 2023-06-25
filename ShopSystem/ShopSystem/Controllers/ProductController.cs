using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopSystem.Data;
using ShopSystem.IService;
using ShopSystem.Models;
using static Dm.net.buffer.ByteArrayBuffer;

namespace ShopSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize]

    public class ProductController : ControllerBase
    {
        public readonly IProductService _iService;
        private readonly ShopDbContext _DbContext;
        public ProductController(IProductService iPservice, ShopDbContext _DbContext)
        {
            _iService = iPservice;
            this._DbContext = _DbContext;
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        [HttpGet("Count")]
        public async Task<object> Get()
        {
            var data = await this._iService.QueryAsync();
            var acount = data.Count();
            return acount;
        }
        [HttpGet("alldata")]
        public async Task<object> getAlldata()
        {
            var data = await this._iService.QueryAsync();
            return data;
        }
        [HttpGet("selectfiled")]
        public async Task<object> selectFiledName()
        {
            var data = await _DbContext.Database.SqlQuery<string>($"SELECT PName,Inventory FROM `products`").ToListAsync();
            return data;
        }
        //[HttpGet("selectfiledinventory")]
        //public async Task<object> selectFiledInventory()
        //{
        //    var data = await _DbContext.Database.SqlQuery<int>($"SELECT Inventory FROM `products`").ToListAsync();
        //    return data;
        //}

        [HttpDelete("byid")]
        public async Task<object> del(string pid)
        {
            var data = await this._iService.Del(c => c.PId == pid);
            return data;
        }
        [HttpGet("byuname")]
        public async Task<object> getlistbyname(string pname)
        {
            var data = await this._iService.AsyncQuery(a => a.PName == pname);
            if (data == null)
            {
                return NotFound();
            }
            return data;
        }
        [HttpPost]
        public async Task<object> add(Products products)
        {
            DateTime dateTimeNow = DateTime.Now;
            //过期节点
            DateTime expirationNode = products.ProduceDate.AddMonths(products.SellbyDate);
            System.TimeSpan t = expirationNode - dateTimeNow;
            if (t.TotalSeconds > 0)
            {
                products.IsExpire = false;
                return await _iService.Add(products);
            }
            else
            {
                products.IsExpire = true;
                return await _iService.Add(products);
            }
        }
        [HttpPut]
        public async Task<object> editAsync(Products products)
        {
            DateTime dateTimeNow= DateTime.Now;
            //过期节点
            DateTime expirationNode = products.ProduceDate.AddMonths(products.SellbyDate);
            System.TimeSpan t = expirationNode - dateTimeNow;
            if (t.TotalSeconds>0)
            {
                products.IsExpire = false;
                return await _iService.EditAsync(products);
            }
            else
            {
                products.IsExpire = true;
                return await _iService.EditAsync(products);
            }
        }
        //[HttpGet("byuid")]
        //public async Task<object> getlistbyid(string eid)
        //{
        //    var data = await this._iService.AsyncQuery(a => a.EId == eid);
        //    if (data == null)
        //    {
        //        return NotFound();
        //    }
        //    return data;
        //}
        [HttpGet("getlistbypage")]
        public async Task<object> getListByPage([FromQuery] int pageNum, int pageSize)
        {
            var data = await _iService.GetListPage(pageNum, pageSize);
            return data;
            //return (data.Count, data).ToTuple();

        }
        /// <summary>
        /// 模糊查询并分页
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("FuzzyQuery")]
        public async Task<object> fuzzyquery(string pid, [FromQuery] int pageNum, int pageSize)
        {
            var data = await _iService.Sortbycondition(pageNum, pageSize, e => EF.Functions.Like(e.PId, "%" + pid + "%"));
            //var data = await _iService.AsyncQuery(e => EF.Functions.Like(e.UId, "%"+uid+"%"));
            return data;
        }
        [HttpGet("FQCount")]
        public async Task<object> FQCount(string pid)
        {
            var data = await _iService.AsyncQuery(e => EF.Functions.Like(e.PId, "%" + pid + "%"));
            return data.Count();
        }
    }
}
