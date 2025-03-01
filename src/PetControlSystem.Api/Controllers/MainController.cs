using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PetControlSystem.Domain.Notifications;
using System.Net;

namespace PetControlSystem.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotificator _notificator;

        protected MainController(INotificator notificator)
        {
            _notificator = notificator;
        }

        protected bool IsValidOperation()
        {
            return !_notificator.HasNotification();
        }

        protected ActionResult CustomResponse(HttpStatusCode httpStatus = HttpStatusCode.OK, object result = null)
        {
            if (IsValidOperation())
            {
                return new ObjectResult(result)
                {
                    StatusCode = Convert.ToInt32(httpStatus)
                };
            }

            return BadRequest(new
            {
                errors = _notificator.GetNotifications().Select(n => n.Message)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) { }
            return CustomResponse();
        }

        protected void AddError(string error)
        {
            _notificator.Handle(new Notification(error));
        }
    }
}
