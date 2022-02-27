using Data;
using Data.Repositories;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebApi.Services;
using WebApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentMembershipRepository, StudentMembershipRepository>();

builder.Services.AddScoped<IStudentsCsvUploadService, StudentsCsvUploadService>();
builder.Services.AddScoped<IStudentMembershipService, StudentMembershipService>();


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

app.UseCors(policy => policy.AllowAnyOrigin());

app.Run();
