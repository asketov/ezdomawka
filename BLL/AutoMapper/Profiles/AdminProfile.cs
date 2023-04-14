using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.Admin;
using BLL.Models.ViewModels;
using DAL.Entities;

namespace BLL.AutoMapper.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<ThemeRequest, ThemeModel>();
            CreateMap<ThemeModel, Theme>();
            CreateMap<ThemeModel, ThemeVm>();
            CreateMap<SubjectRequest, SubjectModel>();
            CreateMap<SubjectModel, Subject>();
            CreateMap<SubjectModel, SubjectVm>();
            CreateMap<Theme, ThemeModel>();
            CreateMap<Subject, SubjectModel>();
            CreateMap<SubjectVm, SubjectModel>();
            CreateMap<ThemeVm, ThemeModel>();
            CreateMap<User, UserVm>();
            CreateMap<FavorSolution, WarnTopFavorSolution>();
            CreateMap<FavorSolution, WarnTopFavorSolutionVm>();
        }
    }
}
