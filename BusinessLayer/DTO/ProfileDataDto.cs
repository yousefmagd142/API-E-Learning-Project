using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTO
{
    public class ProfileDataDto
    {
        public IFormFile VedioURL { get; set; } = default!;
        public IFormFile PhotoURL { get; set; } = default!;

        public string University { get; set; } = string.Empty;
        public string BachelorDegree { get; set; } = string.Empty;

        public string About { get; set; } = string.Empty;
        public string Adress { get; set; } = string.Empty;
        [Url]
        public string FaceBook { get; set; } = string.Empty;
        [Url]
        public string LinkedIn { get; set; } = string.Empty;
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number must contain only numbers.")]
        public string PhoneNum { get; set; } = string.Empty;
    }
}
