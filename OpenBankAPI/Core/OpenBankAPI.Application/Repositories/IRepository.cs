using System;
using Microsoft.EntityFrameworkCore;
using OpenBankAPI.Domain.Entities.Common;

namespace OpenBankAPI.Application.Repositories
{
	public interface IRepository<T> where T : BaseEntity
	{
        DbSet<T> Table { get; }
	}
}

