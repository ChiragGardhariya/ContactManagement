using AutoMapper;
using ContactAPI.Models;
using ContactManagement.Models;

namespace ContactManagement
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<Contact, ContactVM>().ReverseMap();
		}
	}
}
