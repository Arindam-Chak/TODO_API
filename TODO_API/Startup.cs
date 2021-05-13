using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using ToDoAPI.Core.Repositories;
using ToDoAPI.Core.Security.Hashing;
using ToDoAPI.Core.Security.Tokens;
using ToDoAPI.Core.Services;
using ToDoAPI.Extensions;
using ToDoAPI.Persistence;
using ToDoAPI.Security.Hashing;
using ToDoAPI.Security.Tokens;
using ToDoAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TODO_API.Persistence;
using TODO_API.Core.Services;
using TODO_API.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using TODO_API.Controllers.Resources;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ToDoAPI
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<AppDbContext>(options =>
			{
				options.UseInMemoryDatabase("ToDoAPI");
			});
			
			var connection = Configuration.GetConnectionString("TODOConnectionString");
			services.AddDbContext<TodoDBContext>(options =>
			{
				options.UseSqlServer(connection);
			});
			services.AddControllers();

			services.AddCustomSwagger();

			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			services.AddSingleton<IPasswordHasher, PasswordHasher>();
			services.AddSingleton<ITokenHandler, Security.Tokens.TokenHandler>();

			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IAuthenticationService, AuthenticationService>();
			services.AddScoped<ITodoService, TodoService>();
			
			services.Configure<TokenOptions>(Configuration.GetSection("TokenOptions"));
			var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

			var signingConfigurations = new SigningConfigurations(tokenOptions.Secret);
			services.AddSingleton(signingConfigurations);

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(jwtBearerOptions =>
				{
					jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = tokenOptions.Issuer,
						ValidAudience = tokenOptions.Audience,
						IssuerSigningKey = signingConfigurations.SecurityKey,
						ClockSkew = TimeSpan.Zero
					};
				});

			services.AddAutoMapper(this.GetType().Assembly);
			
		}

		public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseDeveloperExceptionPage();

			app.UseRouting();

			app.UseCustomSwagger();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
			app.ConfigureExceptionHandler();
		}
	}
}