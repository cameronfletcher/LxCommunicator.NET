namespace LxCommunicator.NET.Example.Http
{
    using Loxone.Communicator;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Threading.Tasks;

    internal class Program
    {
        private static HttpWebserviceClient client;

        private static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets("f2514628-398e-4dde-8d97-80a5aeb20769")
                .Build();

            var host = config["LoxoneHost"];
            var username = config["LoxoneUsername"];
            var password = config["LoxonePassword"];

            using (client = new HttpWebserviceClient(
                host,
                443,
                2,
                "098802e1-02b4-603c-ffffeee000d80cfd",
                "LxCommunicator.NET.Http"))
            {
                using (TokenHandler handler = new TokenHandler(client, username))
                {
                    handler.SetPassword(password);
                    await client.Authenticate(handler);
                    string version = (await client.SendWebservice(new WebserviceRequest<string>("jdev/cfg/version", EncryptionType.Request))).Value;
                    Console.WriteLine($"Version: {version}");
                    await handler.KillToken();
                    Console.ReadLine();
                }
            }
        }
    }
}
