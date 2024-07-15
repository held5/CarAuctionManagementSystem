using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarAuction.API.Filters
{
  internal class ValidateModelFilter : ActionFilterAttribute
  {
      public override void OnActionExecuting(ActionExecutingContext actionContext)
      {
        if (!actionContext.ModelState.IsValid)
        {
          actionContext.Result = new BadRequestObjectResult(actionContext.ModelState);
        }
      }
  }
}
