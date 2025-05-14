using System.Text.Json;
using DeviceManagementSystem.Database.Repositories.RepositoryExceptions;
using DeviceManagementSystem.Logic.Parsers;
using DeviceManagementSystem.Logic.Parsers.ParsersExceptions;
using DeviceManagementSystem.Logic.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine(connectionString);
builder.Services.AddTransient<IDeviceService, DeviceService>(
    _ => new DeviceService(connectionString));

// Add services to the container.
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

app.UseHttpsRedirection();

app.MapGet("/api/devices", (IDeviceService deviceService) =>
{
    try
    {
        return Results.Ok(deviceService.GetAllDevices());
    }
    catch (Exception e)
    {
        return Results.Problem();
    }
});
app.MapGet("/api/devices/{id}", (IDeviceService deviceService, string id) =>
{
    try
    {
        return Results.Ok(deviceService.GetDeviceById(id));
    }
    catch (NotFoundException e)
    {
        return Results.NotFound();
    }
    catch (Exception e)
    {
        return Results.Problem();
    }
});

app.MapPost("/api/devices", async (IDeviceService deviceService, HttpRequest request) =>
    {
        var parser = DeviceParserFactory.GetParser(request.ContentType);
        if (parser == null)
        {
            return Results.StatusCode(StatusCodes.Status415UnsupportedMediaType);
        }

        try
        {
            var device = await parser.ParseAsync(request.Body);

            if (device is null)
            {
                return Results.BadRequest("Invalid data structure.");
            }

            var result = deviceService.CreateDevice(device);
            if (result is true)
            {
                return Results.Created($"/api/devices/{device.Id}", device);
            }
            else
            {
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        catch (ValidationException e)
        {
            return Results.BadRequest($"Validation failed: {e.Message}");
        }
        catch (JsonException e)
        {
            return Results.BadRequest($"Invalid JSON: {e.Message}");
        }
        catch (ArgumentException e)
        {
            return Results.BadRequest($"Invalid Arguments: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    })
    .Accepts<string>("application/json", ["text/plain"]);

app.MapPut("/api/devices", async (IDeviceService deviceService, HttpRequest request) =>
    {
        var parser = DeviceParserFactory.GetParser(request.ContentType);
        if (parser == null)
        {
            return Results.StatusCode(StatusCodes.Status415UnsupportedMediaType);
        }
        
        try
        {
            var device = await parser.ParseAsync(request.Body);

            if (device is null)
            {
                return Results.NotFound();
            }

            var result = deviceService.UpdateDevice(device);
            if (result is true)
            {
                return Results.NoContent();
            }
            else
            {
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        catch (ValidationException e)
        {
            return Results.BadRequest($"Validation failed: {e.Message}");
        }
        catch (JsonException e)
        {
            return Results.BadRequest($"Invalid JSON: {e.Message}");
        }
        catch (ArgumentException e)
        {
            return Results.BadRequest($"Invalid Arguments: {e.Message}");
        }
        catch (Exception e)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }).Accepts<string>("application/json", ["text/plain"]);

app.MapDelete("api/devices/{id}", (IDeviceService deviceService, string id) =>
{
    try
    {
        return Results.Ok(deviceService.DeleteDeviceById(id));
    }
    catch (NotFoundException e)
    {
        return Results.NotFound();
    }
    catch (Exception e)
    {
        return Results.Problem();
    }
});

app.Run();