using MenuApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MenuApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IRedisService _redis;

        public MenuController(IRedisService redis)
        {
            this._redis = redis;
        }

        [HttpGet("getmenu")]
        public ActionResult<string> GetMenu()
        {

            var valueByte = _redis.GetMenu();

            return "Menu";
        }
    }
}
