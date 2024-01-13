using CloudShift.Kernel.Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CloudShift.Kernel.Controllers
{
    public abstract class ApiControllerBase : ControllerBase
    {
        protected ActionResult ProcessResponse<T>(ResponseWrapper<T> response)
        {
            if (response.IsSucceeded)
            {
                return Ok(response.Data);
            }

            return StatusCode((int)response.ErrorCode, response);
        }
    }
}
