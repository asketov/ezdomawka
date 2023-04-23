using Common.Consts;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Extensions;

public static class AddBaseObjectsToDataContextExtensions
{
    private static Guid UserId => Guid.Parse(Roles.UserId);
    private static Guid AdminId => Guid.Parse(Roles.AdminId);
    private static Guid SuperAdminId => Guid.Parse(Roles.SuperAdminId);
    
    public static async Task<bool> BaseUserRolesAdded(this DataContext dbContext)
    {
        var rolesId = dbContext.Roles.Select(role => role.Id);

        return await rolesId.ContainsAsync(UserId) &&
               await rolesId.ContainsAsync(AdminId) &&
               await rolesId.ContainsAsync(SuperAdminId);
    }

    public static async Task<DataContext> AddBaseUserRoles(this DataContext dbContext)
    {
        await dbContext.Roles.AddRangeAsync(
            new Role(){Id = UserId, Name = "User"},
        new Role(){Id = AdminId, Name = "Admin"},
        new Role(){Id = SuperAdminId, Name = "SuperAdmin"});

        await dbContext.SaveChangesAsync();

        return dbContext;
    }

    public static async Task<bool> AnySubjectAdded(this DataContext dbContext)
    {
        return await dbContext.Subjects.AnyAsync();
    }

    public static async Task<DataContext> AddSubjectFromFile(this DataContext dbContext, string filePath)
    {
        using (var reader = new StreamReader(filePath))
        {
            string name;
            
            while ((name = reader.ReadLine()) != null)
            {
                var newSubject = new Subject()
                {
                    Name = name, InstituteId = Guid.Parse("578c1f27-b393-4d87-9370-6544c5d0a1f0")
                };

                dbContext.Subjects.Add(newSubject);
            }
        }

        await dbContext.SaveChangesAsync();

        return dbContext;
    }
    
    
    public static async Task<bool> AnyThemeAdded(this DataContext dbContext)
    {
        return await dbContext.Themes.AnyAsync();
    }

    public static async Task<DataContext> AddThemeFromFile(this DataContext dbContext, string filePath)
    {
        using (var reader = new StreamReader(filePath))
        {
            string name;
            
            while ((name = reader.ReadLine()) != null)
            {
                var theme = new Theme()
                {
                    Name = name, 
                    InstituteId = Guid.Parse("578c1f27-b393-4d87-9370-6544c5d0a1f0")
                };

                dbContext.Themes.Add(theme);
            }
        }

        await dbContext.SaveChangesAsync();

        return dbContext;
    }
}