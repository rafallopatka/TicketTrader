using Microsoft.AspNetCore.Builder;

namespace TicketTrader.Identity.Extensions
{
    public static class SecurityMiddlewareExtensions
    {
        public static IApplicationBuilder UseSecurityOptions(this IApplicationBuilder app, params string[] allowedOrigins)
        {
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Remove("X-Frame-Options");

                var allowFrom = string.Join(" ", allowedOrigins);
                context.Response.Headers.Add("X-Frame-Options", $"ALLOW-FROM {allowFrom}");

                await next();
            });

            app.UseCsp(options => options
                .DefaultSources(s => s.Self())
                .FontSources(s => s.Self().CustomSources("https://fonts.gstatic.com"))
                .ScriptSources(s => s.Self().UnsafeInline())
                .StyleSources(s => s.Self().UnsafeInline().CustomSources("https://fonts.googleapis.com"))
                .FrameAncestors(s => s.CustomSources(allowedOrigins)));

            return app;
        }
    }
}
