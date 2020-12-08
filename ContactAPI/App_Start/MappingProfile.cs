
using AutoMapper;
using ContactAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ContactAPI.Models.VM.ContactVM;

namespace ContactAPI.App_Start
{
	public class MappingProfile : Profile
	{
		
		public MappingProfile()
		{
			CreateMap<Contact, AddContactInput>();
		}
	}
}