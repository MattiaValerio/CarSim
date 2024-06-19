using CarSim.BackEnd.Context;
using CarSim.BackEnd.lib;
using CarSim.BackEnd.Services.CarServices;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

var Configuration = builder.Configuration;
builder.Services.AddDbContext<DataContext>(options =>
        options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();


// register the car service 
builder.Services.AddScoped<ICarService, CarService>();


// add the schema filter, show the enum values in the swagger
builder.Services.AddSwaggerGen(cfg => cfg.SchemaFilter<EnumSchemaFilter>());

var app = builder.Build();

// initial migration
DatabaseManagementService.MigrateDatabase(app); 

app.UseStaticFiles();
app.UseRouting();

// create a cors policy to allow all origins
app.UseCors(builder =>
       builder.WithOrigins("http://localhost:8080")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials());

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

