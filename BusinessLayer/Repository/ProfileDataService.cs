using BusinessLayer.DTO;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository
{
    public class ProfileDataService : IProfileDataService
    {
        private readonly AppDbContext _db;
        private readonly IHostingEnvironment _env;
        private readonly string _videopath;



        public ProfileDataService(AppDbContext db, IHostingEnvironment env)
        {
            _db = db;
            _env = env;
            _videopath = $"{_env.WebRootPath}/uploads/ProfileData";

        }

        public async Task<ProfileData> GetProfileData()
        {
            var profiledata = await _db.ProfileData.FirstOrDefaultAsync();
            if (profiledata != null) 
            {
                return profiledata;
                    }
            else return null;

        }

        public async Task UpdateProfileData(ProfileDataDto NewProfileData)
        {
            var OldProfiledata = await _db.ProfileData.FirstOrDefaultAsync();
            if (OldProfiledata != null) 
            {
                OldProfiledata.Adress = NewProfileData.Adress;
                OldProfiledata.About = NewProfileData.About;
                OldProfiledata.LinkedIn = NewProfileData.LinkedIn;
                OldProfiledata.BachelorDegree= NewProfileData.BachelorDegree;
                OldProfiledata.University= NewProfileData.University;
                OldProfiledata.FaceBook= NewProfileData.FaceBook;
                OldProfiledata.PhoneNum= NewProfileData.PhoneNum;

                var ProfileVedio = $"{Guid.NewGuid()}{Path.GetExtension(NewProfileData.VedioURL.FileName)}";
                var path = Path.Combine(_videopath, ProfileVedio);
                using var stream = File.Create(path);
                NewProfileData.VedioURL.CopyTo(stream);
                stream.Dispose();

                OldProfiledata.VedioURL = ProfileVedio;

                var ProfilePhoto = $"{Guid.NewGuid()}{Path.GetExtension(NewProfileData.PhotoURL.FileName)}";
                var Photopath = Path.Combine(_videopath, ProfilePhoto);
                using var streamPhoto = File.Create(Photopath);
                NewProfileData.PhotoURL.CopyTo(streamPhoto);
                stream.Dispose();

                OldProfiledata.PhotoURL = ProfilePhoto;
                _db.SaveChanges();
            }

        }
    }
}
