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


        public static Stream Write(List<WifiPark> list)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

            var jsonString = JsonSerializer.Serialize(list, options);
            var byteArray = Encoding.UTF8.GetBytes(jsonString);
            var stream = new MemoryStream(byteArray);

            return stream;
        }
    }
}
