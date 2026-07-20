# 🌐 API Design Guidelines

To ensure consistency and ease of integration for the Frontend / Mobile App teams, all software engineers developing new features at LegendsTeamVN must strictly adhere to the following API design rules.

*[Đọc bằng Tiếng Việt](API_GUIDELINES.VI.md)*

---

## 1. RESTful API Standards

We apply a **Resource-oriented** design pattern. Never embed verbs into the URL path except for highly specific actions.

### Naming Conventions:
- Use plural nouns for all endpoints.
- Write everything in lowercase and separate words with hyphens (`kebab-case`).

✅ **Do (RESTful Standard):**
- `GET /api/users` (Get user list)
- `GET /api/users/{id}` (Get user details)
- `POST /api/users` (Create new user)
- `PUT /api/users/{id}` (Update all information)
- `DELETE /api/users/{id}` (Delete user)

❌ **Don't (Anti-patterns):**
- `GET /api/get-all-users`
- `POST /api/users/create`
- `POST /api/users/{id}/delete`

### For Specific Actions:
When an action cannot be described by CRUD operations, append a verb after the resource identifier.
✅ `POST /api/users/{id}/activate` (Activate an account)

---

## 2. Uniform Response Format

This project **DOES NOT** wrap successful HTTP Responses in a complex custom envelope like `{ "isSuccess": true, "data": ... }`. Instead, we rely entirely on native **HTTP Status Codes**. However, internal code logic must handle flows precisely using the **Result Pattern**.

### In the Application Layer (CQRS Handler):
All Handlers must return a `Result<T>` type. You should never `throw Exception` to control business logic (e.g., when data is not found or data is invalid).
Instead, return an explicit error:

```csharp
// On Success
return Result.Success(userDto);

// On Failure
return Result.Failure<UserResponse>(DomainErrors.User.NotFound);
```

### In the Presentation Layer (Minimal API Endpoints):
The Endpoint is responsible for unwrapping the `Result<T>` and returning the correct HTTP standard response using the `.Match()` or `.MapToIResult()` method.

```csharp
private static async Task<IResult> GetUserById([AsParameters] GetUserRequest request, ISender sender)
{
    var result = await sender.Send(new GetUserQuery(request.Id));
    
    return result.Match(
        onSuccess: response => Results.Ok(response), // Returns HTTP 200 with DTO
        onFailure: error => Results.BadRequest(error) // Or automatically mapped to ProblemDetails
    );
}
```

---

## 3. HTTP Status Codes

Clients communicating with the system will rely 100% on HTTP status codes. You must return the correct code corresponding to the processing situation:

- **200 OK**: Request was successful and returned data (Usually for `GET`, `PUT`).
- **201 Created**: New data was successfully created (Usually for `POST`, returned with `Location` header).
- **204 No Content**: Processing was successful but no data is returned (Usually for `DELETE`).
- **400 Bad Request**: Malformed request, missing data, or business logic violation (Validation).
- **401 Unauthorized**: Missing JWT Token, expired Token, or invalid Token.
- **403 Forbidden**: Token is valid but the user **lacks the permission (Roles/Permissions)** to perform this action.
- **404 Not Found**: The queried resource does not exist in the Database.
- **409 Conflict**: Data conflict (e.g., Email already exists, Court is already booked).
- **500 Internal Server Error**: Unforeseen system crash (Unhandled Exceptions). This error is caught by Middleware and converted into a standard `ProblemDetails` format (RFC 7807) to avoid leaking stack traces.
