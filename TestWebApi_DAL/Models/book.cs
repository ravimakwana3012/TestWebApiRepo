using System;
using System.ComponentModel.DataAnnotations;

namespace TestWebApi_DAL.Models
{
    public class book
    {
        public Guid id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string authorName { get; set; }
    }
}
