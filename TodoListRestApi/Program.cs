using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TodoListRestApi.Data;
using TodoListRestApi.Helpers;
using TodoListRestApi.Services;
using TodoListRestApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseSqlServer());

builder.Services.AddScoped<JwtService>();

builder.Services.AddCors();

builder.Services.AddTransient<ITodosService, TodosService>();
builder.Services.AddTransient<IUsersService, UsersService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => options
       .WithOrigins(new[] { "https://localhost:44398" })
       .AllowAnyHeader()
       .AllowAnyMethod()
       .AllowCredentials());

app.UseAuthorization();

app.MapControllers();

app.Run();
