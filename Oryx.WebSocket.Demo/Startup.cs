using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oryx.WebSocket.Demo.Handler;
using Oryx.WebSocket.Extension.Builder;
using Oryx.WebSocket.Extension.DependencyInjection;

namespace Oryx.WebSocket.Demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddOryxWebSocket();
            services.AddSingleton<TestHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc();

            //      ע��websocket route �� ������
            //      ע��, ��������Ҫ��service��ע��
            //      �������Ĳ����Ļ�,  ��Program.cs �ļ��н�������.

            //      register websocket route and handler 
            //       Note : You must injection handler class in ConfigureServices method
            //       If you need more ConcurrentsConnections , please check Program.cs
            app.UserOryxWebSocket(options =>
            {
                options.Register("/ws", serviceProvider.GetService<TestHandler>());
            });
        }
    }
}
