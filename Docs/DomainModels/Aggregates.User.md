# Domain Models

## User

```csharp
public sealed class User
{
    User Create(UserId id, string firstName, string lastName, string email, string password, string passwordSalt);
}
```

```json
{
    "Id": { "value": "00000000-0000-0000-0000-00000000000" },
    "FirstName":"John",
    "LastName":"Doe",
    "Email":"johndoe@email.com",
    "Password":"6237184",
    "PasswordSalt":"uihzxbcgh64812baaf78",
}
```
