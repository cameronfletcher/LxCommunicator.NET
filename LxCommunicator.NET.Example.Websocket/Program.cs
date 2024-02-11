namespace LxCommunicator.NET.Example.Websocket
{
    using Loxone.Communicator;
    using Loxone.Communicator.Events;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    internal class Program
    {
        private static WebsocketWebserviceClient client;

        private static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets("f2514628-398e-4dde-8d97-80a5aeb20769")
                .Build();

            var host = config["LoxoneHost"];
            var username = config["LoxoneUsername"];
            var password = config["LoxonePassword"];

            // new WebsocketWebserviceClient("testminiserver.loxone.com", 7777, 2, "098802e1-02b4-603c-ffffeee000d80cfd", "LxCommunicator.NET.Websocket")
            using (client = new WebsocketWebserviceClient(
                host,
                443,
                2,
                "098802e1-02b4-603c-ffffeee000d80cfd",
                "LxCommunicator.NET.Websocket"))
            {
                using (TokenHandler handler = new TokenHandler(client, username))
                {
                    handler.SetPassword(password);
                    client.OnReceiveEventTable += Client_OnReceiveEventTable;
                    client.OnAuthenticated += Client_OnAuthenticated;
                    await client.Authenticate(handler);
                    string result = (await client.SendWebservice(new WebserviceRequest<string>("jdev/sps/enablebinstatusupdate", EncryptionType.None))).Value;
                    Console.ReadLine();
                    await handler.KillToken();
                }
            }
        }

        private static void Client_OnAuthenticated(object sender, ConnectionAuthenticatedEventArgs e)
        {
            Console.WriteLine("Successfully authenticated!");
        }

        private static void Client_OnReceiveEventTable(object sender, EventStatesParsedEventArgs e)
        {
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Received event table with {e.States.Count()} states:");

            foreach (EventState state in e.States)
            {
                Console.WriteLine(state.ToString());
            }

            Console.WriteLine();
        }
    }
}
