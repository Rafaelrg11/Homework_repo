using Homework;
using Homework.Operations;
using Microsoft.EntityFrameworkCore;
using Homework.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<AuthorOperation>();
builder.Services.AddScoped<BookOperation>();
builder.Services.AddScoped<LoanOperation>();
builder.Services.AddScoped<UserOperarion>();
builder.Services.AddScoped<AuxiliarTableLoanOperation>();

builder.Services.AddDbContext<HomeworkContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("cadenaSQL"))
);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
