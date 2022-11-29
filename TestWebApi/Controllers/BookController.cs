using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TestWebApi_BAL.Services;
using TestWebApi_DAL.Logging;
using TestWebApi_DAL.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestWebApi.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/Book/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IServiceBook<book> bookService = null;
        private IMapper mapper;
        private ILog logger;
        public BookController(IServiceBook<book> bookService, IMapper mapper, ILog logger)
        {
            logger.Information("Information is logged - BookController");
            this.bookService = bookService;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: api/<BookController>
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var response = bookService.GetAllBook();
                if (response.Count() > 0)
                {
                    return Ok(mapper.Map<IEnumerable<bookDTO>>(response));
                }
                return NotFound("No data in response.");
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest();
            }
        }

        // GET api/<BookController>/224AF142-E0C9-42BE-9478-1917F57E5B6Z
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            try
            {
                var response = bookService.GetById(id);
                if (response != null)
                {
                    return Ok(mapper.Map<bookDTO>(response));
                }
                return NotFound("No data with id - " + id);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest();
            }
        }

        // POST api/<BookController>
        [HttpPost]
        public ActionResult Post([FromBody] book value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = bookService.AddBook(value);
                    if (response.Result != null)
                    {
                        return Ok(mapper.Map<bookDTO>(response.Result));
                    }
                    return NotFound("Data is not inserted.");
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest();
            }
        }
    }
}
