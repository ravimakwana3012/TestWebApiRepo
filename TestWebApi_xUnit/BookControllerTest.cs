using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestWebApi.Controllers;
using TestWebApi_BAL.Services;
using TestWebApi_DAL.Logging;
using TestWebApi_DAL.Models;
using TestWebApi_DAL.Repository;
using Xunit;

namespace TestWebApi_xUnit
{
    public class BookControllerTest
    {
        //private IServiceBook<book> _unitTesting = null;

        private readonly Mock<IServiceBook<book>> bookService;
        private readonly Mock<IMapper> mapper;
        private readonly Mock<ILog> logger;

        public BookControllerTest()
        {
            bookService = new Mock<IServiceBook<book>>();
            mapper = new Mock<IMapper>();
            logger = new Mock<ILog>();
        }

        [Fact]
        //naming convention MethodName_expectedBehavior_StateUnderTest
        public void GetBooks_ListOfBooks_BooksExistsInRepo()
        {
            //arrange
            var books = GetSampleBooks();
            bookService.Setup(x => x.GetAllBook()).Returns(GetSampleBooks);
            var controller = new BookController(bookService.Object, mapper.Object, logger.Object);

            //act
            var actionResult = controller.GetAll();
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as IEnumerable<book>;

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleBooks().Count(), actual.Count());
        }

        private List<book> GetSampleBooks()
        {
            List<book> output = new List<book>
        {
            new book
            {
                name = "Book 1",
                authorName = "Ravi Makwana",
                id = Guid.NewGuid()
            },
            new book
            {
                name = "Book 2",
                authorName = "Ravi Makwana",
                id = Guid.NewGuid()
            },
            new book
            {
                name = "Book 3",
                authorName = "Ravi Makwana",
                id = Guid.NewGuid()
            }
        };
            return output;
        }
    }
}
