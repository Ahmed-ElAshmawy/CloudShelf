using CloudShift.Kernel.Application.Enum;

namespace CloudShift.Kernel.Application.Dto
{
    public class ResponseWrapper<T>
    {
        public T? Data { get; set; }

        public bool IsSucceeded { get; set; }
    
        public string? Message { get; set; }
     
        public ErrorCode ErrorCode { get; set; }
    }
}
