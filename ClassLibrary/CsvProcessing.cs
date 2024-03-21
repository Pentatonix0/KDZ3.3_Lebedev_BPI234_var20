using System.Formats.Asn1;
using System.Globalization;
using System.Text;
using CsvHelper;

namespace ClassLibrary
{
    public class CsvProcessing
    {
        public static string Head_EN = "\"ID\";\"global_id\";\"Name\";\"AdmArea\";\"District\";\"ParkName\";\"WiFiName\";\"CoverageArea\";\"FunctionFlag\";\"AccessFlag\";\"Password\";\"Longitude_WGS84\";\"Latitude_WGS84\";\"geodata_center\";\"geoarea\";";
        public static string Head_RU = "\"Код\";\"global_id\";\"Наименование\";\"Административный округ по адресу\";\"Район\";\"Наименование парка\";\"Имя Wi-Fi сети\";\"Зона покрытия (метры)\";\"Признак функционирования\";\"Условия доступа\";\"Пароль\";\"Долгота в WGS-84\";\"Широта в WGS-84\";\"geodata_center\";\"geoarea\";";
        public static string[] Head = { };
        public static List<WifiPark> Read(Stream stream)     // Method for reading the file.
        {
            
            stream.Position = 0;
            var list = new List<WifiPark>();
            try
            {
                    using (var reader = new StreamReader(stream))
                {
                    var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        Delimiter = ";"
                    };
                    using (var csv = new CsvReader(reader, config))

                    {
                        csv.Read();
                        csv.ReadHeader();
                        csv.Read();

                        string[] heads =
                        {
                        csv.GetField("ID") ?? "",
                        csv.GetField("global_id") ?? "",
                        csv.GetField("Name") ?? "",
                        csv.GetField("AdmArea") ?? "",
                        csv.GetField("District") ?? "",
                        csv.GetField("ParkName") ?? "",
                        csv.GetField("WiFiName") ?? "",
                        csv.GetField("CoverageArea") ?? "",
                        csv.GetField("FunctionFlag") ?? "",
                        csv.GetField("AccessFlag") ?? "",
                        csv.GetField("Password")?? "",
                        csv.GetField("Longitude_WGS84")?? "",
                        csv.GetField("Latitude_WGS84")?? "",
                        csv.GetField("geodata_center") ?? "",
                        csv.GetField("geoarea") ?? ""
                    };

                        Head = heads;

                        while (csv.Read())
                        {
                            var record = new WifiPark(
                                csv.GetField<int>("ID"),
                                csv.GetField<long>("global_id"),
                                csv.GetField("Name") ?? "",
                                csv.GetField("AdmArea") ?? "",
                                csv.GetField("District") ?? "",
                                csv.GetField("ParkName") ?? "",
                                csv.GetField("WiFiName") ?? "",
                                csv.GetField<int>("CoverageArea"),
                                csv.GetField("FunctionFlag") ?? "",
                                csv.GetField("AccessFlag") ?? "",
                                csv.GetField("Password") ?? "",
                                csv.GetField<double>("Longitude_WGS84"),
                                csv.GetField<double>("Latitude_WGS84"),
                                csv.GetField("geodata_center") ?? "",
                                csv.GetField("geoarea") ?? "");

                            list.Add(record);
                        }
                    }
                }
                return list;
            }
            catch(Exception e) 
            {
    
                
            }
            return list;
        }

        public static Stream Write(List<WifiPark> centers) // Method for writing an array of strings.
        {

            Stream memstream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memstream);

            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            };

            CsvWriter csvWriter = new CsvWriter(streamWriter, config);

            csvWriter.WriteHeader<WifiPark>();
            csvWriter.NextRecord();
            csvWriter.WriteField(Head);
            csvWriter.NextRecord();

            foreach (var center in centers)
            {
                csvWriter.WriteRecord(center);
                csvWriter.NextRecord();
            }

            streamWriter.Flush();
            memstream.Position = 0;

            return memstream;
        }
    }
}


    
