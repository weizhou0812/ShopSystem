using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopSystem.Data;
using ShopSystem.IService;
using ShopSystem.Models;

namespace ShopSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize]
    public class EmployeeController : ControllerBase
    {
        public readonly IEmployeeService _iService;
        public EmployeeController(IEmployeeService ieservice)
        {
            _iService = ieservice;
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
        [HttpDelete("byid")]
        public async Task<object> del(string eid)
        {
            var data = await this._iService.Del(c => c.EId== eid);
            return data;
        }
        [HttpGet("byuname")]
        public async Task<object> getlistbyname(string ename)
        {
            var data = await this._iService.AsyncQuery(a => a.EName == ename);
            if (data == null)
            {
                return NotFound();
            }
            return data;
        }
        [HttpPost]
        public async Task<object> add(Employees employees)
        {
            return await _iService.Add(employees);
        }
        [HttpPut]
        public async Task<object> editAsync(Employees employees)
        {
            return await _iService.EditAsync(employees);
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
        public async Task<object> fuzzyquery(string eid, [FromQuery] int pageNum, int pageSize)
        {
            var data = await _iService.Sortbycondition(pageNum, pageSize, e => EF.Functions.Like(e.EId, "%" + eid + "%"));
            //var data = await _iService.AsyncQuery(e => EF.Functions.Like(e.UId, "%"+uid+"%"));
            return data;
        }
        [HttpGet("FQCount")]
        public async Task<object> FQCount(string eid)
        {
            var data = await _iService.AsyncQuery(e => EF.Functions.Like(e.EId, "%" + eid + "%"));
            return data.Count();
        }
    }
}
