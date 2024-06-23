namespace Application.Errors.Common;

public class CustomException : Exception
{
    public Error Error { get; }

    public CustomException(Error error)
        : base(error.Description)
    {
        Error = error;
    }
}
