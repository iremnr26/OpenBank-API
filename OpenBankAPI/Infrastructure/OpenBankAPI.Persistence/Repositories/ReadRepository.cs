 using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OpenBankAPI.Application.Repositories;
using OpenBankAPI.Domain.Entities.Common;
using OpenBankAPI.Persistence.Contexts;

namespace OpenBankAPI.Persistence.Repositories
{
	public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity 
    {
        private readonly OpenBankAPIDbContext _context;

        public ReadRepository(OpenBankAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query;

        }
          
        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }

        //bunu kullandığım yerde de async ve await yapısı kullanmama lazım 
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = Table.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);


        }
        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        // => await  Table.FirstOrDefaultAsync(data => data.Id == long.Parse(id));
        //burda gönderilen değeri longa çevirmeyi unutmamam lazım 
        // => await Table.FindAsync(long.Parse(id));
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = Table.AsNoTracking();
            return await query.FirstAsync(data => data.Id == long.Parse(id));
        }
       

        
    }
}

