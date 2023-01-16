using BLL.Models.Auth;
using BLL.Models.UserModels;
using Common.Exceptions.User;
using Common.Helpers;
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
        /// <exception cref="UserNotFoundException"></exception>
        public async Task<User> GetUserById(Guid id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) throw new UserNotFoundException();
            return user;
        }

        /// <exception cref="UserNotFoundException"></exception>
        public async Task<User> GetUserByCredentials(CredentialModel model)
        {
            var passwordHash = HashHelper.GetHash(model.Password);
            var user = await _db.Users.FirstOrDefaultAsync(u => u.PasswordHash == passwordHash && u.Email == model.Email);
            if (user == null) throw new UserNotFoundException();
            return user;
        }
        public async Task<bool> CheckUserExistByNick(string nick)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Nick == nick);
            return  user != null;
        }
        public async Task<bool> CheckUserExistByEmail(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user != null;
        }

        public async Task ChangePassword(ChangePasswordModel model)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            user.PasswordHash = model.PasswordHash;
            await _db.SaveChangesAsync();
        }
    }
}
