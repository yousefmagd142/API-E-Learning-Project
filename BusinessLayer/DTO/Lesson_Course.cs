using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTO
{
    public class Lesson_Course
    {
        [MaxLength(100)]
        public string LessonTitle { get; set; } = string.Empty;
        public string VideoURL { get; set; } = String.Empty;
        public string Content { get; set; } = string.Empty;

        public string CourseName { get; set; }

    }
}
