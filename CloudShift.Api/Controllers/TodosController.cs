using CloudShift.Application.Services;
using CloudShift.Kernel.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudShift.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController(ITodosService todosService) : ApiControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetTodos()
        {
            var result = await todosService.GetTodos();
            return ProcessResponse(result);
        }
    }
}
