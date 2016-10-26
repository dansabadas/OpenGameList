using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nelibur.ObjectMapper;
using OpenGameListWebApp.Data;
using OpenGameListWebApp.Data.Items;
using OpenGameListWebApp.ViewModels;
using System;

namespace OpenGameListWebApp
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      // Add framework services.
      services.AddMvc();

      // Add EntityFramework's Identity support.
      services.AddEntityFramework();

      //// Add ApplicationDbContext.
      services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

      // Add ApplicationDbContext's DbSeeder
      services.AddSingleton<DbSeeder>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, DbSeeder dbSeeder)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      // Serve static files (html, css, js, images & more). See also the following URL:
      // https://docs.asp.net/en/latest/fundamentals/static-files.html for further reference.
      app.UseStaticFiles(new StaticFileOptions
      {
        OnPrepareResponse = context =>
        {
          var headers = context.Context.Response.Headers;
                  // Disable caching for all static files.
                  headers["Cache-Control"] = Configuration["StaticFiles:Headers:Cache-Control"];
          headers["Pragma"] = Configuration["StaticFiles:Headers:Pragma"];
          headers["Expires"] = Configuration["StaticFiles:Headers:Expires"];
        }
      });
      //app.UseStaticFiles();
      app.UseMvc();

      // TinyMapper binding configuration
      TinyMapper.Bind<Item, ItemViewModel>();

      // Seed the Database (if needed)
      try
      {
        dbSeeder.SeedAsync().Wait();
      }
      catch (AggregateException e)
      {
        throw new Exception(e.ToString());
      }
    }
  }
}
