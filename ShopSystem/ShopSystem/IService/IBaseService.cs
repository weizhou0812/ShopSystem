using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ShopSystem.IService
{
    public interface IBaseService<TEntity> where TEntity : class,new()
    {
        //查询表全部数据     
        public Task<List<TEntity>> QueryAsync();
        //模糊查询
        //新增
        public Task<int> Add(TEntity entity);
        //删除
        public Task<int> Del(Expression<Func<TEntity, bool>> delWhere);
        //修改
        public Task<int> EditAsync(TEntity entity);
        /// <summary>
        /// 自定义条件查询
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public Task<List<TEntity>> AsyncQuery(Expression<Func<TEntity, bool>> selectWhere);
        //分页查询
        public Task<List<TEntity>> GetListPage(int pageNum, int pageSize);
        //按条件分页
        public Task<List<TEntity>> Sortbycondition(int pageNum, int pageSize, Expression<Func<TEntity, bool>> selectWhere);

    }
}
