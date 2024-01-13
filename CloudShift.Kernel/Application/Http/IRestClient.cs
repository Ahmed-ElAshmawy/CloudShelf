using CloudShift.Kernel.Application.Dto;

namespace CloudShift.Kernel.Application.Http
{
    public interface IRestClient
    {
        Task<ResponseWrapper<T>> GetAsync<T>(string url, Dictionary<string, string>? headers = null);
    }
}
