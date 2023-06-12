using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopSystem.Data;
using ShopSystem.IService;
using ShopSystem.Models;
using System.Linq.Expressions;

namespace ShopSystem.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        private readonly ShopDbContext _DbContext;
        public BaseService(ShopDbContext _DbContext)
        {
            this._DbContext = _DbContext;
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> Add(TEntity entity)
        {
            this._DbContext.Add(entity);
            return await this._DbContext.SaveChangesAsync();
        }
        /// <summary>
        /// 查询全部数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<TEntity>> QueryAsync()
        {
            //同步
            //return  this._DbContext.Set<TEntity>().ToList();
            //异步方法
            var data = await this._DbContext.Set<TEntity>().ToListAsync();
            return data;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Del(Expression<Func<TEntity, bool>> delWhere)
        {
            List<TEntity> listDels = await _DbContext.Set<TEntity>().Where(delWhere).ToListAsync();
            listDels.ForEach(model =>
            {
                _DbContext.Entry(model).State = EntityState.Deleted;
            });
            return _DbContext.SaveChanges();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> EditAsync(TEntity entity)
        {
            this._DbContext.Entry(entity).State = EntityState.Modified;
            return await this._DbContext.SaveChangesAsync();
        }
        /// <summary>
        /// 按条件查询
        /// </summary>
        /// <param name="selectWhere"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> AsyncQuery(Expression<Func<TEntity, bool>> selectWhere)
        {
            List<TEntity> list = await _DbContext.Set<TEntity>().Where(selectWhere).ToListAsync();
            return list;
        }

        //分页查询
        public async Task<List<TEntity>> GetListPage(int pageNum, int pageSize)
        {
            return await _DbContext.Set<TEntity>().Skip((pageNum - 1) * pageSize).Take(pageSize).ToListAsync();
        }

    }
}
