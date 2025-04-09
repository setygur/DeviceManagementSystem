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

app.MapGet("/api/devices", () => DeviceService.GetAll());
app.MapGet("/api/device/{id}", (string id) => DeviceService.GetById(id));

//could not be done due to the abstract nature of Device class
// app.MapPost("api/device", (Device device) => DeviceService.CreateDevice(device));
app.MapPost("api/device/embeddeddevice", (EmbeddedDevice device) =>
    DeviceService.CreateEmbeddedDevice(device));
app.MapPost("api/device/personalcomputer", (PersonalComputer device) =>
    DeviceService.CreatePersonalComputer(device));
app.MapPost("api/device/smartwatch", (Smartwatch device) =>
    DeviceService.CreateSmartwatch(device));

app.MapPut("api/device/embeddeddevice/{id}", (string id, EmbeddedDevice device) =>
    DeviceService.UpdateEmbeddedDevice(id, device));
app.MapPut("api/device/personalcomputer/{id}", (string id, PersonalComputer device) =>
    DeviceService.UpdatePersonalComputer(id, device));
app.MapPut("api/device/smartwatch/{id}", (string id, Smartwatch device) =>
    DeviceService.UpdateSmartwatch(id, device));

app.MapDelete("api/device/{id}", (string id) => DeviceService.Delete(id));

app.Run();