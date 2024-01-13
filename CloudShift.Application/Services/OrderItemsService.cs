using CloudShift.Application.Dto;
using CloudShift.Kernel.Application.Dto;

namespace CloudShift.Application.Services
{
    public class OrderItemsService : IOrderItemsService
    {
        private readonly double taxPercentage = 0.15;

        public ResponseWrapper<OrderTaxDetailsDto> CalculateOrderItemsInvoice(List<OrderItemDto> orderItems)
        {
            if (IsEmpty(orderItems))
            {
                return new ResponseWrapper<OrderTaxDetailsDto>
                {
                    IsSucceeded = false,
                    ErrorCode = Kernel.Application.Enum.ErrorCode.InvalidInput,
                    Message= "order Items must have at least one item"
                };
            }

            double totalOrderItemsPrice = 0;
            foreach (var item in orderItems)
            {
                totalOrderItemsPrice += item.UnitPrice * item.Quantity;
            }

            return new ResponseWrapper<OrderTaxDetailsDto>
            {
                IsSucceeded = true,
                Data = new OrderTaxDetailsDto
                {
                    TotalOrderPrice = totalOrderItemsPrice,
                    CalculatedTax = totalOrderItemsPrice * taxPercentage
                }
            };
        }

        private bool IsEmpty(List<OrderItemDto> orderItems)
        {
            return orderItems is null || orderItems.Count == 0;
        }
    }
}
