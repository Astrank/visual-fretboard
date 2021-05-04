using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spotify.Api.Services;

namespace Spotify.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<ISpotifyAccountService, SpotifyAccountService>(c => {
                c.BaseAddress = new Uri("https://accounts.spotify.com/api/token");
            });

            services.AddHttpClient<ISpotifyService, SpotifyService>(client => {
                client.BaseAddress = new Uri("https://api.spotify.com/v1/");
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options => 
            {
                options
                    .AllowAnyOrigin();
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern:"{controller}/{action}/{id?}");
            });
        }
    }
}
