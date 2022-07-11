using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PBTech.APIv1.ViewModel;

namespace PBTech.APIv1.Filters;
public class ValidationFilterAttribute : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = new BadRequestObjectResult(context.ModelState.Select(c => new PropertyError{ Property = c.Key, Message = c.Value.Errors.Select(x => x.ErrorMessage).First() }));  
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
