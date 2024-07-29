using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class SessionExpireFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // If the browser session or authentication session has expired...
            string username = filterContext.HttpContext.Session.GetString("UserName");
            if (!string.IsNullOrEmpty(username))
            {
                username = username.Replace(@"\", "");
            }
            if (string.IsNullOrEmpty(username) || username == "\"\"")
            {

                if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    filterContext.Result = new JsonResult("Login");

                }
                else
                {
                    // will, in turn, redirect to the logon page.

                    filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                        { "Controller", "account" },
                        { "Action", "Login" }
                    });
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
