using System.Text;

namespace ClassLibrary
{
    public class CsvProcessing
    {
        public static string fPath;

        public static string[] Read() // Method for reading the file.
        {
            try
            {
                string[] rowdata = null;
                rowdata = File.ReadAllLines(fPath);
                return rowdata;
            }
            catch
            {
                throw new ArgumentNullException(fPath, "Wrong Path");
            }

        }

        public static string[][] ArrayBuilder(string[] array) // Method for creating a jagged array.
        {
            string[][] data = new string[array.Length][];
            for (int i = 0; i < array.Length; i++)
            {
                data[i] = array[i].Split(';');
                for (int j = 0; j < data[i].Length; j++)
                {
                    data[i][j] = data[i][j].Replace("\"", "");
                }

            }

            return data;
        }

        public static Dictionary<string, int> MakeHeadDict(string[][] data) // Method for creating a dictionary from the header.
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            string[] head = data[0];
            for (int i = 0; i < head.Length; i++)
            {
                dict.Add(head[i].Replace("\"", ""), i);
            }

            return dict;
        }

        public static string[] SampleByArea(string[] data_user, string area, string area_name) // Sampling by value CoverageArea or ParkName.
        {
            string[][] data = CsvProcessing.ArrayBuilder(data_user);
            Dictionary<string, int> dict = MakeHeadDict(data);
            string[] new_data = new string[2];
            new_data[0] = String.Join(';', data[0]);
            new_data[1] = String.Join(';', data[1]);
            foreach (string[] row in data)
            {

                if (row[dict[area_name]] == area.Replace('<', '«').Replace('>', '»'))
                {
                    Array.Resize(ref new_data, new_data.Length + 1);
                    new_data[new_data.Length - 1] = String.Join(";", row);
                }
            }

            return new_data;


        }

        public static string[] SampleByAreas(string[] data_user, string admarea, int coverage_area) // Sampling by value AdmArea and ParkName.
        {
            string[][] data = CsvProcessing.ArrayBuilder(data_user);
            Dictionary<string, int> dict = MakeHeadDict(data);
            string[] new_data = new string[2];
            new_data[0] = String.Join(';', data[0]);
            new_data[1] = String.Join(';', data[1]);
            foreach (string[] row in data)
            {

                if (row[dict["AdmArea"]] == admarea && row[dict["CoverageArea"]] == coverage_area.ToString())
                {
                    Array.Resize(ref new_data, new_data.Length + 1);
                    new_data[new_data.Length - 1] = String.Join(";", row);
                }
            }
            return new_data;
        }
        public static void OutputData(string[] data) // Method for displaying an array on the screen.
        {
            for (int i = 2; i < data.Length; i++)
            {
                Console.WriteLine(data[i].Replace(";;", ";").Replace(";;",";"));
                Console.WriteLine();

            }
            Console.WriteLine();
        }

        public static bool Write(string[] data, string fPath) // Method for writing an array of strings.
        {
            try
            {
                var csv = new StringBuilder();
                foreach (string row in data)
                {
                    csv.AppendLine(row.Replace(',', '.'));

                }
                File.WriteAllText(fPath, csv.ToString());
                Console.WriteLine();
                Console.WriteLine("Запись прошла успешно");
                return true;
            }
            catch
            {
                Console.WriteLine();
                Console.WriteLine($"Failed to write {fPath}\nPlease check your Input Data");
                Console.WriteLine();
                return false;

            }
        }

        public static string[] SortByName(string[] data) // Method for sorting by Name.
        {
            string[][] array = CsvProcessing.ArrayBuilder(data);
            Dictionary<string, int> dict = MakeHeadDict(array);
            int indx = dict["Name"];
            for (int i = 2; i < array.Length; i++)
            {
                for (int j = 2; j < array.Length - 1; j++)
                {
                    if (String.Compare(array[j][indx], array[j + 1][indx]) > 0)
                    {
                        string[] temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
            string[] new_data = new string[2];
            new_data[0] = String.Join(';', array[0]);
            new_data[1] = String.Join(';', array[1]);
            for (int i = 2; i < array.GetLength(0); i++)
            {
                
                Array.Resize(ref new_data, new_data.Length + 1);
                new_data[i] = String.Join(";", array[i]);
            }
            return new_data;
        }
        public static string[] SortByCoverageArea(string[] data) // Method for sorting by CoverageArea.
        {
            string[][] array = CsvProcessing.ArrayBuilder(data);
            Dictionary<string, int> dict = MakeHeadDict(array);
            int indx = dict["CoverageArea"];
            for (int i = 2; i < array.Length; i++)
            {
                for (int j = 2; j < array.Length - 1; j++)
                {
                    int a = int.Parse(array[j][indx]);
                    int b = int.Parse(array[j + 1][indx]);
                   
                    if (a > b)
                    {
                        string[] temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
            string[] new_data = new string[2];
            new_data[0] = String.Join(';', array[0]);
            new_data[1] = String.Join(';', array[1]);
            for (int i = 2; i < array.GetLength(0); i++)
            {
                Array.Resize(ref new_data, new_data.Length + 1);
                new_data[i] = String.Join(";", array[i]);
            }
            return new_data;
        }

    }
}