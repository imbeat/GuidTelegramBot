using System.ServiceProcess;
using System.Threading.Tasks;
using GuidTelegramBot.Core;

namespace GuidTelegramBot.WindowsService
{
    partial class GuidTelegramBotService : ServiceBase
    {
        public GuidTelegramBotService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            RunBot().Wait();
        }

        private async Task RunBot()
        {
            var guidBot = new GuidBot();
            await guidBot.Do();
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}