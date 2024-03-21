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
    public class SendSticker
    { 
        public static async Task SendHello(long chatId)
        {
            try
            {
                //Message message1 = await Telebot.botClient.SendAnimationAsync(
                //    chatId: chatId,
                //    animation: InputFile.FromUri("https://raw.githubusercontent.com/Pentatonix0/KDZ3.3_Lebedev_BPI234_var20/master/ClassLibrary/Stikers/Hi.webp"),
                //    cancellationToken: ReplyUtils.cancellationToken);

                FileStream fs = System.IO.File.Open("Hi.tgs", FileMode.Open);
                Message message1 = await Telebot.botClient.SendStickerAsync(
                    chatId: chatId,
                    sticker: InputFile.FromStream(fs),
                    cancellationToken: ReplyUtils.cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
