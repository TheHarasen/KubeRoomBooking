﻿namespace AuthDomain.Exceptions.Auth;

public class RefreshTokenException(string message) : Exception(message);