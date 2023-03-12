using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Extensions
{
    public static class UserExtensions
    {
        public static IQueryable<User> WithEmailFilter(this IQueryable<User> query, string? email)
        {
            if (email != null) return query.Where(x => x.Email.Contains(email));
            return query;
        }
        public static IQueryable<User> WithNickFilter(this IQueryable<User> query, string? nick)
        {
            if (nick != null) return query.Where(x => x.Nick.Contains(nick));
            return query;
        }
    }
}
