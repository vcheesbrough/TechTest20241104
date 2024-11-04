using CustomerApi.Data;
using CustomerApi.Data.Models;
using CustomerApi.FluentValidation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CustomerApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<CustomerContext>(opt =>
            opt.UseInMemoryDatabase("Customers"));
        builder.Services.AddControllers();
        builder.Services.AddAutoMapper(typeof(CustomerDao));
        builder.Services.AddValidatorsFromAssemblyContaining<CustomerDtoValidator>();
        
        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapControllers()
            .WithOpenApi();

        app.Run();
    }
}