namespace DeviceManagementSystem.Database.Repositories.RepositoryExceptions;

public class NotFoundException : Exception
{
    public NotFoundException() { }

    public NotFoundException(string message) 
        : base(message) { }
}