﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMate.Persistence.Relational
{
    internal class RelationalRepository<T> : IAsyncRepository<T>
        where T : class
    {
        protected DbSet<T> Set { get; }

        public RelationalRepository(DbSet<T> set)
        {
            Set = set;
        }

        public void Add(T entity) => Set.Add(entity);
        public Task AddAsync(T entity) => Set.AddAsync(entity).AsTask();

        public void AddMany(IEnumerable<T> entities) => Set.AddRange(entities);
        public Task AddManyAsync(IEnumerable<T> entities) => Set.AddRangeAsync(entities);

        public IEnumerable<T> GetAll() => Set;

        public T? GetById(params object[] id) => Set.Find(id);
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
        public Task<T?> GetByIdAsync(params object[] id) => Set.FindAsync(id).AsTask();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.

        public void Remove(T entity) => Set.Remove(entity);

        public void RemoveMany(IEnumerable<T> entities) => Set.RemoveRange(entities);

        public void Update(T entity) => Set.Update(entity);

        public void UpdateMany(IEnumerable<T> entities) => Set.UpdateRange(entities);

        public IEnumerable<T> GetPage(int page, int itemsPerPage)
            => Set.Skip(page * itemsPerPage)
                  .Take(itemsPerPage);

        /// <remarks>
        /// Contrary to the rest of overloads, this function evaluates the query internally in the client and thus it 
        /// may result in degraded performance.
        /// </remarks>
        public IEnumerable<T> GetPage(int page, int itemsPerPage, out bool hasNext)
        {
            var items = Set.Skip(page * itemsPerPage)
                            .Take(itemsPerPage + 1)
                            .ToList();

            var count = items.Count();
            hasNext = count == itemsPerPage + 1;

            return items.SkipLast(1);
        }
    }
}
