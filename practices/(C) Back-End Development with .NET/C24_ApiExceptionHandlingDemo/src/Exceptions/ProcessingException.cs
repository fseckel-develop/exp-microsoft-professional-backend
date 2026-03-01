namespace ApiExceptionHandlingDemo.Exceptions;

public sealed class ProcessingException : Exception
{
    public ProcessingException(string message) : base(message)
    {
    }
}