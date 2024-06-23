using Backend.Api.Currency;

namespace Backend.Api;

public class Program
{
    public static void Main(string[] args)
    {
        const string allowAllOrigins = "AllowAllOrigins";
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddCurrency();
        
        builder.Services.AddControllers();
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(opt =>
        {
            opt.AddPolicy(name: allowAllOrigins, builderOptions =>
            {
                builderOptions.AllowAnyOrigin();
                builderOptions.AllowAnyHeader();
                builderOptions.AllowAnyMethod();
            });
        });
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors(allowAllOrigins);
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}