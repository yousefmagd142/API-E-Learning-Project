using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class ProfileData
    {
        public int Id { get; set; }
        public string VedioURL { get; set; }=string.Empty;
        public string PhotoURL { get; set; }= string.Empty;
        public string University { get; set; }=string.Empty ;
        public string BachelorDegree { get; set; } = string.Empty;
        public string About { get; set; } = string.Empty;
        public string Adress { get; set; } = string.Empty;
        public string FaceBook { get; set; } = string.Empty;
        public string LinkedIn { get; set; } = string.Empty;
        public string PhoneNum { get; set; } = string.Empty;
    }
}
