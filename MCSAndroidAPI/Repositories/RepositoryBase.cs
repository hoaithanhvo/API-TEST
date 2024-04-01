using AutoMapper;
using MCSAndroidAPI.Contracts;
using MCSAndroidAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MCSAndroidAPI.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected NidecMCSContext NidecMCSContext { get; set; }
        protected IMapper Mapper { get; set; }
        public RepositoryBase(NidecMCSContext nidecMCSContext, IMapper mapper)
        {
            NidecMCSContext = nidecMCSContext;
            Mapper = mapper;
        }

        public IQueryable<T> FindAll() => NidecMCSContext.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return NidecMCSContext.Set<T>().Where(expression).AsNoTracking();
        }
        public void Create(T entity) => NidecMCSContext.Set<T>().Add(entity);
        public void Update(T entity) => NidecMCSContext.Set<T>().Update(entity);
        public void Delete(T entity) => NidecMCSContext.Set<T>().Remove(entity);
    }
}
