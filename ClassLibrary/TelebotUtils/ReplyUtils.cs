using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace ClassLibrary.TelebotUtils
{
    public class ReplyUtils //A class for user interaction.
    {
        public static ITelegramBotClient botClient;
        public static CancellationToken cancellationToken;
        static Update update;
        public static async Task ReplyToMessageHandler(ITelegramBotClient BotClient, Update Update, CancellationToken CancellationToken)
        {
            var message = Update.Message;
            var text = message.Text;
            var document = message.Document;
            long chatId = message.Chat.Id;
            botClient = BotClient;
            update = Update;
            cancellationToken = CancellationToken;
            if (text != null && Enterable.HighPriority.Contains(text.ToLower()))
            {
                Enterable.ToDefault();
                switch (text.ToLower())
                {
                    case "/start":
                        await SendSticker.SendHello(chatId);
                        SendStart(chatId); break;
                    case "главное меню":
                        MainMenu(chatId); break;
                    case "/help":
                        SendHelp(chatId); break;
                }
            }
            else if (Enterable.EnterField())
            {
                if (Enterable.csv_coverage_area)
                {
                    Enterable.csv_coverage_area = false;
                    bool result = FileProcessing.CsvSampleByCoverageArea(text);
                    ReplyToProcess(chatId, result, true);
                }
                else if (Enterable.csv_park_name)
                {
                    Enterable.csv_park_name = false;
                    bool result = FileProcessing.CsvSampleByParkName(text);
                    ReplyToProcess(chatId, result, true);
                }
                else if (Enterable.csv_adm_area)
                {
                    Enterable.csv_adm_area = false;
                    bool result = FileProcessing.CsvSampleByAdmArea(text);
                    ReplyToProcess(chatId, result, true);
                }

            }

            else if (text !=  null && document == null)
            {
                switch (text.ToLower())
                {
                    case "добавить csv файл":
                        Enterable.csv = true; 
                        InputCSVMessage(chatId); break;
                    case "добавить json файл":
                        Enterable.json = true;
                        InputJsonMessage(chatId); break;
                    case "продолжить с загруженным":
                        IsFileDonloaded(chatId); break;
                    case "выборка":
                        CsvSampleHandler(chatId); break;
                    case "сортировка":
                        CsvSortHandler(chatId); break;
                    case "coveragearea":
                        bool result;
                        if (Enterable.sample_csv)
                        {
                            InputCoverageArea(chatId);
                        }
                        else
                        {
                            Enterable.sort_csv = false;
                            FileProcessing.SortByName();
                            result = FileProcessing.SortByCoverageArea();
                            ReplyToProcess(chatId, result, false);
                            break;
                        }
                        break;
                    case "parkname":
                        InputParkName(chatId); break;
                    case "admarea и coveragearea":
                        InputAdmArea(chatId); break;
                    case "name":
                        Enterable.sort_csv = false;
                        FileProcessing.SortByName();
                        result = FileProcessing.SortByName();
                        ReplyToProcess(chatId, result, false);
                        break;
                    default:
                        InvalidMessage(chatId); break;
                }
            }
            else if (document != null)
            {
                if (Enterable.csv)
                {
                    InputCSV(chatId);
                }
                else if (Enterable.json)
                {
                    InputJson(chatId);
                }
                else
                {
                    InvalidFileInputPosition(chatId);
                }
            }
        }

        public static async Task SendStart(long chatId)
        {
            Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: Texts.HelloText,
                    replyMarkup: Keyboards.FileTypeKeyboard(),
                    cancellationToken: cancellationToken);
        }

        public static async Task MainMenu(long chatId)
        {
            Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: Texts.MainMenu,
                    replyMarkup: Keyboards.FileTypeKeyboard(),
                    cancellationToken: cancellationToken);
        }

        public static async Task SendHelp(long chatId)
        {
            SendSticker.SendHelp(chatId);
            Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Помощь уже в пути...!",
                    replyMarkup: Keyboards.MainMenuKeyboard(),
                    cancellationToken: cancellationToken);
        }
        public static async Task IsFileDonloaded(long chatId)
        {
            if (Enterable.isdonloaded)
            {
                Enterable.sample_csv = true;
                Enterable.sort_csv = true;
                Message sentMessage = await botClient.SendTextMessageAsync(
                     chatId: chatId,
                     text: "Продолжим обрабатывать загруженный файл!",
                     replyMarkup: Keyboards.CsvProcessingKeyboard,
                     cancellationToken: cancellationToken);
            }
            else
            {
                Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Я не смогла найти файл(((\n\n" +
                    "Вероятно, вы его ещё не загружали",
                    replyMarkup: Keyboards.FileTypeKeyboard(),
                    cancellationToken: cancellationToken);
            }
        }
        public static async Task InvalidMessage(long chatId)
        {
            Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Я не смогла вас понять(((\n\n" +
                    "Попробуйте ещё раз",
                    replyMarkup: Keyboards.MainMenuKeyboard(),
                    cancellationToken: cancellationToken);
        }
        public static async Task InvalidFileInputPosition(long chatId)
        {
            Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "В данном месте чата отправка файлов запрещена!\n" +
                    "Я верну вас в главное меню",
                    replyMarkup: Keyboards.RemoveKeyboard,
                    cancellationToken: cancellationToken);
            MainMenu(chatId);

        }

        public static async Task InputCSVMessage(long chatId)
        {
            Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Отправьте в чат CSV файл",
                    replyMarkup: Keyboards.MainMenuKeyboard(),
                    cancellationToken: cancellationToken);
        }

        public static async Task InputJsonMessage(long chatId)
        {
            Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Отправьте в чат Json файл",
                    replyMarkup: Keyboards.MainMenuKeyboard(),
                    cancellationToken: cancellationToken);
        }

        public static async Task InputCSV(long chatId)
        {
            bool iscorrect = true;
            var fileId = update.Message.Document.FileId;
            var fileName = update.Message.Document.FileName;
            var fileInfo = await botClient.GetFileAsync(fileId);
            var filePath = fileInfo.FilePath;
            if (Validators.CSVValidator(fileName))
            {
                using (Stream fileStream = new MemoryStream())
                {
                    var file = await botClient.GetInfoAndDownloadFileAsync(fileId, fileStream, cancellationToken);
                    FileProcessing.list = CsvProcessing.Read(fileStream);
                    
                }
            }
            else
            {
                iscorrect = false;
            }
            Enterable.csv = false;
            IsCorrectInputFile(chatId, iscorrect);


        }

        public static async Task InputJson(long chatId)
        {
            bool iscorrect = true;
            var fileId = update.Message.Document.FileId;
            var fileName = update.Message.Document.FileName;
            var fileInfo = await botClient.GetFileAsync(fileId);
            var filePath = fileInfo.FilePath;
            if (Validators.JsonValidator(fileName))
            {
                using (Stream fileStream = new MemoryStream())
                {
                    await botClient.GetInfoAndDownloadFileAsync(fileId, fileStream, cancellationToken);
                    FileProcessing.list = JsonProcessing.Read(fileStream);
                }
            }
            else
            {
                iscorrect = false;
            }
            Enterable.json = false;
            IsCorrectInputFile(chatId, iscorrect);
        }

        public static async Task IsCorrectInputFile(long chatId, bool iscorrect)
        {
            string text = iscorrect ? "Я успешно прочитала файл!" : "Ваш файл не соответствует требованиям!\n" +
                    "Я верну вас в главное меню";
            Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: text,
                    cancellationToken: cancellationToken);
            if (!iscorrect)
            {
                MainMenu(chatId);
            }
            else
            {
                Enterable.isdonloaded = true;
                CsvProcessingHandler(chatId);
            }
        }

        public static async Task CsvProcessingHandler(long chatId)
        {
            Message sentMessage = await botClient.SendTextMessageAsync(
                   chatId: chatId,
                   text: "Можем отфильтровать или отсортировать данные",
                   replyMarkup: Keyboards.CsvProcessingKeyboard,
                   cancellationToken: cancellationToken);
            Enterable.sample_csv = true;
            Enterable.sort_csv = true;
        }

        public static async Task CsvSampleHandler(long chatId)
        {
            if (Enterable.sample_csv)
            {
                Enterable.sort_csv = false;
                string text = "Вы можете произвести выборку по полям:\n" +
                    "• CoverageArea\n" +
                    "• ParkName\n" +
                    "• AdmArea и CoverageArea\n";
                Message sentMessage = await botClient.SendTextMessageAsync(
                       chatId: chatId,
                       text: text,
                       replyMarkup: Keyboards.CsvSampleKeyboard,
                       cancellationToken: cancellationToken);
            }
            else
            {
                Message sentMessage = await botClient.SendTextMessageAsync(
                       chatId: chatId,
                       text: "Сейчас это сделать нельзя!",
                       cancellationToken: cancellationToken);
            }
        }

        public static async Task CsvSortHandler(long chatId)
        {
            if (Enterable.sort_csv)
            {
                Enterable.sample_csv = false;
                string text = "Вы можете произвести сортировку по полям:\n" +
                    "• Name\n" +
                    "• CoverageArea\n";
                Message sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: text,
                        replyMarkup: Keyboards.CsvSortKeyboard,
                        cancellationToken: cancellationToken);
            }
            else
            {
                Message sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Сейчас это сделать нельзя!",
                        cancellationToken: cancellationToken);
            }
        }

        public static async Task InputCoverageArea(long chatId)
        {
            Message sentMessage = await botClient.SendTextMessageAsync(
                       chatId: chatId,
                       text: "Введите значние поля CoverageArea",
                       replyMarkup: Keyboards.MainMenuKeyboard(),
                       cancellationToken: cancellationToken);
            Enterable.csv_coverage_area = true;
        }

        public static async Task InputParkName(long chatId)
        {
            Message sentMessage = await botClient.SendTextMessageAsync(
                       chatId: chatId,
                       text: "Введите значние поля ParkName",
                       replyMarkup: Keyboards.MainMenuKeyboard(),
                       cancellationToken: cancellationToken);
            Enterable.csv_park_name = true;
        }

        public static async Task InputAdmArea(long chatId)
        {
            Message sentMessage = await botClient.SendTextMessageAsync(
                       chatId: chatId,
                       text: "Введите значние полей AdmArea и CoverageArea в одну строку через пробел",
                       replyMarkup: Keyboards.MainMenuKeyboard(),
                       cancellationToken: cancellationToken);
            Enterable.csv_adm_area = true;
        }
        public static async Task ReplyToProcess(long chatId, bool result, bool issample)
        {
            string process_type = issample ? "выборку" : "сортировку";
            if (!result)
            {
                Message sentMessage = await botClient.SendTextMessageAsync(
                       chatId: chatId,
                       text: $"Я не смогла произвести {process_type}! Проверьте входные данные",
                       replyMarkup: Keyboards.MainMenuKeyboard(),
                       cancellationToken: cancellationToken);
                
            }
            else
            {
                process_type = issample ? "выборки" : "сортировки";
                string name = issample ? "Sampled" : "Sorted";
                Stream stream = CsvProcessing.Write(FileProcessing.processed_list);

                Message sentMessage = await botClient.SendDocumentAsync(
                    chatId: chatId,
                    document: InputFile.FromStream(stream, $"{name}File.csv"),
                    caption: $"Результат {process_type} в формате csv");

                stream = JsonProcessing.Write(FileProcessing.processed_list);

                Message sentMessage2 = await botClient.SendDocumentAsync(
                    chatId: chatId,
                    document: InputFile.FromStream(stream, $"{name}File.json"),
                    caption: $"Результат {process_type} в формате json");

                await SendSticker.SendGive(chatId);
            }

            await WhatsNext(chatId);
        }

        public static async Task WhatsNext(long chatId)
        {
            Message sentMessage = await botClient.SendTextMessageAsync(
                       chatId: chatId,
                       text: "Что будем делать дальше?",
                       replyMarkup: Keyboards.FileTypeKeyboard(),
                       cancellationToken: cancellationToken);
            
        }

    }
    
}
