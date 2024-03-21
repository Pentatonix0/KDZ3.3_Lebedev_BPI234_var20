using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace ClassLibrary.TelebotUtils
{
    public class Keyboards
    {

        public static ReplyKeyboardRemove RemoveKeyboard = new ReplyKeyboardRemove();
        public static ReplyKeyboardMarkup CsvProcessingKeyboard = new(new[]
                {
                    new KeyboardButton[] { "Выборка", "Сортировка", "Главное меню" },
                })
        {
            ResizeKeyboard = true
        };

        public static ReplyKeyboardMarkup CsvSampleKeyboard = new(new[]
                {
                    new KeyboardButton[] { "CoverageArea", "ParkName", "AdmArea и CoverageArea", "Главное меню" },
                })
        {
            ResizeKeyboard = true
        };
        public static ReplyKeyboardMarkup MainMenuKeyboard()
        {
            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                {
                    new KeyboardButton[] { "Главное меню" },
                })
            {
                ResizeKeyboard = true
            };
        
            return replyKeyboardMarkup;
        }

        public static ReplyKeyboardMarkup FileTypeKeyboard()
        {
            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                {
                    new KeyboardButton[] { "Добавить CSV файл", "Добавить Json файл"  },
                })
            {
                ResizeKeyboard = true
            };

            return replyKeyboardMarkup;
        }
    }
}
