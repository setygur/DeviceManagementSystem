namespace DeviceManagementSystem.Logic.Services.ServicesExceptions;

public class NotFoundException : Exception
{
    public NotFoundException() { }

    public NotFoundException(string message) 
        : base(message) { }
}