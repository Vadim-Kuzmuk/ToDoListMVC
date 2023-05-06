using GraphQL;
using ToDoListMVC.Context;
using ToDoListMVC.GraphQL.Schemas;
using ToDoListMVC.Repository;
using ToDoListMVC.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<RepositorySwitcher>();
builder.Services.AddControllersWithViews();

builder.Services.AddGraphQL(builder => builder
            .AddSystemTextJson()
            .AddSchema<TaskSchema>()
            .AddGraphTypes(typeof(TaskSchema).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseGraphQL<TaskSchema>();
app.UseGraphQLAltair();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
