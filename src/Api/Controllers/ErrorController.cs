using System.Web.Http;

namespace Api.Controllers
{
    public class ErrorController: ApiController
    {
        public IHttpActionResult Handle404()
        {
            return Ok();
        }
    }
}