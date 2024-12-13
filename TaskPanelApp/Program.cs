using BusinessLogic;
using BusinessLogic.Interfaces;
using DataAccess;
using Domain;
using DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using TaskPanelApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContext<ContextoSql>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUsuarioLogic, UsuarioLogic>();
builder.Services.AddScoped<IEquipoLogic, EquipoLogic>();
builder.Services.AddScoped<IPanelLogic, PanelLogic>();
builder.Services.AddScoped<ITareaLogic, TareaLogic>();
builder.Services.AddScoped<IEpicaLogic, EpicaLogic>();
builder.Services.AddScoped<IComentarioLogic, ComentarioLogic>();
builder.Services.AddScoped<IPapeleraLogic, PapeleraLogic>();
builder.Services.AddSingleton<SesionLogic>();
builder.Services.AddScoped<RepositorioEquipo>();
builder.Services.AddScoped<RepositorioUsuario>();
//builder.Services.AddScoped<EstadoDeAutenticacion>();


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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();