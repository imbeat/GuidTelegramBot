using System;
using System.Configuration;
using System.Threading.Tasks;
using Telegram.Bot;

namespace GuidTelegramBot
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Run().Wait();
        }
        static async Task Run()
        {
            var token = ConfigurationManager.AppSettings["apiToken"];
            var bot = new Api(token);

            var me = await bot.GetMe();

            Console.WriteLine("Hello my name is {0}", me.Username);

            var offset = 0;

            while (true)
            {
                var updates = await bot.GetUpdates(offset);

                foreach (var update in updates)
                {
                    if (update.Message.Text != null)
                        await bot.SendTextMessage(update.Message.Chat.Id, Guid.NewGuid().ToString());

                    offset = update.Id + 1;
                }

                await Task.Delay(1000);
            }
        }
    }
}