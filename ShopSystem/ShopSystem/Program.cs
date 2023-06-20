using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using ShopSystem.Data;
using ShopSystem.IService;
using ShopSystem.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling=ReferenceLoopHandling.Ignore;
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    options.SerializerSettings.DateTimeZoneHandling=Newtonsoft.Json.DateTimeZoneHandling.Local;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
//描述api保护方式
c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    Description = "使用Bearer方法的jwt授权报头",
    Name = "Authorization",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.Http,
    Scheme = "bearer"
});
//在api中添加全局要求
c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});
builder.Services.AddDbContext<ShopDbContext>(option =>
{
    option.UseMySQL(builder.Configuration.GetConnectionString("ShopConn"));
});
//, IEmployeeService, EmployeeService
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IProductService, ProductService>();

//配置跨域服务
builder.Services.AddCors(options =>
{
    options.AddPolicy("cors", p =>
    {
        p.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection(nameof(AuthSettings)));
//builder.Services.AddScoped<IUserService ,UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("cors");

app.UseMiddleware<JwtMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
