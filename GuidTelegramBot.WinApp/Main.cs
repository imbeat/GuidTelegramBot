using System;
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
            _bot = new GuidBot();
            _bot.MessageReceived += (sender, args) => txtLog.AppendText(string.Format("{0} : {1}{2}", args.UserName, args.Text, Environment.NewLine));
            this.Resize += Main_Resize;
            this.notifyIcon.MouseDoubleClick += formNotifyIcon_MouseDoubleClick;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            await _bot.Do();
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
    }
}