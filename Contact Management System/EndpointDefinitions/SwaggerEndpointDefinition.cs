using Contact_Management_System.Utility;

namespace Contact_Management_System.EndpointDefinitions
{
    public class SwaggerEndpointDefinition : IEndpointDefintion
    {
        public void DefineEndpoints(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SimpleApi v1"));
        }

        public void DefineServices(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "SimpleApi", Version = "v1" });
            });
        }
    }
}
