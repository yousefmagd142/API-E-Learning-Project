using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLayer.DTO
{
    public class LessonDTO
    {
        [Required]
        [MaxLength(100)]
        public string LessonTitle { get; set; }=string.Empty;
        public int CourseID { get; set; }
        public string Content { get; set; } = string.Empty;
        public IFormFile VideoFile { get; set; } = default!;

    }
}
