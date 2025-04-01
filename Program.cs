using BorrowingSystemAPI.Context;
using BorrowingSystemAPI.Interfaces.Repository;
using BorrowingSystemAPI.Mapper;
using BorrowingSystemAPI.Repositories;
using BorrowingSystemAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<BorrowingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Services

builder.Services.AddScoped<UserService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
