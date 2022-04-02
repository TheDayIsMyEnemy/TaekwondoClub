using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApi.Mappings;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var allowAllCorsPolicy = "Allow All Cors Policy";

builder.Services.AddDbContext<TaekwondoClubContext>(options =>
{
    options.UseSqlServer(config.GetConnectionString("TaekwondoClubConnection"));
});

builder.Services.AddAutoMapper(typeof(TaekwondoClubProfile));

builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        //o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        //o.JsonSerializerOptions.MaxDepth = 0;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IMembershipRepository, MembershipRepository>();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IUploadStudentsCsvFileService, UploadStudentsCsvFileService>();
builder.Services.AddScoped<IMembershipService, MembershipService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(allowAllCorsPolicy,
        builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(allowAllCorsPolicy);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
