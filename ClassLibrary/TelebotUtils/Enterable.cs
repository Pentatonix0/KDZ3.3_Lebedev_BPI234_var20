using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.TelebotUtils
{
    public class Enterable
    {
        public static bool csv = false;
        public static bool json = false;
        public static bool sample_csv = false;
        public static bool sort_csv = false;
        public static bool csv_coverage_area= false;
        public static bool csv_park_name = false;
        public static bool csv_adm_area = false;
        public static string[] HighPriority = new string[] { "/start", "/help", "главное меню" };

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
