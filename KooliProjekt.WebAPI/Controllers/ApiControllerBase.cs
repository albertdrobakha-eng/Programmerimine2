using KooliProjekt.Application.Infrastructure.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace KooliProjekt.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]    
    public abstract class ApiControllerBase : Controller
    {
        private static JsonSerializerSettings _serializerSettings =
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore                
            };

        protected IActionResult Result(OperationResult result)
        {
            var serialized = JsonConvert.SerializeObject(result, _serializerSettings);

            if (result.HasErrors)
            {
                return Content(serialized, "application/json", System.Text.Encoding.UTF8);
            }

            return Content(serialized, "application/json", System.Text.Encoding.UTF8);
        }

        protected IActionResult Result<T>(OperationResult<T> result)
        {
            var serialized = JsonConvert.SerializeObject(result, _serializerSettings);

            if (result.HasErrors)
            {
                return Content(serialized, "application/json", System.Text.Encoding.UTF8);
            }

            if (result.Value == null)
            {
                return NotFound();
            }

            return Content(serialized, "application/json", System.Text.Encoding.UTF8);
        }
    }
}
