namespace Server.Shared.Exceptions;

public class ForbiddenException(string message) : Exception(message);