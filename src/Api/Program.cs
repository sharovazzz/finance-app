using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Data;
using PersonalFinanceApp.Interfaces;
using PersonalFinanceApp.Repository;
using PersonalFinanceApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepository, UserDbRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IUserCategoryRepository, UserCategoryDbRepository>();
builder.Services.AddScoped<IUserCategoryService, UserCategoryService>();

builder.Services.AddScoped<IUserExpenseRepository, UserExpenseDbRepository>();
builder.Services.AddScoped<IUserExpenseService, UserExpenseService>();

builder.Services.AddScoped<IUserBudgetRepository, UserBudgetDbRepository>();
builder.Services.AddScoped<IUserBudgetService, UserBudgetService>();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

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
