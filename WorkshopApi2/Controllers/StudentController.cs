using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WorkshopApi2.Models;

namespace WorkshopApi2.Controllers
{
    public class StudentController : ApiController
    {
        static List<Student> students = new List<Student>{
            new Student{id = 1, firstName = "Micha", lastName = "Abi Nader", phoneNumber = 70458378, numberOfCourses = 3}
        };
        // GET: api/Student
        public IHttpActionResult Get()
        {
            return Content(HttpStatusCode.OK, students);
        }

        // GET: api/Student/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Student
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Student/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Student/5
        public void Delete(int id)
        {
        }
    }
}
