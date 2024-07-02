using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WorkshopApi2.Models;
using WorkshopApi2.Functions;
using System.Web.UI.WebControls;

namespace WorkshopApi2.Controllers
{
    public class AuthController : ApiController
    {
        CryptoGraphy crypto = new CryptoGraphy();
        public IHttpActionResult register(User user)
        {
            user.save("student");
            return Content(HttpStatusCode.Created, "User created successfully");

        }

        public IHttpActionResult login(User user)
        {
            var result = crypto.VerifyUser(user.email, user.password);
            if (!result.verified)
            {
                return Content(HttpStatusCode.Unauthorized, "Incorrect Emal or Password");
            }
            return Content(HttpStatusCode.OK, new {result.token});
        }
    }
}
