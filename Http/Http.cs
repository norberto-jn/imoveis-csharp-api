using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace moveis_api.Http
{
    public abstract class Http
    {
        public abstract NotFoundObjectResult NotFound(string message);

        public abstract BadRequestObjectResult BadRequest(string message);

        public abstract NoContentResult NoContent();

    }
}