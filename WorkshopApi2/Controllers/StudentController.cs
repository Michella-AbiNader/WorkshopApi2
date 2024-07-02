using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WorkshopApi2.Functions;
using WorkshopApi2.Models;
using System.Web.UI.WebControls;

namespace WorkshopApi2.Controllers
{
    public class StudentController : ApiController
    {
        static string connectionString = "Data Source=DESKTOP-5GQU1D3;Initial Catalog=AOU_workshop;User ID=micha; Password=micha123 ";
        
        // GET: api/Student     (get all users)
        public IHttpActionResult Get([FromUri] string firstName = "") //search by first name(potional: if no firstName given, it will return all
        {
            string query = "SELECT * FROM users WHERE type = student";
            SqlOperations sql = new SqlOperations();
            DataTable students = sql.sqlToDataTable(query);
            if (students == null)
            {
                return Content(HttpStatusCode.InternalServerError, "Something went wrong");

            }
            return Content(HttpStatusCode.OK, students);
        }

        // GET: api/Student/5    (get user by id)
        public IHttpActionResult Get(int id)
        {
           // User user = new User();
            //return Content(HttpStatusCode.OK, user.get(id);
            //or:
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    DataTable students = new DataTable();
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        string query = "SELECT id, first_name, last_name, gender FROM users WHERE id = @id";
                        SqlParameter sqlParameter = new SqlParameter("@id", id);
                        command.Connection = sqlConnection;
                        command.Parameters.Add(sqlParameter);
                        command.CommandText = query;
                        command.CommandType = System.Data.CommandType.Text;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            students.Load(reader);
                        }
                    }
                    sqlConnection.Close();
                    return Content(HttpStatusCode.OK, students);

                }
                catch (SqlException ex)
                {
                    return Content(HttpStatusCode.InternalServerError, ex);
                }
            }
        }

        // POST: api/Student     (add user)
        public IHttpActionResult Post(User user)
        {
            user.save("student");
            return Content(HttpStatusCode.Created, "Student created successfully!");
          
        }

        // PUT: api/Student/5     (edit a user)
        [ApiAuthentication]
        public IHttpActionResult Put(int id, [FromBody]Student student)
        {
           if(!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "All fields are required!");
            }
            SqlOperations sql = new SqlOperations();
            string query = "UPDATE INTO ...";
            //make sql parameters  check session 7.2 last 5 mins
            //sql.executeSql
            return Content(HttpStatusCode.OK,"");
        }

        // DELETE: api/Student/5
        [ApiAuthentication]
        public IHttpActionResult Delete(int id)
        {
           
            return Content(HttpStatusCode.OK, "Student deleted successfully");
        }
    }
}
