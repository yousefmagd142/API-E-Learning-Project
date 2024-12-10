using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string CourseName { get; set; }=string.Empty;

        public string Description { get; set; } = string.Empty;

        public string CourseCover { get; set; } = string.Empty;

        // Navigation property
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    }
}
