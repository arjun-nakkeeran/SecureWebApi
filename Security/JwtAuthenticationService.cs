using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyMicroservice.Security;

public class JwtAuthenticationService
{
    private readonly IConfiguration _configuration;

    public JwtAuthenticationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureAuthentication(IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Audience = _configuration["Jwt:Audience"];
                options.Authority = _configuration["Jwt:Issuer"];
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                };
            });
    }

    public void ConfigureAuthorization(IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(SecurityConstants.EditPolicy, policy => policy.RequireClaim("scope", SecurityConstants.EditScope));
            options.AddPolicy(SecurityConstants.DeletePolicy, policy => policy.RequireClaim("scope", SecurityConstants.DeleteScope));
        });
    }
}
