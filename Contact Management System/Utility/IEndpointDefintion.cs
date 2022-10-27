namespace Contact_Management_System.Utility
{
    public interface IEndpointDefintion
    {
        void DefineEndpoints(WebApplication app);
        void DefineServices (IServiceCollection services);
    }
}
