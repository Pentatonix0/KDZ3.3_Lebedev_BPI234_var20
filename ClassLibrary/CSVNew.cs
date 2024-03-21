using System.Formats.Asn1;
using System.Globalization;
using System.Text;
using CsvHelper;

namespace ClassLibrary
{
    public class CSVNew
    {
        public static string Head_EN = "\"ID\";\"global_id\";\"Name\";\"AdmArea\";\"District\";\"ParkName\";\"WiFiName\";\"CoverageArea\";\"FunctionFlag\";\"AccessFlag\";\"Password\";\"Longitude_WGS84\";\"Latitude_WGS84\";\"geodata_center\";\"geoarea\";";
        public static string Head_RU = "\"Код\";\"global_id\";\"Наименование\";\"Административный округ по адресу\";\"Район\";\"Наименование парка\";\"Имя Wi-Fi сети\";\"Зона покрытия (метры)\";\"Признак функционирования\";\"Условия доступа\";\"Пароль\";\"Долгота в WGS-84\";\"Широта в WGS-84\";\"geodata_center\";\"geoarea\";";
        public static string[] Titles = { };
        public static List<WifiPark> Read(Stream stream)     // Method for reading the file.
        {
            
            stream.Position = 0;
            var records = new List<WifiPark>();
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

                        string[] titles =
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

                        Titles = titles;

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

                            records.Add(record);
                        }
                    }
                }
                return records;
            }
            catch(Exception e) 
            {
                Console.WriteLine(e.Message);
                
            }
            return records;


            
        }




        public static WifiPark[] SampleByCoverageArea(WifiPark[] data_user, string area) // Sampling by value CoverageArea.
        {
            WifiPark[] new_data = new WifiPark[0];
            foreach (WifiPark wp in data_user)
            {
                if (wp.CoverageArea.ToString() == area)
                {
                    Array.Resize(ref new_data, new_data.Length + 1);
                    new_data[new_data.Length - 1] = wp;
                }

            }
            return new_data;


        }

        public static WifiPark[] SampleByParkname(WifiPark[] data_user, string area, string area_name) // Sampling by value ParkName.
        {
            WifiPark[] new_data = new WifiPark[0];
            foreach (WifiPark wp in data_user)
            {
                if (wp.ParkName.ToString() == area)
                {
                    Array.Resize(ref new_data, new_data.Length + 1);
                    new_data[new_data.Length - 1] = wp;
                }

            }
            return new_data;


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
            csvWriter.WriteField(Titles);
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




        //public static string[] SortByName(string[] data) // Method for sorting by Name.
        //{
        //    string[][] array = CSVNew.ArrayBuilder(data);
        //    Dictionary<string, int> dict = MakeHeadDict(array);
        //    int indx = dict["Name"];
        //    for (int i = 2; i < array.Length; i++)
        //    {
        //        for (int j = 2; j < array.Length - 1; j++)
        //        {
        //            if (String.Compare(array[j][indx], array[j + 1][indx]) > 0)
        //            {
        //                string[] temp = array[j];
        //                array[j] = array[j + 1];
        //                array[j + 1] = temp;
        //            }
        //        }
        //    }
        //    string[] new_data = new string[2];
        //    new_data[0] = String.Join(';', array[0]);
        //    new_data[1] = String.Join(';', array[1]);
        //    for (int i = 2; i < array.GetLength(0); i++)
        //    {

        //        Array.Resize(ref new_data, new_data.Length + 1);
        //        new_data[i] = String.Join(";", array[i]);
        //    }
        //    return new_data;
        //}
        //public static string[] SortByCoverageArea(string[] data) // Method for sorting by CoverageArea.
        //{
        //    string[][] array = CSVNew.ArrayBuilder(data);
        //    Dictionary<string, int> dict = MakeHeadDict(array);
        //    int indx = dict["CoverageArea"];
        //    for (int i = 2; i < array.Length; i++)
        //    {
        //        for (int j = 2; j < array.Length - 1; j++)
        //        {
        //            int a = int.Parse(array[j][indx]);
        //            int b = int.Parse(array[j + 1][indx]);

        //            if (a > b)
        //            {
        //                string[] temp = array[j];
        //                array[j] = array[j + 1];
        //                array[j + 1] = temp;
        //            }
        //        }
        //    }
        //    string[] new_data = new string[2];
        //    new_data[0] = String.Join(';', array[0]);
        //    new_data[1] = String.Join(';', array[1]);
        //    for (int i = 2; i < array.GetLength(0); i++)
        //    {
        //        Array.Resize(ref new_data, new_data.Length + 1);
        //        new_data[i] = String.Join(";", array[i]);
        //    }
        //    return new_data;
        //}

    
