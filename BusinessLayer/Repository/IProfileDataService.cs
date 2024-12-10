using BusinessLayer.DTO;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository
{
    public interface IProfileDataService
    {
        public Task UpdateProfileData(ProfileDataDto profileData);
        public Task<ProfileData> GetProfileData();
    }
}
