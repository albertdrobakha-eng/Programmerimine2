using Microsoft.Extensions.Configuration;

namespace KooliProjekt.Application.Authentication.Jwt
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpireMinutes { get; set; }

        public JwtSettings(IConfiguration config)
        {
            var section = config.GetSection("Jwt");
            Key = section["Key"];
            Issuer = section["Issuer"];
            Audience = section["Audience"];
            ExpireMinutes = int.Parse(section["ExpireMinutes"] ?? "60");
        }
    }
}
