using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.TelebotUtils
{
    public class Enterable
    {
        public static bool csv = false; //Flags for data entry, selections, and sorting.
        public static bool json = false; //Flags for data entry, selections, and sorting.
        public static bool sample_csv = false; //Flags for data entry, selections, and sorting.
        public static bool sort_csv = false; //Flags for data entry, selections, and sorting.
        public static bool csv_coverage_area= false; //Flags for data entry, selections, and sorting.
        public static bool csv_park_name = false; //Flags for data entry, selections, and sorting.
        public static bool csv_adm_area = false; //Flags for data entry, selections, and sorting.
        public static bool isdonloaded = false; //Flags for data entry, selections, and sorting.
        public static string[] HighPriority = new string[] { "/start", "/help", "главное меню" }; //Priority of calls.

        public static bool EnterField()
        {
            return csv_adm_area || csv_park_name || csv_coverage_area;
        }

        public static void ToDefault()
        {
            csv = false;
            json = false;
            sample_csv = false;
            sort_csv = false;
            csv_coverage_area = false;
            csv_park_name = false;
            csv_adm_area = false;
        }   
    }
}
