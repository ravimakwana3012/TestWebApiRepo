using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi_DAL.Logging;
using TestWebApi_DAL.Models;
using TestWebApi_DAL.Repository;

namespace TestWebApi_BAL.Services
{
    public class ServiceBook : IServiceBook<book>
    {
        public readonly IRepository<book> repository;
        private ILog logger;

        public ServiceBook()
        {
        }

        public ServiceBook(IRepository<book> repository, ILog logger)
        {
            logger.Information("Information is logged - ServiceBook");
            this.repository = repository;
            this.logger = logger;
        }

        //Create Method
        public async Task<book> AddBook(book bookObj)
        {
            try
            {
                if (bookObj == null)
                {
                    throw new ArgumentNullException(nameof(bookObj));
                }
                else
                {
                    return await repository.Create(bookObj);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        //Get All
        public IEnumerable<book> GetAllBook()
        {
            try
            {
                return repository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        //Get By Id
        public book GetById(Guid id)
        {
            try
            {
                return repository.GetById(id);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }
    }
}
