﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OpenBankAPI.Application.Repositories;
using OpenBankAPI.Domain.Entities.Common;
using OpenBankAPI.Persistence.Contexts;

namespace OpenBankAPI.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
	{
        private readonly OpenBankAPIDbContext _context;


        public WriteRepository(OpenBankAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T model)
        {
           EntityEntry<T> entityEntry= await Table.AddAsync(model);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> datas)
        {
            await Table.AddRangeAsync(datas);
            return true;
        }

        public bool Remove(T model)
        {
            EntityEntry<T> entityEntry = Table.Remove(model);
            return entityEntry.State == EntityState.Deleted ;
        }

        public bool RemoveRange(List<T> datas)
        {
            Table.RemoveRange(datas);
            return true;
         }

        public async Task<bool> RemoveAsync(string id)
        {
            T model =  await Table.FirstOrDefaultAsync(data => data.Id == long.Parse(id));
            return Remove(model);
        }
         
        public  bool Update(T model)
        {
           EntityEntry entityEntry= Table.Update(model);
            return entityEntry.State == EntityState.Modified;
          
        }

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();

       
    }
}

