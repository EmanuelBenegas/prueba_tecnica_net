
using Prueba_Tecnica_Net.Mapping;
using Services.Services.Interfaces;
using Services.Services.Service;
using Services.Repositories.Interfaces;
using Prueba_Tecnica_Net.Options;
using Services.DapperContext;

namespace Prueba_Tecnica_Net
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IRetailerService, RetailerService>();
            builder.Services.AddScoped<IRetailerRepository, RetailerRepository>();
            builder.Services.AddScoped<IExternalApiService, ExternalApiService>();

            builder.Services
                  .AddOptions<ApiRetailerOptions>()
                  .Bind(builder.Configuration.GetSection(ApiRetailerOptions.Key));

            builder.Services.AddSingleton<AppDapperContext>();

            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
