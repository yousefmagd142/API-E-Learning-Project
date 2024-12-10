using BusinessLayer.DTO;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository
{
    public interface IUserAccountRegisteration
    {
        public UserAccount RegisterUser(UserAccountDTO item);
        public UserDto GetUser(string username);
    }
}
