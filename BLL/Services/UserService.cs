

using Common.Exceptions.User;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class UserService
    {
        private readonly DataContext _db;
        public UserService(DataContext db)
        {
            _db = db;
        }

        public async Task<User> GetUserById(Guid id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) throw new UserNotFoundException();
            return user;
        }
    }
}
