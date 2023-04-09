# Domain Models

## User

```csharp
public sealed class User
{
    User Create(string firstName, string lastName, UserRole role, string email, string password, string passwordSalt);
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
}
```
