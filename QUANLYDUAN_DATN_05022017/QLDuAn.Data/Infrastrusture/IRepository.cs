using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace QLDuAn.Data.Infrastrusture
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);

        T Delete(T entity);

        T Delete(int id);

        void Update(T entity);

        void DeleteMuti(Expression<Func<T, bool>> where);

        IEnumerable<T> GetAll(string[] includes = null);

        T GetById(int id);

        T GetByConditon(Expression<Func<T, bool>> express, string[] includes = null);

        IEnumerable<T> GetMuti(Expression<Func<T, bool>> predicate, string[] includes = null);

        IEnumerable<T> GetMutiPaging(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50, string[] includes = null);

        int Count(Expression<Func<T, bool>> where);

        bool CheckContain(Expression<Func<T, bool>> predicate);
    }
}