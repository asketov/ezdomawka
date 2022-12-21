using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.Admin;
using DAL.Entities;

namespace BLL.AutoMapper.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<ThemeRequest, ThemeModel>();
            CreateMap<ThemeModel, Theme>();
            CreateMap<SubjectRequest, SubjectModel>();
            CreateMap<SubjectModel, Subject>();
        }
    }
}
