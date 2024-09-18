using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc;

namespace BE_GOStudy.AppStart
{
    public static class SwaggerConfig
    {
        public static void ConfigureSwaggerForServices(this IServiceCollection services, string appName)
        {
            services.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
            });

        }
        public static void ConfigureSwaggerForApps(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var providerApiVersionDescription in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{providerApiVersionDescription.GroupName}/swagger.json",
                        providerApiVersionDescription.GroupName.ToUpperInvariant());
                }
            });
        }
    }
}
