using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace SMS.Tools.Tools
{
    public abstract class StartupTemplate
    {
        public IConfiguration? Configuration { get; set; }

        public abstract void ConfigureServices(IServiceCollection services);
        public abstract void ConfigureApp(WebApplication app, IWebHostEnvironment env);
    }
}
