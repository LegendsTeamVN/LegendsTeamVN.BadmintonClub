using LegendsTeamVN.BadmintonClub.Application.Features.Auth.Register;
using LegendsTeamVN.BadmintonClub.Application.Features.Auth.Login;
using LegendsTeamVN.Core.Presentation.Abstractions;
using LegendsTeamVN.Core.Presentation.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using LegendsTeamVN.BadmintonClub.Application.Features.Auth.Logout;
using System.Security.Claims;
using LegendsTeamVN.BadmintonClub.Application.Features.Auth.Refresh;

namespace LegendsTeamVN.BadmintonClub.Presentation.Endpoints;

public class AuthEndpoint : EndpointGroupBase
{
    protected override string Name => "auth";

    protected override void Map(RouteGroupBuilder group)
    {
        group.MapPost("register", Register)
             .WithName("RegisterUser")
             .WithSummary("Register a new user")
             .WithDescription("Creates a new user account.");

        group.MapPost("login", Login)
             .WithName("LoginUser")
             .WithSummary("Login to the system")
             .WithDescription("Returns a JWT access token upon successful login and sets a refresh token cookie.");
             
        group.MapPost("refresh", Refresh)
             .WithName("RefreshToken")
             .WithSummary("Refresh access token")
             .WithDescription("Uses the refresh token from cookies and access token from Authorization header to issue a new access token.");

        group.MapPost("logout", Logout)
             .WithName("LogoutUser")
             .WithSummary("Logout from the system")
             .WithDescription("Revokes the session and clears the refresh token cookie.")
             .RequireAuthorization();

    }

    private static async Task<IResult> Register(RegisterCommand command, ISender sender)
    {
        var result = await sender.Send(command);

        return result.Match(
            onSuccess: id => Results.Ok(new { UserId = id, Message = "User created successfully." })
        );
    }

    private static async Task<IResult> Login(LoginQuery query, ISender sender, HttpContext context)
    {
        var result = await sender.Send(query);

        return result.Match(
            onSuccess: response =>
            {
                if (!string.IsNullOrEmpty(response.RefreshToken))
                {
                    context.Response.Cookies.Append("refreshToken", response.RefreshToken, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = response.RefreshTokenExpiryTime
                    });
                }
                
                return Results.Ok(new { AccessToken = response.AccessToken });
            }
        );
    }
    
    private static async Task<IResult> Refresh(HttpContext context, ISender sender)
    {
        var refreshToken = context.Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(refreshToken))
        {
            return Results.Unauthorized();
        }

        var authorizationHeader = context.Request.Headers.Authorization.FirstOrDefault();
        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
        {
            return Results.Unauthorized();
        }

        var accessToken = authorizationHeader.Substring("Bearer ".Length).Trim();
        var command = new RefreshTokenCommand(accessToken, refreshToken);
        var result = await sender.Send(command);

        return result.Match(
            onSuccess: response =>
            {
                if (!string.IsNullOrEmpty(response.RefreshToken))
                {
                    context.Response.Cookies.Append("refreshToken", response.RefreshToken, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = response.RefreshTokenExpiryTime
                    });
                }
                return Results.Ok(new { AccessToken = response.AccessToken });
            }
        );
    }

    private static async Task<IResult> Logout(HttpContext context, ISender sender)
    {
        var email = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(email)) return Results.Unauthorized();

        var command = new LogoutCommand(email);
        await sender.Send(command);

        context.Response.Cookies.Delete("refreshToken");
        return Results.Ok(new { Message = "Logged out successfully." });
    }

}
