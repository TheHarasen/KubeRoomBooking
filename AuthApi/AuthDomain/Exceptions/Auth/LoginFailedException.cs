﻿namespace AuthDomain.Exceptions.Auth;

public class LoginFailedException(string email) : Exception($"Invalid email: {email} or password");