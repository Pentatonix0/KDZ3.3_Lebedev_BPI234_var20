using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.TelebotUtils
{
    public class Texts //A class for representing the texts being sent.
    {
        public static string HelloText = "Я умею обрабатывать данные об WiFi-точках в парках Москвы.\n\n" +
            "Что я могу:\n\n" +
            "• Принимать данные в .csv и .json форматах\n" +
            "• Производить выборку и сортировку по различным полям\n" +
            "• А также возвращать обработанные данные в форматах .csv и .json\n\n" +
            "• Если остались вопросы, то в разделе /help ты можешь поискать на них ответ;)";

        public static string MainMenu = "Добро пожаловать в главное меню!\n\n" +
            "Чем займёмся?";
    }
}
