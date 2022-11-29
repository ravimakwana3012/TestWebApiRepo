using AutoMapper;
using TestWebApi_DAL.Models;
//using TestWebApi.Models;

namespace TestWebApi.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<book, bookDTO>();
        }
    }
}
