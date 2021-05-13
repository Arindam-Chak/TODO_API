using AutoMapper;
using ToDoAPI.Controllers.Resources;
using ToDoAPI.Core.Models;

namespace ToDoAPI.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<UserCredentialsResource, User>();
        }
    }
}