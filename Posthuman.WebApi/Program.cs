using Microsoft.EntityFrameworkCore;
using Posthuman.Core;
using Posthuman.Core.Repositories;
using Posthuman.Core.Services;
using Posthuman.Data;
using Posthuman.Services;
using Posthuman.Data.Repositories;
using System.Reflection;
using Posthuman.Core.Models.DTO;
using PosthumanWebApi.Controllers;

var builder = WebApplication.CreateBuilder(args);

BuildServices();

BuildAutoMapper();

BuildSwagger();

var app = builder.Build();

try
{
    using (var scope = app.Services.CreateScope())
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

    //app.MapControllerRoute(
    //    name: "default",
    //    pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();

}
catch(Exception e)
{
    int x = 0;
    x = x + 5;

}


void BuildAutoMapper()
{
    // TODO: fix
    var assembly1 = typeof(TodoItemDTO).Assembly;
    var name = assembly1.GetName().Name;

    var assembly2 = typeof(ProjectsController).Assembly;
    var name2 = assembly1.GetName().Name;

    builder.Services.AddAutoMapper(new Assembly[] { assembly1, assembly2 });

}

void BuildServices()
{
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
    builder.Services.AddTransient<IEventItemsService, EventItemsService>();
    builder.Services.AddTransient<IAvatarsService, AvatarsService>();
}

void BuildSwagger()
{
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new() { Title = "PosthumanWebApi", Version = "v1" });
    });

}


