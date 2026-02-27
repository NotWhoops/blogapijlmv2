using blogapijlmv2.Properties.Services;
using blogapijlmv2.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<BlogItemService>();
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<UserService>();

var connectionString = builder.Configuration.GetConnectionString
("MyBlogString2");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
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
