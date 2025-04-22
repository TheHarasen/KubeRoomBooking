namespace AuthDomain.Requests;

public record DeleteSelfRequest(Guid Id, string Email, string Password);
