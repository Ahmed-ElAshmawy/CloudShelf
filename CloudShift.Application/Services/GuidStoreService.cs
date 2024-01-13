using CloudShift.Application.Dto;
using CloudShift.Application.Repositories;
using CloudShift.Domain.Model;
using CloudShift.Kernel.Application.Dto;

namespace CloudShift.Application.Services
{
    public class GuidEntitiesService(IBaseRepository<GuidEntity> repository) : IGuidEntitiesService
    {
        public async Task<ResponseWrapper<GuidEntityDto>> GetGuid()
        {
            var result = await repository.GetFirst();
            return new ResponseWrapper<GuidEntityDto>
            {
                IsSucceeded = true,
                Data = new GuidEntityDto { GuidValue = result?.GuidValue ?? Guid.NewGuid() }
            };
        }
    }
}
