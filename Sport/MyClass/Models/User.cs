﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Không để trống!")]
        public String FullName { get; set; }
        [Required(ErrorMessage = "Không để trống!")]
        public String UserName { get; set; }
        [Required(ErrorMessage = "Không để trống!")]
        public String Password { get; set; }
        [Required(ErrorMessage = "Không để trống!")]
        public String Email { get; set; }
        [Required(ErrorMessage = "không để trống!")]
        public String Gender { get; set; }
        [Required(ErrorMessage = "không để trống!")]
        public String Phone { get; set; }
        public String Img { get; set; }
        public int Roles { get; set; }
        public String Address { get; set; }
        public int? Created_By { get; set; }
        public DateTime? Created_At { get; set; }
        public int? Updated_By { get; set; }
        public DateTime? Updated_At { get; set; }
        public int Status { get; set; }
    }
}
