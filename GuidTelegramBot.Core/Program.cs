using System;
using System.Threading.Tasks;
using Telegram.Bot;

namespace GuidTelegramBot.Core
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Run().Wait();
        }
        static async Task Run()
        {
            var bot = new Api("115571224:AAFe4gEZ4b9hzlwtZrfaS-4HITTuN-Pi9rU");

            var me = await bot.GetMe();

            Console.WriteLine("Hello my name is {0}", me.Username);

            var offset = 0;

            while (true)
            {
                var updates = await bot.GetUpdates(offset);

                foreach (var update in updates)
                {
                    if (update.Message.Text != null)
                        await bot.SendTextMessage(update.Message.Chat.Id, update.Message.Text);

                    offset = update.Id + 1;
                }

                await Task.Delay(1000);
            }

        }
    }
}