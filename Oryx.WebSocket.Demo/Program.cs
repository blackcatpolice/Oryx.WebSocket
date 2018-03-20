using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Oryx.WebSocket.Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                //注意: 此处为dotnet core 线程池, 按需设置, 默认为256
                //         这意味着, 即便下面MaxConcurrentUpgradedConnections 值设置 够高, 应用程序也
                //         最多只能处理256条连接, 剩余链接则需等待, 
                //         若剩余等待的链接数超出了MaxConcurrentUpgradedConnections 设置的值, 应用程序则会返回500
                //
                //Note : This method is dotnet core thread pool, the default pool number is 256,
                //           This means that , even if you set  MaxConcurrentUpgradedConnections a big value, the application only have 256 concurrent thread,
                //           more of 256 connection will wait, and if the waiting threads numbe more than MaxConcurrentUpgradedConnections value, 
                //          application will response 500
                //.UseLibuv(options =>
                //{
                //    options.ThreadCount = 2000;
                //})
                .UseHttpSys(options =>
                {
                    options.MaxAccepts = 10000;
                    options.MaxConnections = 10000;
                    options.RequestQueueLimit = 10000;
                })
                .UseKestrel(options =>
                {
                    options.Limits.MaxConcurrentConnections = 10000;
                    options.Limits.MaxConcurrentUpgradedConnections = 10000;
                })
                .Build();
    }
}
