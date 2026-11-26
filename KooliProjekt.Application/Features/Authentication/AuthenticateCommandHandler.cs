using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Authentication;
using KooliProjekt.Application.Authentication.Jwt;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Authentication
{
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, OperationResult<string>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly JwtTokenBuilder _tokenBuilder;

        public AuthenticateCommandHandler(ApplicationDbContext dbContext, JwtTokenBuilder tokenBuilder)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _tokenBuilder = tokenBuilder ?? throw new ArgumentNullException(nameof(tokenBuilder));
        }

        public async Task<OperationResult<string>> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<string>();
            if (request == null || string.IsNullOrWhiteSpace(request.UserName) || request.Password == null)
                return result.AddError("Invalid user name or password.");

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName, cancellationToken);
            if (user == null || !PasswordHelper.Verify(request.Password, user.PasswordHash))
                return result.AddError("Invalid user name or password.");

            var claims = new System.Collections.Generic.List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            if (user.IsAdmin) claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            result.Value = _tokenBuilder.GenerateToken(claims.ToArray());
            return result;
        }
    }
}
