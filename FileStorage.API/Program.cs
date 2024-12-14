using FileStorage.Bussiness.Abstract;
using FileStorage.Bussiness.Concrete;
using FileStorage.DataAccess;
using FileStorage.DataAccess.Abstract;
using FileStorage.DataAccess.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IUserService, UserManager>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IFolderService, FolderManager>();
builder.Services.AddSingleton<IFolderRepository, FolderRepository>();
builder.Services.AddSingleton<IFileService, FileManager>();
builder.Services.AddSingleton<IFileRepository, FileRepository>();
builder.Services.AddSingleton<IPermissionService, PermissionManager>();
builder.Services.AddSingleton<IPermissionRepository, PermissionRepository>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.Name = "NetCookieAuthentication";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    //options.LoginPath = "";
    options.AccessDeniedPath = "";
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
