using CloudShift.Domain;

namespace CloudShift.Api.Extensions
{
    public static class WebApplicationExtension
    {
        public static void SeedApplicationData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CloudShiftDbContext>();
            SeedData.Seeder(context);
        }
    }
}
