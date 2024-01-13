using CloudShift.Application.Dto;
using CloudShift.Application.Services;
using CloudShift.Kernel.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CloudShift.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController(IOrderItemsService orderItemsService) : ApiControllerBase
    {
        [HttpPost("invoice")]
        public IActionResult OrderItemsInvoice(List<OrderItemDto> orderItems)
        {
            var Invoice = orderItemsService.CalculateOrderItemsInvoice(orderItems);
            return ProcessResponse(Invoice);
        }
    }
}
