using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;


namespace ClassLibrary.TelebotUtils
{
    public class SendStiker
    { 
        public static async Task SendHello(long chatId)
        {
            try
            {
                Message message1 = await Telebot.botClient.SendStickerAsync(
                    chatId: chatId,
                    sticker: InputFile.FromUri("https://raw.githubusercontent.com/Pentatonix0/KDZ3.3_Lebedev_BPI234_var20/master/ClassLibrary/Stikers/Help.webp"),
                    cancellationToken: ReplyUtils.cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
