using AuthDomain.Entities;

namespace AuthDomain.Requests;

public record EditSelfRequest(Guid Id, string Email, string PasswordOld, string PasswordNew, string FirstName, string LastName);
