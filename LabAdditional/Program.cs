using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using LabAdditional.Data;

var builder = WebApplication.CreateBuilder(args);

// ВАЖНО: запускать через dotnet run --urls http://0.0.0.0:5001
// В настройках сетей винды поставить общий доступ в т.ч. для файлов и устройства в одной сети
// с другого устройства запрашивать по ip:5001 другого устройства (ipconfig чтобы узнать)

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(b =>
        b.SetIsOriginAllowed(_ => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors();

app.MapBlazorHub();
app.MapControllers();
app.MapFallbackToPage("/_Host");

app.Run();