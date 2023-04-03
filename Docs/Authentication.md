# Authorization

## Token

The app use authentication by jwt bearer token. The interface responsible for the generation of the token is the IJwtTokenGenerator that is located in the path 'DDDTableTopFriend.Application\\Common\\Interfaces\\Authentication\\IJwtTokenGenerator.cs'.

The interface has one contract that is the following.

```csharp
public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string firstName, string lastName);
}
```

It generates te token using the Options in the JwtSettings class that is following the Option Pattern and can be encountered on  the path 'DDDTableTopFriend.Infrastructure\\Authentication\\JwtSettings.cs' 

The class is the flowing:

```csharp
public class JwtSettings
{
    public const string SectionName = "JwtSettings";
    public string Secret { get; init; } = null!;
    public int ExpiratoryMinutes { get; init; }
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
}
```

## Api

On the API there is two contracts that abstract the properties to the endpoints of the authorization. The first one is the RegisterRequest that is a record with only init properties. bellow is the code of the record.

'DDDTableTopFriend.Contracts\\Authentication\\RegisterRequest.cs'

```csharp
public record RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password);
```

This request leads to a command  DDDTableTopFriend.Application\\Authentication\\Register\\Commands\\RegisterCommand.cs

```csharp
public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;
```

And this command has a validator that can be encountered in the path: 'DDDTableTopFriend.Application\\Authentication\\Register\\Commands\\RegisterCommandValidator.cs'

```csharp
public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress();
            
        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .NotNull()
            .Length(3, 20);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .NotNull()
            .Length(3, 20);
    }
}
```

the following endpoint is used for register a user.

``` http
POST {{host_https}}/{{api_v1_route}}/auth/register
Content-Type: application/json
{
    "firstName":"First",
    "lastName":"Last",
    "email":"testemail@gmail.com",
    "password":"testpassword"
}
```

And the response is a AuthenticationResponse encountered in the path 'DDDTableTopFriend.Contracts\\Authentication\\AuthenticationResponse.cs':

```json
HTTP/1.1 200 OK
{ 
    "id": "00000000-000-000-000-000000000000",
    "firstName": "First",
    "lastName": "Last",
    "email": "testemail@gmail.com",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI4NDJjNTZlZS0zNTZjLTRiYjMtYWY5OC0zYjQxMWJlOGE0YTciLCJqdGkiOiI5YjAyYWUzNy1iZWU2LTQ1ZmYtODM2ZC0wMmViMzM0MWVjM2MiLCJnaXZlbl9uYW1lIjoiZW1haWx0ZXN0ZUBnbWFpbC5jb20iLCJmYW1pbHlfbmFtZSI6IlBlZHJvIiwiZXhwIjoxNjgwNDY5Mjg0LCJpc3MiOiJUYWJsZVRvcCBGcmllbmQiLCJhdWQiOiJUYWJsZVRvcCBGcmllbmQifQ.NAHMC5AnzDfZsC-7nabonOS3x41L6aTzByqNPphBjJc"
}
```


The second contract is the LoginRequest that can be encountered in the path 'DDDTableTopFriend.Contracts\\Authentication\\LoginRequest.cs'

```csharp
public record LoginRequest(
    string Email,
    string Password);
```

This request leads to a Query  'DDDTableTopFriend.Application\\Authentication\\Login\\Queries\\LoginQuery.cs'

```csharp
public record LoginQuery(
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;
```

And this command has a validator that can be encountered in the path: ''

```csharp
public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress();
            
        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull();
    }
}
```

the following endpoint is used for authenticate a user.

``` http
POST {{host_https}}/{{api_v1_route}}/auth/login

Content-Type: application/json

{
    "email":"testemail@gmail.com",
    "password":"testpassword"
}
```

And the response is a AuthenticationResponse encountered in the path 'DDDTableTopFriend.Contracts\\Authentication\\AuthenticationResponse.cs':

```json
HTTP/1.1 200 OK
{ 
    "id": "00000000-000-000-000-000000000000",
    "firstName": "First",
    "lastName": "Last",
    "email": "testemail@gmail.com",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI4NDJjNTZlZS0zNTZjLTRiYjMtYWY5OC0zYjQxMWJlOGE0YTciLCJqdGkiOiI5YjAyYWUzNy1iZWU2LTQ1ZmYtODM2ZC0wMmViMzM0MWVjM2MiLCJnaXZlbl9uYW1lIjoiZW1haWx0ZXN0ZUBnbWFpbC5jb20iLCJmYW1pbHlfbmFtZSI6IlBlZHJvIiwiZXhwIjoxNjgwNDY5Mjg0LCJpc3MiOiJUYWJsZVRvcCBGcmllbmQiLCJhdWQiOiJUYWJsZVRvcCBGcmllbmQifQ.NAHMC5AnzDfZsC-7nabonOS3x41L6aTzByqNPphBjJc"
}
```
