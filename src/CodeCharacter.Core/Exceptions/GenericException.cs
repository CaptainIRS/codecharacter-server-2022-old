namespace CodeCharacter.Core.Exceptions;

/// <summary>
///     Generic error with message
/// </summary>
public class GenericException : Exception
{
    /// <inheritdoc />
    public GenericException()
    {
    }

    /// <inheritdoc />
    public GenericException(string? message) : base(message)
    {
    }

    /// <inheritdoc />
    public GenericException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}