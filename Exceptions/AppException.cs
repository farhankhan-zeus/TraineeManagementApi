using System.Net;
namespace TraineeManagementApi.Exceptions;
public abstract class AppException : Exception
{
    public HttpStatusCode StatusCode { get; }

    protected AppException(string message,bool success=false, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        StatusCode = statusCode;
    }
}
public sealed class UnauthorizedException : AppException
{
    public UnauthorizedException(string message) : base(message,false, HttpStatusCode.Unauthorized)
    {
        
    }
}

public sealed class NotFoundException : AppException
{
    public NotFoundException(string resourceName, object key)
        : base($"{resourceName} with identifier '{key}' was not found.",false, HttpStatusCode.NotFound)
    {
    }
}

public sealed class BadRequestException : AppException
{
    public BadRequestException(string message)
        : base(message,false, HttpStatusCode.BadRequest)
    {
    }
}


public sealed class ConflictException : AppException
{
    public ConflictException(string message)
        : base(message,false, HttpStatusCode.Conflict)
    {
    }
}
public sealed class JwtOperationException : Exception
{
    public JwtOperationException()
        
    {
    }
}

public sealed class UnSupportedMediaType: AppException
{
    public UnSupportedMediaType(string message):base(message,false,HttpStatusCode.UnsupportedMediaType){}
}

public sealed class ValidationException : AppException
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException(IDictionary<string, string[]> errors)
        : base("One or more validation errors occurred.", false,HttpStatusCode.BadRequest)
    {
        Errors = errors;
    }

    public ValidationException(string field, string error)
        : base("One or more validation errors occurred.",false, HttpStatusCode.BadRequest)
    {
        Errors = new Dictionary<string, string[]>
        {
            { field, [error] }
        };
    }
}