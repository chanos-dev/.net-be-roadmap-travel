namespace FiltersAndAttributes.Filters
{
    public class FilterMiddlewarePipeline
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                Console.WriteLine("FilterMiddlewarePipeline Configure");

                context.Response.Headers.Add("Pipeline", "Middleware");

                await next();
            });
        }
    }
}
