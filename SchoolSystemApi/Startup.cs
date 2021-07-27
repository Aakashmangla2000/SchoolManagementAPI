using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SchoolSystem.Business.Business.Concrete;
using SchoolSystem.Business.Business.Contract;
using SchoolSystem.Repository.Repository.Concrete;
using SchoolSystem.Repository.Repository.Contract;
using StudentSystem.Abstractions.Models;
using System;
using System.IO;
using System.Reflection;

namespace SchoolSystemApi
{
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
            services.Configure<StudentDatabaseSettings>(Configuration.GetSection("StudentDatabaseSettings"));


            services.AddSingleton<IStudentDatabaseSettings>(
                        serviceProvider => serviceProvider
                        .GetRequiredService<IOptions<StudentDatabaseSettings>>()
                        .Value);

            services.AddTransient<IStudentBusiness, StudentBusiness>();
            services.AddTransient<ITeacherBusiness, TeacherBusiness>();

            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "School API",
                    Description = "An API for School Database Management",
                    Contact = new OpenApiContact
                    {
                        Name = "Aakash Mangla",
                        Email = "aakashmangla15@gmail.com",
                        Url = new Uri("https://aakashmangla.web.app"),
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
