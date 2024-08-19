#region Microsoft

global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using Microsoft.OpenApi.Models;
global using Microsoft.AspNetCore.Diagnostics;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.OpenApi.Any;
global using Microsoft.AspNetCore.Cryptography.KeyDerivation;
global using Microsoft.AspNetCore.Authorization;

#endregion

#region System

global using System.Security.Cryptography;
global using System.Threading.RateLimiting;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Diagnostics;
global using System.Net.Http.Headers;

#endregion

#region Others

global using Asp.Versioning;
global using Carter;
global using FluentValidation;
global using MediatR;
global using MinimalApi.Data;
global using Swashbuckle.AspNetCore.SwaggerGen;

#endregion