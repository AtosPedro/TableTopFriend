# Domain Models

## User

```csharp
public sealed class User
{
    User Create(
            string firstName,
            string lastName,
            string email,
            string plainPassword,
            UserRole userRole,
            DateTime createdAt);

    void Update(
        string firstName,
        string lastName,
        string email,
        string plainPassword,
        UserRole userRole,
        DateTime updatedAt);

    void MarkToDelete(DateTime deletedAt);

    bool IsValidPassword(string plainPassword);
}
```

```json
{
    "Id": { "value": "00000000-0000-0000-0000-00000000000" },
    "FirstName":"John",
    "LastName":"Doe",
    "Role" : 1,
    "Email":"johndoe@email.com",
    "Password":"6237184",
    "PasswordSalt":"uihzxbcgh64812baaf78",
    "createdAt":"2023-01-01T00:00:00.0000000Z",
    "updatedAt":"2023-01-01T00:00:00.0000000Z",
}
```
