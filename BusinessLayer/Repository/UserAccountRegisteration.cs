using BusinessLayer.DTO;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Repository
{
    public class UserAccountRegisteration : IUserAccountRegisteration
    {
        private readonly AppDbContext db;

        public UserAccountRegisteration(AppDbContext Db)
        {
            db = Db;
        }


        public UserAccount RegisterUser(UserAccountDTO newUser)
        {
            UserAccount userAccount = new UserAccount
            {
                UserName = newUser.UserName,
                PasswordHash = newUser.Password ,
                Email = newUser.Email ,
                PhoneNumber = newUser.PhoneNumber ,                
            };
            return userAccount;
        }

        public UserDto GetUser(string userName)
        {
            var user = db.UserAccount
                .Where(o => o.UserName == userName)
                .Select(o => new UserDto
                {
                    UserName = o.UserName,
                    Email = o.Email,
                    PhoneNumber = o.PhoneNumber,
                    Role = db.UserRoles
                        .Where(ur => ur.UserId == o.Id)
                        .Join(db.Roles,
                              ur => ur.RoleId,
                              r => r.Id,
                              (ur, r) => r.Name) 
                        .FirstOrDefault() 
                })
                .FirstOrDefault();

            return user;
        }



        public UserAccount LogIn(UserAccount item)
        {
            throw new NotImplementedException();
        }


    }
}
