using System;

namespace GuidTelegramBot.Core
{
    public class MessageInfoEventArgs : EventArgs
    {
        public string Text { get; set; }

        public int ChatId { get; set; }

        public string UserName { get; set; }
    }
}