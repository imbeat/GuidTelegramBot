using System.Threading.Tasks;
using GuidTelegramBot.Core;

namespace GuidTelegramBot.Console
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
            guidBot.MessageReceived+= (sender, args) => System.Console.WriteLine("{0} : {1}",args.UserName, args.Text);
            await guidBot.Do();
        }
    }
}