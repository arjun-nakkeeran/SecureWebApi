using Microsoft.AspNetCore.Authentication.JwtBearer;
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
                    //IssuerSigningKey = Todo: Add logic to retrieve and set the signing key if necessary
                };
            });
    }

    public void ConfigureAuthorization(IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(SecurityConstants.EditPolicy, policyBuilder => policyBuilder.RequireClaim("scope", SecurityConstants.EditScope));
            options.AddPolicy(SecurityConstants.DeletePolicy, policyBuilder => policyBuilder.RequireClaim("scope", SecurityConstants.DeleteScope));
        });
    }
}
