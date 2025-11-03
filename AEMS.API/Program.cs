using IMS.DataAccess.Repositories;
using IMS.API.Utilities;
using IMS.Business.DTOs.Requests;
using IMS.Domain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using IMS.Business.Services;
using ZMS.API.Utilities;
using IMS.Business.Configurations;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    //options.ExampleFilters();
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.UseAllOfToExtendReferenceSchemas();
    options.OperationFilter<AuthResponsesOperationFilter>();
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlBusinessFilename = $"{Assembly.GetAssembly(typeof(LoginReq))?.GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlBusinessFilename));
});



builder.InjectAllServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
        //.WithOrigins(
        //    "http://localhost:3000",
        //    "http://localhost:3001",
        //    "http://localhost:3000/",
        //    "http://localhost:3001/",
        //    "https://localhost:3000",
        //    "https://localhost:3001",
        //    "http://localhost:3001",
        //    "http://127.0.0.1:5500",
        //    "http://onlineilm.com"
        //    )
               .AllowAnyHeader()
               .AllowAnyMethod();
               //.AllowCredentials();
        //.WithExposedHeaders("content-type", "Token");
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

// Configure Mapster to prevent circular references
MapsterConfig.Configure();

app.UseCors("AllowAll");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    //foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
    //{
    //    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
    //        "API " + description.GroupName.ToUpperInvariant());
    //}

    options.ConfigObject.PersistAuthorization = true;
    options.ConfigObject.DeepLinking = true;
    options.ConfigObject.Filter = "";
    options.DocumentTitle = "IMS - API";
});

app.UseHttpsRedirection();
app.UseCors();




app.MapControllers();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

TryRunMigration(app);

async void TryRunMigration(IHost webApplication)
{

    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    dbContext.Database.Migrate();
    // Now, Execute Data Seeding 2.0:
    //dbContext.SeedData();
}

app.Run();