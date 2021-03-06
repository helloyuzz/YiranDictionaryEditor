﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration {
            get;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddRazorPages().AddRazorRuntimeCompilation();

            //services.AddDbContext<WebAppContext>(options => options.UseSqlServer(Configuration.GetConnectionString("WebAppContext")));
            var connection = "Filename=./demo.db";
            services.AddDbContext<WebAppContext>(options => options.UseSqlite(connection));

            services.Configure<RazorPagesOptions>(options => options.RootDirectory = "/Pages");

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddRazorPagesOptions(options => {
            //    options.RootDirectory = "/DbSchema";
            //    options.Conventions.AddPageRoute("/Index","");
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,IWebHostEnvironment env) {
            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapRazorPages();
            });
        }
    }
}
