namespace GuidTelegramBot.Core
{
    /// <summary>
    /// Состояние бота
    /// </summary>
    public enum BotState
    {
        /// <summary>
        /// Остановлен
        /// </summary>
        Stopped,
        /// <summary>
        /// Останавливается
        /// </summary>
        Stopping,
        /// <summary>
        /// Работает
        /// </summary>
        Working,
    }
}