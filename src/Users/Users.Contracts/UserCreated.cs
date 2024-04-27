namespace Users.Contracts;

public record UserCreated(Guid Id, string Username, string FirstName, string LastName, string Email, bool EmailConfirmed, int Role);