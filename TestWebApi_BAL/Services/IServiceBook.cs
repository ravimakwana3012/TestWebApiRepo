using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestWebApi_DAL.Models;

namespace TestWebApi_BAL.Services
{
    public interface IServiceBook<T>
    {
        public Task<T> AddBook(T _object);
        public IEnumerable<T> GetAllBook();
        public T GetById(Guid Id);
    }
}
