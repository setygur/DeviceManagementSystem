using System.Text.Json;
using System.Text.Json.Nodes;
using DeviceManagementSystem.Logic;
using DeviceManagementSystem.Objects.Devices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

//TODO revamp
// app.MapGet("/api/devices", () => DeviceService.GetAll());
// app.MapGet("/api/devices/{id}", (string id) => DeviceService.GetById(id));

//TODO add update

//TODO revamp
// app.MapDelete("api/device/{id}", (string id) => DeviceService.Delete(id));

app.Run();