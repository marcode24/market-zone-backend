using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.Filters;

public class ValidateIdAttribute : ActionFilterAttribute
{
  public override void OnActionExecuting(ActionExecutingContext context)
  {
    if (context.ActionArguments.TryGetValue("id", out object? value) && value is string id)
    {
      if (!int.TryParse(id, out _))
      {
        context.Result = new BadRequestObjectResult(new { Message = "Id parameter must be an integer" });
        return;
      }
    }
  }
}
