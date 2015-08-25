using System;
using System.Configuration;
using System.Threading.Tasks;
using Telegram.Bot;

namespace GuidTelegramBot.Core
{
    public class GuidBot : IBot
    {
        private readonly Api _bot;

        private int _offset;

        public event EventHandler<MessageInfoEventArgs> MessageReceived;

        public GuidBot()
        {
            var token = ConfigurationManager.AppSettings["apiToken"];
            _bot = new Api(token);
            _offset = 0;
        }

        public async Task Do()
        {
            const string defaultHelpText = "Send any text to generate new GUID.";

            while (true)
            {
                var updates = await _bot.GetUpdates(_offset);

                foreach (var update in updates)
                {
                    var message = update.Message;
                    var messageText = message.Text;
                    var chatId = message.Chat.Id;
                    if (messageText != null)
                    {
                        var replyText = defaultHelpText;
                        if (messageText.ToUpper() != "/HELP" && messageText.ToUpper() != "HELP")
                        {
                            replyText = Guid.NewGuid().ToString();
                        }
                        await _bot.SendTextMessage(chatId, replyText);
                    }
                    var messageInfo = new MessageInfoEventArgs
                    {
                        ChatId = chatId,
                        Text = messageText,
                        UserName = message.From.Username,
                    };
                    await RaiseMessageReceivedEvent(messageInfo);

                    _offset = update.Id + 1;
                }

                await Task.Delay(1000);
            }
        }

        private async Task RaiseMessageReceivedEvent(MessageInfoEventArgs messageInfo)
        {
            var eventHandler = MessageReceived;
            if (eventHandler != null)
            {
                eventHandler(this, messageInfo);
            }
        }
    }
}