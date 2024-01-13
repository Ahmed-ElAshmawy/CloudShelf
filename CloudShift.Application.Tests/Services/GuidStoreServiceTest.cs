using CloudShift.Application.Repositories;
using CloudShift.Application.Services;
using CloudShift.Domain.Model;
using CloudShift.Kernel.Application.Dto;
using Moq;

namespace CloudShift.Application.Tests.Services
{
    [TestClass]
    public class GuidEntitiesServiceTest
    {
        private readonly Mock<IBaseRepository<GuidEntity>> _mockRepository = new Mock<IBaseRepository<GuidEntity>>();

        [TestMethod]
        public async Task GetGuid_AreEqual()
        {
            // Arrange
            var guid = Guid.Parse("6B29FC40-CA47-1067-B31D-00DD010662DA");
            _mockRepository.Setup(service => service.GetFirst())
                .ReturnsAsync(new GuidEntity
                {
                    Id = 1,
                    GuidValue = guid
                });

            GuidEntitiesService _GuidEntitiesService = new GuidEntitiesService(_mockRepository.Object);

            // Act
            var result = await _GuidEntitiesService.GetGuid();

            // Assert
            Assert.AreEqual(result.Data?.GuidValue, guid);
        }
    }
}
