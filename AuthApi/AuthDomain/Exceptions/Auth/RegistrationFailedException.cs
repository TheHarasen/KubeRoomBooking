namespace AuthDomain.Exceptions.Auth;

public class RegistrationFailedException(IEnumerable<string> errors) 
	: Exception($"Registrations failed with following errors: {string.Join(Environment.NewLine, errors)}");
