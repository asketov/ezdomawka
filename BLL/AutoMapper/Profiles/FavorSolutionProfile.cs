using AutoMapper;
using BLL.Models.Admin;
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
            CreateMap<AddSolutionRequest, SolutionModel>().ReverseMap();
            CreateMap<SolutionModel, FavorSolution>()
                .ForMember(u => u.Created, k => k.MapFrom(d => DateTimeOffset.UtcNow))
                .ForMember(d => d.FavorSubjects, s => s.MapFrom(a => a.Subjects))
                .ForMember(d => d.FavorThemes, s => s.MapFrom(a => a.Themes));
            CreateMap<ThemeModel, FavorTheme>().ForMember(d=>d.ThemeId, f=>f.MapFrom(m=>m.Id));
            CreateMap<SubjectModel, FavorSubject>().ForMember(d => d.SubjectId, f => f.MapFrom(m => m.Id));
            CreateMap<FavorSolutionVm, SolutionModel>();
            CreateMap<SolutionModel, FavorSolutionVm>().ForMember(d => d.Nick, k => k.MapFrom(a => a.Author.Nick));
            CreateMap<FavorSolution, SolutionModel>().ForMember(f=>f.Themes, k=>k.MapFrom(d=>d.FavorThemes))
                .ForMember(f => f.Subjects, k => k.MapFrom(d => d.FavorSubjects));
            CreateMap<FavorTheme, ThemeModel>()
                .ForMember(k => k.Id, f => f.MapFrom(l => l.ThemeId))
                .ForMember(m => m.Name, f => f.MapFrom(s => s.Theme.Name));
            CreateMap<FavorSubject, SubjectModel>()
                .ForMember(k => k.Id, f => f.MapFrom(l => l.Subject.Id))
                .ForMember(m => m.Name, f => f.MapFrom(s => s.Subject.Name));

        }
    }
}
