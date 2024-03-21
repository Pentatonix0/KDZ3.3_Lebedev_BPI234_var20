using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.TelebotUtils
{
    internal class Validators
    {
        public static bool CSVValidator(string filename)
        {
            int start = filename.Length - 4;
            int length = 4;
            string h = filename.Substring(start, length);
            return h == ".csv";
        }

        public static bool JsonValidator(string filename)
        {
            int start = filename.Length - 5;
            int length = 5;
            string h = filename.Substring(start, length);
            return h == ".json";
        }
    }
}
