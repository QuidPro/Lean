using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace QuantConnect.Lean.Launcher
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    //TODO: Get bot Id from URL and set
                    //localhost:8080?{botId}
                    char[] ignoreChars = { '?' };
                    string botId = context.Request.QueryString.Value.TrimStart(ignoreChars);
                    Console.WriteLine(botId);
                    Environment.SetEnvironmentVariable("BOT_ID", botId);
                    Program.Run();
                    await context.Response.WriteAsync($"Backtest started!\n");
                });
            });
        }
    }
}