using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;
using Swashbuckle.AspNetCore.Swagger;

namespace Swashbuckle.AspNetCore.Bug
{
    public static class Program
    {
        public static void Main()
        {
            const string swaggerDocName = "V1";

            var services = new ServiceCollection();

            // Add some commong services
            services.AddOptions();
            services.AddLogging();
            services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();

            // Configure MVC services
            var mvc = services.AddMvcCore();
            mvc.AddApplicationPart(typeof(Program).Assembly);
            mvc.AddControllersAsServices();
            mvc.AddApiExplorer();
            mvc.AddNewtonsoftJson();

            // Configure Swagger Generator
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    swaggerDocName,
                    new OpenApiInfo  { Title = "API",  Version = "v1", }
                );
            });
            
            // Try to generate OpenAPI document
            using var serviceProvider = services.BuildServiceProvider();
            var provider = serviceProvider.GetRequiredService<ISwaggerProvider>();
            var swagger = provider.GetSwagger(swaggerDocName);

            using var sw = new StringWriter();
            var w = new OpenApiJsonWriter(sw);
            swagger.SerializeAsV3(w);
            var json = sw.ToString();
            Console.Out.WriteLine(json);
        }
    }
}
