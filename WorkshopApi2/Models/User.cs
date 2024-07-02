using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WorkshopApi2.Functions;

namespace WorkshopApi2.Models
{
    public class User
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string gender { get; set; }
        public string date_of_birth { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string token { get; set; }
        public string role { get; set; }


        public void save(string type)
        {
            CryptoGraphy crypto = new CryptoGraphy();
            password = crypto.encryptedPassword(password);
            string query = "INSERT INTO(first_name, last_name, phone_number, gender, email,password, date_of_birth, type)" +
                "VALUES(@first_name, @last_name, @phone_number, @gender, @email,@password, @date_of_birth, @type);" +
                "SEELCT SCOPE_IDENTITY() AS userId";

            SqlParameter[] sqlParameters = new SqlParameter[]
           {
                new SqlParameter("@first_name", firstName),
                new SqlParameter("@last_name", lastName),
                new SqlParameter("@phone_number", phoneNumber),
                new SqlParameter("@gender", gender),
                new SqlParameter("@email", email),
                new SqlParameter("@date_of_birth", date_of_birth),
                new SqlParameter("@password", password),
                new SqlParameter("@type", type)
           };
            SqlOperations sql = new SqlOperations();
            DataTable userIdDt = sql.sqlToDataTable(query, sqlParameters);
            int userId = 0;
            if(userIdDt != null && userIdDt.Rows.Count > 0)
            {
                userId = Convert.ToInt32(userIdDt.Rows[0]["userId"]);
            }
            string token = crypto.generateJwtToken(userId.ToString(), email, type);
            query = "UPDATE users SET token = @token WHERE id = @userId";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@userId", userId),
                new SqlParameter("@token", token)
            };
            sql.executeSql(query, parameters);
        }

        public User get(int Id)
        {
            string query = "SELECT id, first_name, last_name, gender FROM users WHERE id = @id";
            SqlOperations sql = new SqlOperations();
            DataTable students = sql.sqlToDataTable(query);
            var user = new User();

            if (students != null && students.Rows.Count > 0)
            {
                DataRow row = students.Rows[0];
                user.id = Convert.ToInt32(row["id"]);
                user.firstName = row["firstName"].ToString();
                //....
            }
            return user;
        }
    }
}