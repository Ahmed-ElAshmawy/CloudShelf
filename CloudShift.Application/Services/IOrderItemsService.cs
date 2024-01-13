using CloudShift.Application.Dto;
using CloudShift.Kernel.Application.Dto;


namespace CloudShift.Application.Services
{
    public interface IOrderItemsService
    {
        ResponseWrapper<OrderTaxDetailsDto> CalculateOrderItemsInvoice(List<OrderItemDto> orderItems);
    }
}
