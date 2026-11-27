using System;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using KooliProjekt.Application.Authentication.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
namespace KooliProjekt.WebAPI.Authentication
{
    public sealed class JwtAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly JwtSettings _settings;
        public JwtAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, JwtSettings settings) : base(options, logger, encoder) => _settings = settings;
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var header = Request.Headers.Authorization.ToString();
            if (!header.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)) return Task.FromResult(AuthenticateResult.NoResult());
            try
            {
                var validation = new TokenValidationParameters { ValidateIssuer = true, ValidIssuer = _settings.Issuer, ValidateAudience = true, ValidAudience = _settings.Audience, ValidateIssuerSigningKey = true, IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key)), ValidateLifetime = true, ClockSkew = TimeSpan.FromMinutes(1), NameClaimType = ClaimTypes.Name, RoleClaimType = ClaimTypes.Role };
                var principal = new JwtSecurityTokenHandler().ValidateToken(header.Substring(7), validation, out _);
                return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal, Scheme.Name)));
            }
            catch (Exception ex) { return Task.FromResult(AuthenticateResult.Fail(ex)); }
        }
        protected override Task HandleChallengeAsync(AuthenticationProperties properties) { Response.StatusCode = 401; return Task.CompletedTask; }
    }
}
