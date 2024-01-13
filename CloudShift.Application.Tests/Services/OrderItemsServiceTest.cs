using CloudShift.Application.Dto;
using CloudShift.Application.Services;
using CloudShift.Kernel.Application.Enum;

namespace CloudShift.Application.Tests.Services
{
    [TestClass]
    public class OrderItemsServiceTest
    {
        private readonly OrderItemsService _service = new OrderItemsService();

        [TestMethod]
        public void CalculateOrderItemsInvoice_AreEqual()
        {
            // Arrange
            List<OrderItemDto> orderItems = new List<OrderItemDto>
            {
                new OrderItemDto{ ItemId = 1, UnitPrice= 50, Quantity = 1},
                new OrderItemDto{ ItemId = 2, UnitPrice= 50, Quantity = 1}
            };

            // Act
            var result = _service.CalculateOrderItemsInvoice(orderItems);

            // Assert
            Assert.AreEqual(result.Data?.TotalOrderPrice, 100);
            Assert.AreEqual(result.Data?.CalculatedTax, 15);
        }

        [TestMethod]
        public void CalculateOrderItemsInvoice_EmptyOrderItems()
        {
            // Arrange
            List<OrderItemDto> orderItems = new List<OrderItemDto>();

            // Act
            var result = _service.CalculateOrderItemsInvoice(orderItems);

            // Assert
            Assert.IsNull(result.Data);
            Assert.AreEqual(result.ErrorCode, ErrorCode.InvalidInput);
        }

        [TestMethod]
        public void CalculateOrderItemsInvoice_NullOrderItems()
        {
            // Act
            var result = _service.CalculateOrderItemsInvoice(orderItems: null);

            // Assert
            Assert.IsNull(result.Data);
            Assert.AreEqual(result.ErrorCode, ErrorCode.InvalidInput);
        }
    }
}
