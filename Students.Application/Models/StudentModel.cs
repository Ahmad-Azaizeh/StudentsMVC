using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Students.Application.Models
{
    public class StudentModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Full name is required")]
        [MaxLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;


        [Range(16, 100, ErrorMessage = "Age must be between 16 and 100")]
        public int Age { get; set; }

        public string Major { get; set; } = string.Empty;
    }
}
