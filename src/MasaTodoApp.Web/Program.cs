using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MasaTodoApp.Web.ApiCallers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMasaBlazor();

builder.Services.Configure<TodoServiceOptions>(builder.Configuration.GetSection("TodoService"))
.AddAutoRegistrationCaller(typeof(Program).Assembly);

var app = builder.Build();


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
