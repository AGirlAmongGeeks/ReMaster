using AutoMapper;
using ReMaster.BusinessLogic.Company.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReMaster.EntityFramework.Model;

namespace ReMaster
{
    public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Company, CompanyListDto>()
				.ForMember(dest => dest.OwnerFullName, opt => opt.MapFrom(src => src.OwnerFirstName + " " + src.OwnerLastName ));
			CreateMap<CompanyListDto, Company>();

			CreateMap<Company, Company>().ForMember(c => c.Id, opt => opt.UseDestinationValue()); // doesn't work anyway...
			//.ForMember(x => x.Id, opt => opt.Ignore()).ForMember(x => x.Fax, opt => opt.UseValue("TEST"));

		}
	}
}
