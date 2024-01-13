using CloudShift.Application.Dto;
using CloudShift.Kernel.Application.Dto;

namespace CloudShift.Application.Services
{
    public interface IGuidEntitiesService
    {
        Task<ResponseWrapper<GuidEntityDto>> GetGuid();
    }
}
