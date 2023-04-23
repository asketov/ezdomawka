using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.Auth;
using Common.Consts;
using Common.Exceptions.User;
using DAL;
using DAL.Entities;

namespace BLL.Services
{
    public class AuthService
    {
        private readonly DataContext _db;
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public AuthService(DataContext db, UserService userService, IMapper mapper, EmailService service)
        {
            _db = db;
            _userService = userService;
            _mapper = mapper;
        }
        /// <exception cref="NickAlreadyExistException"></exception>
        /// <exception cref="EmailAlreadyExistException"></exception>
        public async Task<User> RegisterUser(RegisterModel model)
        {
            if (await _userService.CheckUserExistByNick(model.Nick)) throw new NickAlreadyExistException();
            if (await _userService.CheckUserExistByEmail(model.Email)) throw new EmailAlreadyExistException();

            var user = _mapper.Map<User>(model);
            user.InstituteId = Guid.Parse(Institutes.DefaultVuzVoenmehId);
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }
    }
}
