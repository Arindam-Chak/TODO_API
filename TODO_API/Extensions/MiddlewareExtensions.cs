using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using System.Reflection;
using TODO_API.Controllers.Resources;

namespace ToDoAPI.Extensions
{
	public static class MiddlewareExtensions
	{
		public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(cfg =>
			{
				cfg.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "Todo API with OAuth",
					Version = "v1",
					Description = "Example API that shows how to implement JSON Web Token authentication and authorization with ASP.NET Core 3.1, built from scratch.",
					Contact = new OpenApiContact
					{
						Name = "Arindam Chakraborty",
						
					},
					License = new OpenApiLicense
					{
						Name = " ",
					},
				});

				cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "JSON Web Token to access resources. Example: Bearer {token}",
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey
				});

				cfg.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
						},
						new [] { string.Empty }
					}
				});

				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile.Replace("_", ""));
				cfg.IncludeXmlComments(xmlPath);
			});

			return services;
		}

		public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
		{
			app.UseSwagger().UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API");
				options.DocumentTitle = "Todo API";
			});

			return app;
		}
		public static void ConfigureExceptionHandler(this IApplicationBuilder app)
		{
			app.UseExceptionHandler(appError =>
			{
				appError.Run(async context =>
				{
					IExceptionHandlerFeature contextFeature = context.Features.Get<IExceptionHandlerFeature>();
					if (contextFeature?.Error != null)
					{
						int statusCode = StatusCodes.Status409Conflict;
						string result = String.Empty;
						TodoErrorResponse errorModel = null;

						if (contextFeature.Error is TODOCustomError customException)
						{
							errorModel = new TodoErrorResponse(customException.ResponseStatusCode, customException.ErrorMessage);
							statusCode = customException.ResponseStatusCode;

						}
						else
						{

							errorModel = new TodoErrorResponse(statusCode, contextFeature.Error.Message);
						}

						context.Response.StatusCode = statusCode;
						context.Response.ContentType = "application/json";
						result = JsonConvert.SerializeObject(errorModel, new StringEnumConverter());
						await context.Response.WriteAsync(result).ConfigureAwait(false);
					}
				});
			}); 
		}
	}
}
