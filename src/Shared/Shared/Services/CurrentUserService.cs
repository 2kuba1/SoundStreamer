﻿using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Shared.Services;

public interface ICurrentUserService
{
    public ClaimsPrincipal? User { get; }
    public Guid? Id { get; }
}

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;

    public Guid? Id => User.FindFirst(c => c.Type == "Id") is null
        ? Guid.Empty
        : Guid.Parse(User.FindFirst(c => c.Type == "Id").Value);
}