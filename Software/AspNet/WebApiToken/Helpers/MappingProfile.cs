using AutoMapper;
using WebApiToken.Entities;
using WebApiToken.Models;

namespace WebApiToken.Helpers
{
    // This class maps objects, so that e.g. the UserModel (that is returned by the API) can be mapped to the User (that is the object stored in the database)
    public class MappingProfile : Profile
    {

        // Constructor
        public MappingProfile()
        {

            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateModel, User>();
            CreateMap<User, UserPublicModel>();

        }

    }
}
