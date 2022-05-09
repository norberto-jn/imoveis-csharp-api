using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace moveis_api.Http
{
    public class HttpResponse : Http
    {
        public override NotFoundObjectResult NotFound(string message)
        {
            object value = new { message = message };
            return new NotFoundObjectResult(value);
        }

        public override BadRequestObjectResult BadRequest(string message)
        {
            object value = new { message = message };
            return new BadRequestObjectResult(value);
        }

        public override NoContentResult NoContent()
        {
            return new NoContentResult();
        }


    }
}