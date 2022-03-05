using Microsoft.EntityFrameworkCore;
using RestWithASPNETUdemy.Model.Base;
using RestWithASPNETUdemy.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNETUdemy.Repository.Implementations
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected MySQLContext _context;

        private DbSet<T> dataset;

        public GenericRepository(MySQLContext context)
        {
            _context = context;
            dataset = _context.Set<T>(); /*É como se estivesse setando uma tabela no startup*/
        }


        public T Create(T item)
        {
            try
            {
                dataset.Add(item);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return item;
        }

        public void Delete(long id)
        {
            var result = dataset.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    dataset.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public T Update(T item)
        {
            if (!Exists(item.Id)) return null;

            var result = dataset.SingleOrDefault(p => p.Id.Equals(item.Id));
            if (result != null)
            {
                try
                {
                    //dataset.Update(result);
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return item;
        }

        public List<T> FindAll()
        {
            return dataset.ToList();
        }

        public T FindByID(long id)
        {
            return dataset.SingleOrDefault(p => p.Id.Equals(id));
        }

        public bool Exists(long id)
        {
            return dataset.Any(p => p.Id.Equals(id));
        }

        public List<T> FindWithPagedSearch(string query)
        {
            return dataset.FromSqlRaw<T>(query).ToList();
        }
        public int GetCount(string query)
        {
            var result = "";
            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    result = command.ExecuteScalar().ToString();
                }
            }
            return int.Parse(result);
        }
    }
}
