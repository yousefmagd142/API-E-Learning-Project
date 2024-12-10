using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTO
{
    public class CourseDto
    {

        [Required]
        [MaxLength(100)]
        public string CourseName { get; set; }=string.Empty;

        public string Description { get; set; }=string.Empty;

        public IFormFile CourseCover { get; set; } =default!; 
    }

}
