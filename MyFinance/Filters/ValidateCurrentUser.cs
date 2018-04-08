using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Filters
{
    public class ValidateCurrentUser :Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var userName = context.ActionArguments["userName"];
            if(context.HttpContext.User.Identity.Name!=(string)userName)
            {
                context.Result = new RedirectToActionResult("Index", "Home",new { });

            }
        }
    }
}
