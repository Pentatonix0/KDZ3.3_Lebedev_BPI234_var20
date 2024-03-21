using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ClassLibrary.TelebotUtils
{
    public class FileProcessing
    {
        public static List<WifiPark> list; //Input data.
        public static List<WifiPark> processed_list; //Output data.
        public static bool CsvSampleByCoverageArea(string area_value) //Selection.
        {
            try
            {
                processed_list = list.Where(x => x.CoverageArea == Int32.Parse(area_value)).ToList();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public static bool CsvSampleByParkName(string area_value) //Selection.
        {
            try
            {
                processed_list = list.Where(x => x.ParkName == area_value.Replace(',', '.')).ToList();
                Console.WriteLine(area_value);
                Console.WriteLine(list[0].ParkName);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public static bool CsvSampleByAdmArea(string area_value) //Selection.
        {
            try
            {
                string[] values = area_value.Split();
                string adm_area = values[0];
                string cover_area = values[1];
                processed_list = list.Where(x => x.CoverageArea == Int32.Parse(cover_area) && x.AdmArea == adm_area).ToList();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public static bool SortByName() //Sorting.
        {
            try
            {
                processed_list = list.OrderBy(x => x.Name).ToList();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public static bool SortByCoverageArea() //Sorting.
        {
            try
            {
                processed_list = list.OrderBy(x => x.CoverageArea).ToList();
                return true;
            }
            catch
            {
                return false;
            }

        }



    }
}
