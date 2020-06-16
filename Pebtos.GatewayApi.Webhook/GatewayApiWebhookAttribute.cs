using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Logging;

namespace Pebtos.GatewayApi.Webhook
{
    internal class GatewayApiWebhookFilter : ActionFilterAttribute
    {
        private readonly WebhookOptions _options;

        public GatewayApiWebhookFilter(WebhookOptions options)
        {
            _options = options;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await _options.OnTokenValidatingAsync(new TokenValidatingContext());

            if (context.HttpContext.Request.Headers.TryGetValue(_options.HeaderName, out var jwtValues))
            {
                var jwtToken = jwtValues.FirstOrDefault();
                if (jwtToken == null)
                {
                    await _options.OnForbiddenAsync(new ForbiddenContext($"Did not find any JWT token in header {_options.HeaderName}"));
                    context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                }
                else
                {
                    var keyBytes = Encoding.ASCII.GetBytes(_options.JwtSecret ?? string.Empty);
                    if (keyBytes.Length < 16)
                        Array.Resize(ref keyBytes, 16);

                    var key = new SymmetricSecurityKey(keyBytes);
                    
                    try
                    {
                        var tokenHandler = new JwtSecurityTokenHandler();

                        var claimsPrincipal = tokenHandler.ValidateToken(jwtToken, new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = key,
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateActor = false,
                            ValidateTokenReplay = false,
                            ValidateLifetime = false
                        }, out var validatedToken);
                        
                        await _options.OnTokenValidatedAsync(new TokenValidatedContext
                        {
                            Principal = claimsPrincipal
                        });
                        await next();
                    }
                    catch(Exception e)
                    {
                        await _options.OnForbiddenAsync(new ForbiddenContext($"Token validation failed. Exception message was: {e.Message}"));
                        context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                    }
                }
            }
            else
            {
                await _options.OnForbiddenAsync(new ForbiddenContext($"Did not find any JWT token in header {_options.HeaderName}"));
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
        }
    }

    /// <summary>
    /// Validates whether a request is really comming from GatewayAPI.
    /// </summary>
    public class GatewayApiWebhookAttribute : Attribute, IFilterFactory
    {
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService<GatewayApiWebhookFilter>();
        }
    }
}
