using System.Threading.Tasks;
using GuidTelegramBot.Core;

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
            var guidBot = new GuidBot();
            await guidBot.Do();
        }
    }
}