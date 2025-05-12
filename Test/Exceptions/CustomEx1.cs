namespace Template.Exceptions;

public class CustomEx1 : Exception
{
    public CustomEx1()
    {
    }

    public CustomEx1(string? message) : base(message)
    {
    }

    public CustomEx1(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}