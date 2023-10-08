using Console_Api.Interfaces;
using Console_Api.Models.Data;
using Console_Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddTransient<CustomMiddleware1>();


            //services.TryAddTransient<IProductRepository, TestRepository>();
            services.TryAddTransient<CosMosDBContext, CosMosDBContext>();
            services.TryAddTransient<IProgramService, ProgramService>();
            services.TryAddTransient<IApplicationService, ApplicationService>();
            services.TryAddTransient<IWorkFlowService, WorkFlowService>();
            services.TryAddTransient<IPreviewService, PreviewService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
