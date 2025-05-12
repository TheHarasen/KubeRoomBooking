using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SubApplication.Options;
using System.Text;

namespace SubApi;

public static class DependencyInjection
{
	public static void SetupBuilder(this WebApplicationBuilder builder)
	{
		builder.Services.Configure<CorsOptions>(
			builder.Configuration.GetSection(CorsOptions.CorsOptionsKey));

		builder.Services.AddCors(options =>
		{
			var corsConfig = builder.Configuration
				.GetSection(CorsOptions.CorsOptionsKey)
				.Get<CorsOptions>();

			options.AddPolicy("localreactpolicy", policy =>
			{
				policy
					.AllowAnyHeader()
					.AllowAnyMethod()
					.WithOrigins(corsConfig?.AllowedOrigins ?? [])
					.AllowCredentials();
			});
		});

		builder.Services.Configure<JwtOptions>(
			builder.Configuration.GetSection(JwtOptions.JwtOptionsKey));

		builder.Services.AddAuthentication(opt =>
		{
			opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			opt.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(options =>
		{
			var jwtOptions = builder.Configuration.GetSection(JwtOptions.JwtOptionsKey)
				.Get<JwtOptions>() ?? throw new ArgumentException(nameof(JwtOptions));

			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = jwtOptions.Issuer,
				ValidAudience = jwtOptions.Audience,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
			};

			options.Events = new JwtBearerEvents
			{
				OnMessageReceived = context =>
				{
					context.Token = context.Request.Cookies["ACCESS_TOKEN"];
					return Task.CompletedTask;
				}
			};
		});
	}
}
