using ApplicationCore.Models;
using AutoMapper;
using WebApi.Models;

namespace WebApi.Mappings
{
    public class TaekwondoClubProfile : Profile
    {
        public TaekwondoClubProfile()
        {
            CreateMap<Student, StudentDto>();
            CreateMap<ClubMembership, ClubMembershipDto>();
        }
    }
}
