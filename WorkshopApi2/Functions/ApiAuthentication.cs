using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WorkshopApi2.Functions
{
    public class ApiAuthentication: ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string token = actionContext.Request.Headers.Authorization?.Parameter ?? "";
            CryptoGraphy cryptoGraphy = new CryptoGraphy();
            if(!cryptoGraphy.isTokenValid(token, ref actionContext))
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(System.Net.HttpStatusCode.Unauthorized, "Unauthorized!");
                return;
            }

            base.OnActionExecuting(actionContext);
        }
    }
}