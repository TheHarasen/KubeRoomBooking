using AuthDomain.Entities;

namespace AuthDomain.Exceptions.User;

public class UserNotRightRoleException(Role was, Role shouldBe) : Exception(was+ " is the wrong role, requires " + shouldBe);