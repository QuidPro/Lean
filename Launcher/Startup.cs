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
                    System.Collections.Specialized.NameValueCollection parsedString = System.Web.HttpUtility.ParseQueryString(context.Request.QueryString.Value);
                    string botId = parsedString.Get("botId");
                    string startTime = parsedString.Get("startTime");
                    string endTime = parsedString.Get("endTime");
                    Console.WriteLine(botId);
                    Console.WriteLine(startTime);
                    Console.WriteLine(endTime);

                    
                    string json = $"{{ \"botId\":\"{botId}\",\"startTime\":\"{startTime}\",\"endTime\":\"{endTime}\"}}";
                    System.IO.File.WriteAllText(@"/Storage/data.json", json);

                    Environment.SetEnvironmentVariable("BOT_ID", botId,EnvironmentVariableTarget.Machine);
                    Environment.SetEnvironmentVariable("START_TIME", startTime, EnvironmentVariableTarget.Machine);
                    Environment.SetEnvironmentVariable("END_TIME", endTime, EnvironmentVariableTarget.Machine);
                    Program.Run();
                    await context.Response.WriteAsync($"Backtest started!\n");
                });
            });
        }
    }
}