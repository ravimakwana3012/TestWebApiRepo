using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestWebApi_DAL.Repository
{
    public interface IRepository<T>
    {
        public Task<T> Create(T _object);
        public IEnumerable<T> GetAll();
        public T GetById(Guid Id);
    }
}
