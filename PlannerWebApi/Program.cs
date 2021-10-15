using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PosthumanWebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var pid = System.Diagnostics.Process.GetCurrentProcess().Id;

string connectionString = builder.Configuration.GetConnectionString("PosthumanDatabaseConnectionString");

builder
    .Services
    .AddDbContext<PosthumanContext>(
        options => options.UseSqlServer("Data Source=DMITRUSPACE\\SQLEXPRESS;Initial Catalog=PosthumanaeArchivae;Integrated Security=True"));

builder.Services.AddCors();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "PosthumanWebApi", Version = "v1" });
});


var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

app.UseCors(builder =>
{
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    
    app.UseSwagger();
    app.UseSwaggerUI(c => 
        c.SwaggerEndpoint(
            "/swagger/v1/swagger.json", 
            "PosthumanWebApi v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

