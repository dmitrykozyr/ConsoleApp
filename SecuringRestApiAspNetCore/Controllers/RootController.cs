using Microsoft.AspNetCore.Mvc;
using SecuringRestApiAspNetCore.Models;

namespace SecuringRestApiAspNetCore.Controllers
{
    [Route("/")]
    [ApiController]
    [ApiVersion("1.0")] // Настройка версионирования
                        // Если отправить запрос в Postman, то в Headers отобразится номер версии
                        // Если в Postman в Headers добаить key="Accept" value="application/json;v=2.0"
                        // то выдаст ошибку 'UnsupportedApiVersion', т.к. нет такой версии
    public class RootController : ControllerBase
    {
        [HttpGet(Name = nameof(GetRoot))]
        [ProducesResponseType(200)]
        public IActionResult GetRoot()
        {
            /*var response = new
            {
                href = Url.Link(nameof(GetRoot), null),
                rooms = new
                {
                    // В Postman вернется ссылка на метод RoomsController.GetRooms
                    //href = Url.Link(nameof(RoomsController.GetRooms), null)
                },
                info = new
                {
                    href = Url.Link(nameof(InfoController.GetInfo), null)
                }
            };*/

            // Благодаря коду в классе Link можем заменить код сверху на код снизу
            var response = new RootResponse
            {
                Href = null,
                Rooms = Link.ToCollection(nameof(RoomsController.GetAllRooms)),
                Info = Link.To(nameof(InfoController.GetInfo))
            };

            return Ok(response);
        }
    }
}
