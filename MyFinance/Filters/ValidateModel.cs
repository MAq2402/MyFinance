using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Filters
{
    public class ValidateModel : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //var model = context.ActionArguments["modelFromBody"];
            //if(!context.ModelState.IsValid)
            //{

            //    var view = new ViewResult
            //    {
            //        ViewData
            //    }

            //    context.Result = new ViewResult { ViewData = new { userName = context.ActionArguments["userName"], modelFromBody = model } };
            //    }
            //}
        }
    }
}

