using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pebtos.GatewayApi.Webhook
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds GatewayAPI JWT token validation for all actions decorated with the attribute <see cref="GatewayApiWebhookAttribute"/>.
        /// </summary>
        /// <param name="setupAction">Action used for configuring the webhook. Make sure to set the JWT secret to the value specified in the GatewayAPI administration.</param>
        /// <returns></returns>
        public static IServiceCollection SetupGatewayApiWebhook(this IServiceCollection services, Action<WebhookOptions> setupAction)
        {
            services.AddTransient<GatewayApiWebhookFilter>();

            var options = new WebhookOptions();
            services.AddSingleton(options);

            setupAction?.Invoke(options);

            return services;
        }

        /// <summary>
        /// Adds GatewayAPI JWT token validation for all actions decorated with the attribute <see cref="GatewayApiWebhookAttribute"/>.
        /// </summary>
        /// <param name="jwtSecret">The JWT secret specified in the GatewayAPI administration.</param>
        /// <returns></returns>
        public static IServiceCollection SetupGatewayApiWebhook(this IServiceCollection services, string jwtSecret)
        {
            services.AddTransient<GatewayApiWebhookFilter>();

            var options = new WebhookOptions
            {
                JwtSecret = jwtSecret
            };
            services.AddSingleton(options);

            return services;
        }
    }
}
