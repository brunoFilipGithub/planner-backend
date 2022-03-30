using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using planner_backend.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddCors(options =>
{
  options.AddPolicy(name: "cors",
                    builder =>
                    {
                      builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                    });
});

builder.Services.AddDbContextPool<EventsContext>(options =>
{
  var connetionString = builder.Configuration.GetConnectionString("EventsContext");
  options.UseMySql(connetionString, ServerVersion.AutoDetect(connetionString));
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.UseCors("cors");

app.MapControllers();

app.Run();
