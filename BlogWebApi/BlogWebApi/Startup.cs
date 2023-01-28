using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.OpenApi.Models;
using Microsoft.Data.Sqlite;



namespace BlogWebApi
{
    using BlogWebApi.DataAccess;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // SQLLite Database
            //services.AddTransient(serviceProvider => new PostRepository(@"Data Source=PostDatabase.db"));
            services.AddTransient<IPostRepository>(serviceProvider => new PostRepository(@"Data Source=PostDatabase.db"));
            services.AddCors();
            services.AddControllers();

            //dependencies
            services.AddDependecies();

            //swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "PostsApi",
                    Version = "v1"
                });
            });

            //odata
            services.AddOData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //swagger
            app.UseSwagger();

            // Disable CORS
            app.UseCors(x => x
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .SetIsOriginAllowed(origin => true) // allow any origin
                   .AllowCredentials()); // allow credentials

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.yaml", "PostsApi v1"));

            app.UseRouting();

            app.UseAuthorization();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                //odata
                endpoints.EnableDependencyInjection();
                endpoints.Select().Filter().Expand().OrderBy().Count(QueryOptionSetting.Allowed).MaxTop(50);
                endpoints.MapODataRoute("odata", "odata", ODataExtensions.GetEdmModel());
            });
        }
    }
}