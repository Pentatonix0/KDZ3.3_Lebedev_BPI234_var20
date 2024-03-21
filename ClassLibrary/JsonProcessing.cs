using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class JsonProcessing
    {
        public static string WifiParkToJson(WifiPark[] wifiparks) // Method to convert SpespecifiedProduct to json.
        {
            var options = new JsonSerializerOptions {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true };
            string json = JsonSerializer.Serialize(wifiparks, options);
            return json;
        }
        public static WifiPark[] JsonToWifiPark(string json) // Method to convert SpespecifiedProduct to json.
        {
            WifiPark[] wifipark = JsonSerializer.Deserialize<WifiPark[]>(json);
            return wifipark;
        }

        public static string ReadJson(string path) // Method for reading data.
        {
            using StreamReader file = new StreamReader(path);
            string json = file.ReadToEnd();
            return json;
        }

        public static void WriteJson(string path, string json) // A method for filtering data.
        {
            File.WriteAllText(path, json);
        }

        public static List<WifiPark> Read(Stream stream)
        {
            stream.Position = 0;
            using (var sr = new StreamReader(stream))
            {
                var jsonString = sr.ReadToEnd();
                var records = JsonSerializer.Deserialize<List<WifiPark>>(jsonString);

                return records;
            }
        }


        public static Stream Write(List<WifiPark> items)
        {
            var options = new JsonSerializerOptions
            {
                // Отступы для удобного чтения файла.
                WriteIndented = true,
            };

            options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

            var jsonString = JsonSerializer.Serialize(items, options);
            var byteArray = Encoding.UTF8.GetBytes(jsonString);
            var stream = new MemoryStream(byteArray);

            return stream;
        }
    }
}
