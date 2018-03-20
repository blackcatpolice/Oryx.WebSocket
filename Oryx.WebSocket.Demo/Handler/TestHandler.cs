using Oryx.WebSocket.Infrastructure;
using Oryx.WebSocket.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Oryx.WebSocket.Demo.Handler
{
    public class TestHandler : IOryxHandler
    {
        public async Task OnClose(OryxWebSocketMessage msg)
        {
            await Task.Run(() =>
             {
                 Debug.WriteLine(msg.Message);
             });
        }

        public async Task OnReciveMessage(OryxWebSocketMessage msg)
        {
            await Task.Run(() =>
            {
                Debug.WriteLine(msg.Message);
            });
        }
    }
}
