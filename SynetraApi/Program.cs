﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SynetraApi.Data;
using SynetraApi.Models;
using SynetraApi.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SynetraApiContext") ?? throw new InvalidOperationException("Connection string 'SynetraApiContext' not found.")));
// Add services to the container.
builder.Services.AddAuthentication();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<DataContext>();
builder.Services.AddScoped<IParcService, ParcService>();
builder.Services.AddScoped<IComputerService, ComputerService>();
var MyAllowSpecificOrigins = "_MyAllowSubdomainPolicy";
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("_myAllowSpecificOrigins", builder =>
     builder.WithOrigins("https://localhost:7057/")
      .SetIsOriginAllowed((host) => true)
      .AllowAnyMethod()
      .AllowAnyHeader()
      .AllowCredentials());
});

var app = builder.Build();
app.UseCors("_myAllowSpecificOrigins");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapIdentityApi<User>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
