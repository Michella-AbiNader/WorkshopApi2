using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkshopApi2.Models
{
    public class Student
    {
        public int id { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public int phoneNumber { get; set; }
        public int numberOfCourses { get; set; }


    }
}