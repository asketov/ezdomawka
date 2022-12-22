using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.FavorSolution;
using BLL.Models.ViewModels;
using DAL.Entities;

namespace BLL.AutoMapper.Profiles
{
    public class FavorSolutionProfile : Profile
    {
        public FavorSolutionProfile()
        {
            CreateMap<SubjectVm, Subject>();
            CreateMap<ThemeVm, Theme>();
            CreateMap<AddSolutionRequest, AddSolutionModel>();
            CreateMap<AddSolutionModel, FavorSolution>()
                .ForMember(u => u.Created, k => k.MapFrom(d => DateTimeOffset.UtcNow));
        }
    }
}
