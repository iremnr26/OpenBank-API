using System;
using NuGet.Protocol.Core.Types;
using OpenBankAPI.Domain.Entities.Common;

namespace OpenBankAPI.Application.Repositories
{
	public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
		//ekleme silme vb.
		Task<bool> AddAsync(T model );
		Task<bool> AddRangeAsync(List<T> datas);

        bool Remove(T model);
        bool RemoveRange(List<T> datas);
        Task<bool> RemoveAsync(string id);

        bool Update (T model);

        Task<int> SaveAsync();
    }
}
 
 