#region Builder Object
using AutoMapper;
using Common;
using DataAcces;
using Domain.UserService;
using Mapping;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using ServiceCollection;
using System.Globalization;
using System.Reflection;
using System.Text.Json.Serialization;
#endregion


#region Builder Object
var builder = WebApplication.CreateBuilder(args);
#endregion

#region General Dependency
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped(typeof(Configuration));
builder.Services.AddMemoryCache();
#endregion

//Avoid cycles in json deserialization.
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

#region Swagger Config
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Jelmi Api",
            Version = "v1",
            Description = "The Test.",
            Contact = new OpenApiContact()
            {
                Email = "soporte@jelmi.co",
                Name = "Jelmi",
                Url = new Uri("https://jelmi.com.co/")
            },
        });

    // Set the comments path for the Swagger JSON and UI.
    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //c.IncludeXmlComments(xmlPath);
});
#endregion

#region IoC

//Registra contexto de la bd.
builder.Services.AddDbContext<ContextDb>( Options =>
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//Registra la inversion de control
IoC.AddDependency(builder.Services);

#endregion

#region Auto Mapper Config
//Registra automapper
var mapperConfig = new MapperConfiguration(m => { m.AddProfile(new MappingProfile()); });
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

#endregion

#region app configuration

var app = builder.Build();
app.UseFileServer();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

app.UseHttpsRedirection();

app.MapControllers();

var supportedCultures = new[]
{
        new CultureInfo("es-CO")
    };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("es-CO"),
    // Formatting numbers, dates, etc.
    SupportedCultures = supportedCultures,
    // UI strings that we have localized.
    SupportedUICultures = supportedCultures
});

app.Run();

#endregion


//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
