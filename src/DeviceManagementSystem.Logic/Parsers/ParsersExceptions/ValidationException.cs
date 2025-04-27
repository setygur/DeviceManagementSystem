namespace DeviceManagementSystem.Logic.Parsers.ParsersExceptions;

public class ValidationException : Exception
{
    public ValidationException() { }

    public ValidationException(string message) 
        : base(message) { }
}