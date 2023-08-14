using DevsuTest.DependencyInjection;
using DevsuTest.Context.Contexts;
using DevsuTest.Application;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using DevsuTest.Core.Middleware;
using DevsuTest.InputFormatter;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddControllers(options => options.InputFormatters.Insert(0, JsonPatchInputFormatter.GetJsonPatchInputFormatter()))
            .AddNewtonsoftJson()
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<DevsuDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DevsuDB")));

        builder.Services.AddServiceDependencies();
        builder.Services.AddApplication();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}