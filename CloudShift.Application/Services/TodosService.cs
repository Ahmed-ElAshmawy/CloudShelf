using CloudShift.Application.Dto;
using CloudShift.Kernel.Application.Dto;
using CloudShift.Kernel.Application.Enum;
using CloudShift.Kernel.Application.Http;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace CloudShift.Application.Services
{
    public class TodosService(IRestClient restClient, IConfiguration configuration) : ITodosService
    {
        public async Task<ResponseWrapper<List<TodoDto>>> GetTodos()
        {
            var apiUrl = configuration.GetValue<string>("ExternalApis:JsonplaceholderTodo");
            var result = await restClient.GetAsync<JsonDocument>(apiUrl);

            if (result.IsSucceeded && result.Data != null)
            {
                return new ResponseWrapper<List<TodoDto>>
                {
                    IsSucceeded = true,
                    Data = JsonSerializer.Deserialize<List<TodoDto>>(result.Data)
                };
            }

            return new ResponseWrapper<List<TodoDto>>
            {
                ErrorCode = ErrorCode.IntegrationCommunicationError,
                Message = result.Message
            };
        }
    }
}
