using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using GuidTelegramBot.Core;

namespace GuidTelegramBot.WinApp
{
    public partial class Main : Form
    {
        private readonly GuidBot _bot;

        public Main()
        {
            InitializeComponent();
            notifyIcon.Visible = false;
            btnStop.Enabled = false;
            _bot = new GuidBot();
            _bot.MessageReceived += (sender, args) => txtLog.AppendText(string.Format("{0} : {1}{2}", args.UserName, args.Text, Environment.NewLine));
            this.Resize += Main_Resize;
            this.notifyIcon.MouseDoubleClick += formNotifyIcon_MouseDoubleClick;
            this.notifyIcon.MouseClick += formNotifyIcon_MouseDoubleClick;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (_bot.State == BotState.Stopped)
            {
                btnStart.Enabled = false;
                btnStop.Enabled = true;

                Task.Factory.StartNew(() => _bot.Start()).ConfigureAwait(false);

                txtLog.AppendText("Старт!" + Environment.NewLine);
            }
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            // проверяем наше окно, и если оно было свернуто, делаем событие        
            if (WindowState == FormWindowState.Minimized)
            {
                // прячем наше окно из панели
                this.ShowInTaskbar = false;
                // делаем нашу иконку в трее активной
                notifyIcon.Visible = true;
            }
        }

        private async void formNotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // делаем нашу иконку скрытой
            notifyIcon.Visible = false;
            // возвращаем отображение окна в панели
            this.ShowInTaskbar = true;
            //разворачиваем окно
            WindowState = FormWindowState.Normal;
        }

        private async void btnStop_Click(object sender, EventArgs e)
        {
            if (_bot.State == BotState.Working)
            {
                btnStop.Enabled = false;
                btnStart.Enabled = true;

                await _bot.Stop();

                txtLog.AppendText("Стоп!" + Environment.NewLine);
            }
        }
    }
}