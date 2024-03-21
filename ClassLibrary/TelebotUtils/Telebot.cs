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
        public static void Start()
        {
            var botClient = new TelegramBotClient("7189734909:AAHa_j9Qf4HAaCGXGicPORFtRzeh3cDieuU");

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

            var me = botClient.GetMeAsync(); //------------await

            Console.WriteLine($"Start listening for @{me}");
            Console.ReadLine();

            // Send cancellation request to stop bot
            cts.Cancel();



            async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {
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

                Console.WriteLine(ErrorMessage);
                return Task.CompletedTask;
            }
        }
    }
}