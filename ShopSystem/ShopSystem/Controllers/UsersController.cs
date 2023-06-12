﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities;
using ShopSystem.IService;
using ShopSystem.Models;
using ShopSystem.Service;
using System.Linq.Expressions;

namespace ShopSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _iService;
        public UsersController(IUserService iUserService)
        {
            this._iService = iUserService;
        }
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        [HttpGet("Count")]
        public async Task<object> Get()
        {
            var data = await this._iService.QueryAsync();
            var acount= data.Count();
            return acount;

        }
        [HttpDelete("byid")]
        public async Task<object> del(string id)
        {
            var data = await this._iService.Del(c => c.UId == id);
            return data;
        }
        [HttpGet("byuname")]
        public async Task<object> getlistbyname(string uname)
        {
            var data=await this._iService.AsyncQuery(a=>a.UName==uname);
            if (data==null)
            {
                return NotFound();
            }
            return data;
        }
        [HttpPost]
        public async Task<object> add(Users users)
        {
            return await _iService.Add(users);
        }
        [HttpPut]
        public async Task<object> editAsync(Users users)
        {
            return await _iService.EditAsync(users);
        }
        [HttpGet("byuid")]
        public async Task<object> getlistbyid(string uid)
        {
            var data = await this._iService.AsyncQuery(a => a.UName == uid);
            if (data == null)
            {
                return NotFound();
            }
            return data;
        }
        [HttpGet("getlistbypage")]
        public async Task<object>  getListByPage([FromQuery]int pageNum,int pageSize)
        {
            var data = await _iService.GetListPage(pageNum,pageSize);
            return data;
            //return (data.Count,data).ToTuple();
   
        }

    }
}