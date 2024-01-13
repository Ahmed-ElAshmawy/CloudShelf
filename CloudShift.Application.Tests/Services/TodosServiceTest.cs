using CloudShift.Application.Dto;
using CloudShift.Application.Services;
using CloudShift.Kernel.Application.Dto;
using CloudShift.Kernel.Application.Enum;
using CloudShift.Kernel.Application.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Text.Json;

namespace CloudShift.Application.Tests.Services
{
    [TestClass]
    public class TodosServiceTest
    {
        private readonly Mock<IRestClient> _mockService = new Mock<IRestClient>();
        private readonly Mock<IConfiguration> _mockConfiguration = new Mock<IConfiguration>();
        private readonly Mock<IConfigurationSection> _mockConfigurationSection = new Mock<IConfigurationSection>();

        public TodosServiceTest()
        {
            _mockConfigurationSection.Setup(a => a.Value).Returns("testvalue");
            _mockConfiguration.Setup(a => a.GetSection(It.IsAny<string>())).Returns(_mockConfigurationSection.Object);
            _mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("");
        }

        [TestMethod]
        public async Task GetTodos_AreEqual()
        {
            // Arrange
            List<TodoDto> todoDtoList = new List<TodoDto>
            {
                new TodoDto{Id = 1,UserId = 1,Title = "todo 1",Completed = true},
                new TodoDto{Id = 2,UserId = 1,Title = "todo 2",Completed = true}
            };

            string todoJsonResult = JsonSerializer.Serialize(todoDtoList);
            _mockService.Setup(service => service.GetAsync<JsonDocument>(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
                .ReturnsAsync(new ResponseWrapper<JsonDocument> { Data = JsonDocument.Parse(todoJsonResult), IsSucceeded = true });

            TodosService _service = new TodosService(_mockService.Object, _mockConfiguration.Object);

            // Act
            var result = await _service.GetTodos();

            // Assert
            Assert.AreEqual(result.Data?.Count, todoDtoList.Count);
            Assert.AreEqual(result.Data?.FirstOrDefault()?.Title, todoDtoList.First().Title);
        }

        [TestMethod]
        public async Task GetTodos_ReturnIntegrationCommunicationError()
        {
            // Arrange
            _mockService.Setup(service => service.GetAsync<JsonDocument>(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
                 .ReturnsAsync(new ResponseWrapper<JsonDocument>
                 {
                     IsSucceeded = false
                 });

            TodosService _service = new TodosService(_mockService.Object, _mockConfiguration.Object);

            // Act
            var result = await _service.GetTodos();

            // Assert
            Assert.IsNull(result.Data);
            Assert.AreEqual(result.ErrorCode, ErrorCode.IntegrationCommunicationError);
        }
    }
}
