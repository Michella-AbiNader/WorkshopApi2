using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WorkshopApi2.Models
{
    public class Student
    {
        public int id { get; set; }

        [Required]
        public String firstName { get; set; }

        [Required]
        public String lastName { get; set; }

        public int phoneNumber { get; set; }

        [Required]
        public String email{ get; set; }
        public String gender { get; set; }
        public String date_of_birth { get; set; }


    }
}