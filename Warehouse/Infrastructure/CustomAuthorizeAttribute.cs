
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Warehouse.DB.Contexts;
namespace Warehouse.Infrastructure
{
    public class CustomAuthorize : ActionFilterAttribute
    {
        private readonly WarehouseDbContext _context;
        string _roles;
        public CustomAuthorize( WarehouseDbContext context)
        {
            _context = context;
        }

        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            bool authorized = false;
            try
            {
                var userIDNumber = context.HttpContext.Session.Get<string>("UserName");

                if (!string.IsNullOrEmpty(userIDNumber))
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Account" }, { "Action", "Login" } });
                }
                //else
                //{
                //    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Login" }, { "Action", "Index" } });
                   
                //}
            }
            catch (Exception ex)
            {

                authorized = false;
                //context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Login" }, { "Action", "Index" } });
            }

        }
    }
}