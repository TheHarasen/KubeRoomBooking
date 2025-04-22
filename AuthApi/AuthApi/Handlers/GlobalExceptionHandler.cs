using AuthDomain.Exceptions.Auth;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace AuthApi.Handlers;

public class GlobalExceptionHandler : IExceptionHandler
{
	private readonly ILogger<GlobalExceptionHandler> _logger;

	public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
	{
		_logger = logger;
	}

	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		var (statuscode, message) = GetExceptionDetails(exception);

		_logger.LogError(exception, exception.Message);

		httpContext.Response.StatusCode = (int)statuscode;
		await httpContext.Response.WriteAsJsonAsync(message, cancellationToken);

		return true;
	}

	private (HttpStatusCode statuscode, string message) GetExceptionDetails(Exception exception)
	{
		return exception switch
		{
			LoginFailedException => (HttpStatusCode.Unauthorized, exception.Message),
			UserAlreadyExistsException => (HttpStatusCode.Conflict, exception.Message),
			RegistrationFailedException => (HttpStatusCode.BadRequest, exception.Message),
			RefreshTokenException => (HttpStatusCode.Unauthorized, exception.Message),
			_=> (HttpStatusCode.InternalServerError, $"An expected error occurred: {exception.Message}")
		};
	}
}
