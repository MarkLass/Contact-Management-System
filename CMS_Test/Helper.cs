using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace CMS_Test
{
    public static class Helper
    {
        public static async Task<T?> GetResponseValue<T>(IResult result)
        {
            var mockHttpContext = new DefaultHttpContext
            {
                // RequestServices needs to be set so the IResult implementation can log.
                RequestServices = new ServiceCollection().AddLogging().BuildServiceProvider(),
                Response =
                {
                    // The default response body is Stream.Null which throws away anything that is written to it.
                    Body = new MemoryStream(),
                },
            };

            await result.ExecuteAsync(mockHttpContext);
            
            // Reset MemoryStream to start so we can read the response.
            mockHttpContext.Response.Body.Position = 0;
            var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            return await JsonSerializer.DeserializeAsync<T>(mockHttpContext.Response.Body, jsonOptions);
        }
    }
}
