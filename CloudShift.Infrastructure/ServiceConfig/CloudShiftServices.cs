using CloudShift.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CloudShift.Infrastructure.ServiceConfig
{
    public static class CloudShiftServices
    {
        public static IServiceCollection AddServicesfff(this IServiceCollection services)
        {
            return services.AddScoped<IOrderItemsService, OrderItemsService>();
        }
    }
}
