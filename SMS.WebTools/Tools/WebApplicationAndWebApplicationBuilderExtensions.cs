using Microsoft.AspNetCore.Builder;
using SMS.Tools.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.WebTools.Tools
{
    public static class WebApplicationAndWebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder RegisterServices<TStartup>(
            this WebApplicationBuilder builder) where TStartup : StartupTemplate, new()
        {
            var startup = new TStartup()
            {
                Configuration = builder.Configuration
            };

            startup.ConfigureServices(builder.Services);
            return builder;
        }

        public static WebApplication ConfigureUsing<TStartup>(
            this WebApplication app) where TStartup : StartupTemplate, new()
        {
            var startup = new TStartup()
            {
                Configuration = app.Configuration
            };

            startup.ConfigureApp(app, app.Environment);
            return app;
        }

        public static WebApplication CreateWebAppUsing<TStartup>(
            this WebApplicationBuilder builder) where TStartup : StartupTemplate, new()
        {
            return builder
                .RegisterServices<TStartup>()
                .Build()
                .ConfigureUsing<TStartup>();
        }
    }
}
