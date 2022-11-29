using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestWebApi_DAL.Models;
using TestWebApi_DAL.Repository;
using System.Linq;
using TestWebApi_DAL.Logging;

namespace TestWebApi.Repository
{
    public class BookRepository : IRepository<book>
    {
        private readonly TestDBContext testDbContext;
        private ILog logger;

        public BookRepository(TestDBContext testDbContext, ILog logger)
        {
            logger.Information("Information is logged - BookRepository");
            this.testDbContext = testDbContext;
            this.logger = logger;
        }
        public async Task<book> Create(book _bookObj)
        {
            try
            {
                if (_bookObj != null)
                {
                    var bookObj = await testDbContext.AddAsync(_bookObj);
                    await testDbContext.SaveChangesAsync();
                    return bookObj.Entity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }
        public IEnumerable<book> GetAll()
        {
            try
            {
                var lstBooks = testDbContext.book.ToList();
                if (lstBooks.Count > 0) return lstBooks;
                else return null;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }
        public book GetById(Guid Id)
        {
            try
            {
                if (Id != null)
                {
                    var bookObj = testDbContext.book.FirstOrDefault(x => x.id == Id);
                    if (bookObj != null) return bookObj;
                    else return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }
    }
}
