using Microsoft.AspNetCore.Mvc;

namespace MooD.Controllers
{

    public class HejController : Controller
    {

        public IActionResult Index()
        {
            return Ok("Hej");
        }
    }
}
