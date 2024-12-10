using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string LessonTitle { get; set; } = string.Empty;

        public int CourseID { get; set; }

        [ForeignKey("CourseID")]
        public Course Course { get; set; }

        [Url]
        public string VideoURL { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
    }
}
