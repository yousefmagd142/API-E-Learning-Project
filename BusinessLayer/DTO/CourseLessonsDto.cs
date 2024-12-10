using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTO
{
    public class CourseLessonsDto
    {
        public string CourseName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string CourseCoverUrl { get; set; } = default!;

        public List<lesson> lessons { get; set; } = new List<lesson>();

    }
    public class lesson
    {
        public string LessonTitle { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = default!;
    }
}
