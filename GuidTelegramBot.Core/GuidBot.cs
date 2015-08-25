using System;
using System.Configuration;
using System.Threading.Tasks;
using Telegram.Bot;

namespace GuidTelegramBot.Core
{
    /// <summary>
    /// Бот для Telegram, генерирующий GUID'ы
    /// </summary>
    public class GuidBot : IBot
    {
        private readonly Api _bot;

        private int _offset;

        private BotState _state;

        public GuidBot()
        {
            var token = ConfigurationManager.AppSettings["apiToken"];
            _bot = new Api(token);
            _state = BotState.Stopped;
            _offset = 0;
        }

        /// <summary>
        /// Текущее состояние бота
        /// </summary>
        public BotState State
        {
            get { return _state; }
            //set { _state = value; }
        }

        //public async Task Pause()
        //{
        //    _state = BotState.Stopped;
        //}
        //public async Task Resume()
        //{
        //    _state = BotState.Working;
        //    await Start();
        //}

        public async Task Start()
        {
            if (_state == BotState.Working)
            {
                throw new InvalidOperationException("Бот уже запущен");
            }
            _state = BotState.Working;
            const string defaultHelpText = "Send any text to generate new GUID.";

            while (State == BotState.Working)
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
            _state = BotState.Stopped;
        }

        /// <summary>
        /// Событие "Получено новое сообщение"
        /// </summary>
        public event EventHandler<MessageInfoEventArgs> MessageReceived;

        public async Task Stop()
        {
            _state = BotState.Stopping;
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