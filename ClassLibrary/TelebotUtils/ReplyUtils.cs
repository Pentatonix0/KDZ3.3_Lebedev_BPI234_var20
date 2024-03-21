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
    public class ReplyUtils
    {
        public static ITelegramBotClient botClient;
        static Update update;
        public static CancellationToken cancellationToken;
        public static async Task ReplyToMessageHandler(ITelegramBotClient BotClient, Update Update, CancellationToken CancellationToken)
        {
            var message = Update.Message;
            var text = message.Text;
            var document = message.Document;
            long chatId = message.Chat.Id;
            botClient = BotClient;
            update = Update;
            cancellationToken = CancellationToken;
            Console.WriteLine(Enterable.EnterField());
            if (text != null && Enterable.HighPriority.Contains(text.ToLower()))
            {
                Enterable.ToDefault();
                switch (text.ToLower())
                {
                    case "/start":
                        SendSticker.SendHello(chatId);
                        SendStart(chatId); break;
                    case "главное меню":
                        SendStart(chatId); break;
                    case "/help":
                        SendHelh(chatId); break;
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
                    case "выборка":
                        CsvSampleHandler(chatId); break;
                    case "coveragearea":
                        InputCoverageArea(chatId); break;
                    case "parkname":
                        InputParkName(chatId); break;
                    case "admarea и coveragearea":
                        InputAdmArea(chatId); break;
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
                    text: "Вас приветствует бот для обработки CSV (и Json) файлов!",
                    replyMarkup: Keyboards.FileTypeKeyboard(),
                    cancellationToken: cancellationToken);
        }

        public static async Task SendHelh(long chatId)
        {
            Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Помощь!",
                    replyMarkup: Keyboards.MainMenuKeyboard(),
                    cancellationToken: cancellationToken);
        }

        public static async Task InvalidMessage(long chatId)
        {
            Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Сообщение не распознано, повторите ввод",
                    replyMarkup: Keyboards.MainMenuKeyboard(),
                    cancellationToken: cancellationToken);
        }
        public static async Task InvalidFileInputPosition(long chatId)
        {
            Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "В данном месте чата отправка файлов запрещена!\n" +
                    "Вы перенаправлены в главное меню",
                    replyMarkup: Keyboards.RemoveKeyboard,
                    cancellationToken: cancellationToken);
            SendStart(chatId);

        }

        public static async Task InputCSVMessage(long chatId)
        {
            Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Отправьте в чат CSV файл.\n" +
                    "Текст в сообщении с файлом будет проигнорирован.",
                    replyMarkup: Keyboards.MainMenuKeyboard(),
                    cancellationToken: cancellationToken);
        }

        public static async Task InputJsonMessage(long chatId)
        {
            Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Отправьте в чат Json файл.\n" +
                    "Текст в сообщении с файлом будет проигнорирован.",
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
                    //FileProcessing.File = fileStream;
                    Console.WriteLine(10);
                    FileProcessing.list = CSVNew.Read(fileStream);
                    
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
            string text = iscorrect ? "Файл успешно прочитан" : "Отправленный файл не соответствует требованиям!\n" +
                    "Вы переправлены в главное меню";
            Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: text,
                    cancellationToken: cancellationToken);
            if (!iscorrect)
            {
                SendStart(chatId);
            }
            else
            {
                //FileProcessing.JsonToCsv();+++++++++++++++++++++
                CsvProcessingHandler(chatId);
            }
        }

        public static async Task CsvProcessingHandler(long chatId)
        {
            Message sentMessage = await botClient.SendTextMessageAsync(
                   chatId: chatId,
                   text: "Вы можете отфильтровать файл или произвести выборку",
                   replyMarkup: Keyboards.CsvProcessingKeyboard,
                   cancellationToken: cancellationToken);
            Enterable.sample_csv = true;
            Enterable.sort_csv = true;
        }

        public static async Task CsvSampleHandler(long chatId)
        {
            if (Enterable.sample_csv && Enterable.sort_csv)
            {
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
                       text: "В данном месте диалога операция недоступна!",
                       cancellationToken: cancellationToken);
            }

        }
        public static async Task CsvSortHandler(long chatId)
        {

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
                       text: $"Не удалось произвести {process_type}! Проверьте входные данные",
                       replyMarkup: Keyboards.MainMenuKeyboard(),
                       cancellationToken: cancellationToken);
                Enterable.csv_coverage_area = true;
            }
            else
            {
                process_type = issample ? "выборки" : "сортировки";
                Stream stream = CSVNew.Write(FileProcessing.processed_list);

                Message sentMessage = await botClient.SendDocumentAsync(
                    chatId: chatId,
                    document: InputFile.FromStream(stream, "SampledFile.csv"),
                    caption: $"Результат {process_type} в формате csv");

                stream = JsonProcessing.Write(FileProcessing.processed_list);

                Message sentMessage2 = await botClient.SendDocumentAsync(
                    chatId: chatId,
                    document: InputFile.FromStream(stream, "SampledFile.json"),
                    caption: $"Результат {process_type} в формате json");
            }
        }

    }
    
}
