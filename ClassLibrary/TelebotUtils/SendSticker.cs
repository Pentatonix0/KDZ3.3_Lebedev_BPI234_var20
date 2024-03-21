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
    public class SendSticker //Class for sending stickers.
    { 
        public static async Task SendHello(long chatId)
        {
           
            Message message1 = await Telebot.botClient.SendStickerAsync(
                chatId: chatId,
                sticker: InputFile.FromUri("https://raw.githubusercontent.com/Pentatonix0/KDZ3.3_Lebedev_BPI234_var20/master/ClassLibrary/Stickers/Hi.webp"),
                cancellationToken: ReplyUtils.cancellationToken);
                
            
        }

        public static async Task SendHelp(long chatId)
        {

            Message message1 = await Telebot.botClient.SendStickerAsync(
                chatId: chatId,
                sticker: InputFile.FromUri("https://raw.githubusercontent.com/Pentatonix0/KDZ3.3_Lebedev_BPI234_var20/master/ClassLibrary/Stickers/Help.webp"),
                cancellationToken: ReplyUtils.cancellationToken);


        }

        public static async Task SendGive(long chatId)
        {

            Message message1 = await Telebot.botClient.SendStickerAsync(
                chatId: chatId,
                sticker: InputFile.FromUri("https://raw.githubusercontent.com/Pentatonix0/KDZ3.3_Lebedev_BPI234_var20/master/ClassLibrary/Stickers/Give.webp"),
                cancellationToken: ReplyUtils.cancellationToken);


        }
    }
}
