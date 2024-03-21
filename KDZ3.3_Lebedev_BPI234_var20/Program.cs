using System;
using ClassLibrary;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using System.IO;
using ClassLibrary.TelebotUtils;

namespace project
{
    class Program
    {
        public static void Main(string[] args)
        {
            string filename = "E:\\C#\\seminars\\homeworks\\KDZ2\\wifi-parks.csv";
            string filename2 = "E:\\C#\\seminars\\homeworks\\KDZ2\\new.csv";
            string filename3 = "E:\\C#\\seminars\\homeworks\\KDZ2\\new.json";
            Telebot.Start();
            //WifiPark[] arr = CSVNew.Read(filename);
            ////Console.WriteLine(1);
            //////foreach (WifiPark wifiPark in arr)
            //////{
            //////    Console.WriteLine(wifiPark.Parkname);
            //////}

            ////WifiPark[] samp = CSVNew.SampleByCoverageArea(arr, "20");
            ////CSVNew.Write(samp, filename2);

            //string json = JsonProcessing.WifiParkToJson(arr);
            //System.IO.File.WriteAllText(filename3, json);

            //string json = JsonProcessing.ReadJson(filename3);
            //FileProcessing.JsonToCsv(json);

            //string path = "E:\\C#\\KDZ 3-3\\KDZ3.3_Lebedev_BPI234_var20_vol2\\KDZ3.3_Lebedev_BPI234_var20\\bin\\Debug\\net6.0\\file.json";
            ////string path2 = "E:\\C#\\KDZ 3-3\\file.json";
            //string json = JsonProcessing.ReadJson(path);
            //FileProcessing.JsonToCsv();
            //Console.WriteLine(json);


        }
    }
}