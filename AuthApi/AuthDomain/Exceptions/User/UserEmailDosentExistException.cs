namespace AuthDomain.Exceptions.User;

public class UserEmailDosentExistException(string email) : Exception("Cant find user with Email: " + email);