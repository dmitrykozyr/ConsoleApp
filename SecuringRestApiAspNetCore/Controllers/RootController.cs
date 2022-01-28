using Microsoft.AspNetCore.Mvc;

namespace SecuringRestApiAspNetCore.Controllers
{
    [Route("/")]
    [ApiController]
    [ApiVersion("1.3")] // Настройка версионирования
                        // Если отправить запрос в Postman, то в Headers отобразится номер версии
                        // Если в Postman в Headers добаить key="Accept" value="application/json;v=2.0"
                        // то выдаст ошибку 'UnsupportedApiVersion', т.к. нет такой версии
    public class RootController : ControllerBase
    {
        [HttpGet(Name = nameof(GetRoot))]
        [ProducesResponseType(200)]
        public IActionResult GetRoot()
        {
            var response = new
            {
                href = Url.Link(nameof(GetRoot), null),
                rooms = new
                {
                    // В Postman вернется ссылка на метод RoomsController.GetRooms
                    href = Url.Link(nameof(RoomsController.GetRooms), null)
                }
            };

            return Ok(response);
        }
    }
}
