using System;
using ClassLibrary;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using ClassLibrary.TelebotUtils;

namespace ClassLibrary
{
    public class Telebot
    {
        public static TelegramBotClient botClient = new TelegramBotClient("7189734909:AAHa_j9Qf4HAaCGXGicPORFtRzeh3cDieuU");
        public static void Start() //Launching the bot.
        {
            using CancellationTokenSource cts = new();

            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

            var me = botClient.GetMeAsync(); 

            Console.WriteLine($"Start listening for @{me}");
            Console.ReadLine();

            // Send cancellation request to stop bot.
            cts.Cancel();



            async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {
                if (update.Message is not { } message)
                    return;
                // Only process text messages.
               
                ReplyUtils.ReplyToMessageHandler(botClient, update, cancellationToken);
            }

            Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
            {
                var ErrorMessage = exception switch
                {
                    ApiRequestException apiRequestException
                        => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                    _ => exception.ToString()
                };

                return Task.CompletedTask;
            }
        }
    }
}