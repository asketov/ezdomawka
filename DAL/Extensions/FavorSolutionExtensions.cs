using DAL.Entities;

namespace DAL.Extensions
{
    public static class FavorSolutionExtensions
    {
        public static IQueryable<FavorSolution> WithThemeIdFilter(this IQueryable<FavorSolution> query, Guid? themeId)
        {
            if (themeId != null) return query.Where(x => x.ThemeId == themeId);
            return query;
        }
        public static IQueryable<FavorSolution> WithSubjectIdFilter(this IQueryable<FavorSolution> query, Guid? subjectId)
        {
            if (subjectId != null) return query.Where(x => x.FavorSubjects.Any(x => x.SubjectId == subjectId));
            return query;
        }
        public static IQueryable<FavorSolution> WithPriceFilter(this IQueryable<FavorSolution> query, int minPrice, int maxPrice)
        {
            return query.Where(x => x.Price >= minPrice && x.Price <= maxPrice);
        }
    }
}
