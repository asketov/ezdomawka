using AutoMapper;
using BLL.Models.FavorSolution;
using DAL.Entities;

namespace BLL.AutoMapper.Actions
{
    public class FavorSolutionMapperAction : IMappingAction<AddSolutionModel, FavorSolution>
    {
        public void Process(AddSolutionModel source, FavorSolution destination, ResolutionContext context)
        {
            destination.FavorThemes = new List<FavorTheme>();
            destination.FavorSubjects = new List<FavorSubject>();
            foreach (var theme in source.Themes)
            {
                destination.FavorThemes.Add(new FavorTheme()
                {
                    ThemeId = theme.Id
                });
            }
            foreach (var subject in source.Subjects)
            {
                destination.FavorSubjects.Add(new FavorSubject()
                {
                    SubjectId = subject.Id
                });
            }
        }
    }
}
