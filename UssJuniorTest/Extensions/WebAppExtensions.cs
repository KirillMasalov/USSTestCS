namespace UssJuniorTest.Extensions
{
    public static class WebAppExtensions
    {
        public static WebApplication UseCustomizeSwagger(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
            return app;
        }
    }
}
