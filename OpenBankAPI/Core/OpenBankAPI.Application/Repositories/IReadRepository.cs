using System;
using NuGet.Protocol.Core.Types;
using System.Linq.Expressions;
using OpenBankAPI.Domain.Entities.Common;

namespace OpenBankAPI.Application.Repositories
{
	public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {//EF Core Tracking Performans Optimizasyonu için tracing ekledim

        // select işlemleri 
        IQueryable<T> GetAll(bool tracking = true);
        //şartlı sorgu
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
        //tekil nesne getiricek
        Task<T> GetSingleAsync (Expression<Func<T, bool>> method, bool tracking = true);
        //id olanı çekk
        Task<T> GetByIdAsync(string id, bool tracking = true); 

    }
} 

