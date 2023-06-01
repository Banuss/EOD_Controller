using System;
using System.Threading;
using Websocket.Client;
using Newtonsoft.Json;

namespace EOD_WPF.Remote
{
    class Rumble
    {
        public string remoteURL { get; set; }

        private string streaming_API_Key = "YOUR_USER_KEY";

        public Rumble()
        {
            
        }



        public bool sendRumble(int intensity)
        {
            try
            {
                var exitEvent = new ManualResetEvent(false);
                var url = new Uri("wss://marketdata.tradermade.com/feedadv");

                using (var client = new WebsocketClient(url))
                {
                    client.ReconnectTimeout = TimeSpan.FromSeconds(30);
                    client.ReconnectionHappened.Subscribe(info =>
                    {

                    });
                    client.MessageReceived.Subscribe(msg =>
                    {
                        if (msg.ToString().ToLower() == "connected")
                        {
                            string data = "{\"userKey\":\"" + streaming_API_Key + "\", \"symbol\":\"EURUSD,GBPUSD,USDJPY\"}";
                            client.Send(data);
                        }
                    });
                    client.Start();
                    //Task.Run(() => client.Send("{ message }"));
                    exitEvent.WaitOne();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
