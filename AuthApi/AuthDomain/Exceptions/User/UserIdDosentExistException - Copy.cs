namespace AuthDomain.Exceptions.User;

public class UserIdDosentExistException(string id) : Exception("Cant find user with Id: " + id);