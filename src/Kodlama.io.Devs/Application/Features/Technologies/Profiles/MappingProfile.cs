using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Commands.UpdateTechnology;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreatedTechnologyDto, Technology>().ReverseMap();
            CreateMap<CreateTechnologyCommand, Technology>().ReverseMap();
            CreateMap<Technology, TechnologyListDto>().ForMember(c => c.LanguageName, opt => opt.MapFrom(c => c.Language.Name)).ReverseMap();
            CreateMap<IPaginate<Technology>, ListTechnologyModel>().ForMember(c=>c.Items,opt=>opt.MapFrom(c=>c.Items)).ReverseMap();
            CreateMap<UpdateTechnologyCommand, Technology>().ReverseMap();
            CreateMap<Technology, UpdatedTechnologyDto>().ReverseMap();
            CreateMap<Technology, DeletedTechnologyDto>().ReverseMap();
            
        }
    }
}
