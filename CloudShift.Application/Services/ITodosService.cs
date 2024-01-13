using CloudShift.Application.Dto;
using CloudShift.Kernel.Application.Dto;

namespace CloudShift.Application.Services
{
    public interface ITodosService
    {
        Task<ResponseWrapper<List<TodoDto>>> GetTodos();
    }
}
