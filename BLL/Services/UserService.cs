using AutoMapper;
using AutoMapper.QueryableExtensions;
using BLL.Models.Auth;
using BLL.Models.FavorSolution;
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
        private readonly IMapper _mapper;
        public UserService(DataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
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
            var user = await GetUserByEmail(model.Email);
            user.PasswordHash = model.PasswordHash;
            await _db.SaveChangesAsync();
        }
        /// <exception cref="UserNotFoundException"></exception>
        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null) throw new UserNotFoundException();
            return user;
        }

        public async Task DeleteUser(Guid userId)
        {
            var user = await GetUserById(userId);
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }

        public async Task<List<SolutionModel>> GetFavorSolutionsByUserId(Guid userId, CancellationToken token)
        {
            var favorSolutions = await _db.FavorSolutions.Where(x => x.AuthorId == userId)
                .ProjectTo<SolutionModel>(_mapper.ConfigurationProvider).AsNoTracking().ToListAsync(token);
            return favorSolutions;
        }

        public async Task<bool> CheckUserHasFavor(Guid userId, Guid favorId)
        {
            return await _db.FavorSolutions.AnyAsync(x => x.AuthorId == userId && x.Id == favorId);
        }

        public async Task<bool> UserIsBanned(Guid userId)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) return false;
            return user.IsBanned;
        }

        public async Task<Ban?> GetCurrentBanOrDefault(Guid userId)
        {
            var ban = await _db.Bans.FirstOrDefaultAsync(f => f.BanTo < DateTime.UtcNow && f.UserId == userId);
            return ban;
        }

        public async Task UnbanUser(Guid userId)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) user.IsBanned = false;
            await _db.SaveChangesAsync();
        }
    }
}
