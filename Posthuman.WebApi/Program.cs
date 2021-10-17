using Microsoft.EntityFrameworkCore;
using Posthuman.Core;
using Posthuman.Core.Repositories;
using Posthuman.Core.Services;
using Posthuman.Data;
using Posthuman.Services;
using Posthuman.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var pid = System.Diagnostics.Process.GetCurrentProcess().Id;

string connectionString = builder.Configuration.GetConnectionString("PosthumanDatabaseConnectionString");

builder
    .Services
    .AddDbContext<PosthumanContext>(
        options => options
        .UseSqlServer("Data Source=DMITRUSPACE\\SQLEXPRESS;Initial Catalog=PosthumanaeArchivae2;Integrated Security=True", 
        x => x.MigrationsAssembly("Posthuman.Data")));

builder.Services.AddCors();
builder.Services.AddControllers();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<ITodoItemsRepository, TodoItemsRepository>();
builder.Services.AddTransient<IProjectsRepository, ProjectsRepository>();
builder.Services.AddTransient<IEventItemsRepository, EventItemsRepository>();
builder.Services.AddTransient<IAvatarsRepository, AvatarsRepository>();

builder.Services.AddTransient<ITodoItemsService, TodoItemsService>();
builder.Services.AddTransient<IProjectsService, ProjectsService>();

builder.Services.AddAutoMapper(typeof(StartupBase));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "PosthumanWebApi", Version = "v1" });
});


var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    //SeedData.Initialize(services);
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

