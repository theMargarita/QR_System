using Application.IServices;
using Application.Services;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace QR_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Logging
            builder.Services.AddLogging();

            // Services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IQrCodeService, QrCodeService>();
            builder.Services.AddScoped<IContextService, ContextService>();
            builder.Services.AddScoped<IContextPartService, ContextPartService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            //builder.Services.AddScoped<IUserTabService, UserTabService>();

            // Controllers
            builder.Services.AddControllers();

            // Swagger / OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Database
            builder.Services.AddDbContext<QrDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Middleware pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run(); 
        }
    }
}
