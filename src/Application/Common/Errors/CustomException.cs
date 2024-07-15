namespace Application.Common.Errors;

public class CustomException : Exception
{
    public Error Error { get; }

    public CustomException(Error error)
        : base(error.Description)
    {
        Error = error;
    }
}
