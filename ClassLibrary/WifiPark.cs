using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public  class WifiPark
    {
        public int ID { get; set; }
        public long global_id { get; set; }
        public string Name { get; set; }
        public string AdmArea { get; set; }
        public string District { get; set; }
        public string ParkName { get; set; }
        public string WiFiName { get; set; }
        public int CoverageArea { get; set; }
        public string FunctionFlag { get; set; }
        public string AccessFlag { get; set; }
        public string Password { get; set; }
        public double Longitude_WGS84 { get; set; }
        public double Latitude_WGS84 { get; set; }
        public string geodata_center { get; set; }
        public string geoarea { get; set; }

        public WifiPark(int iD,long global_id, string name, string admArea, string district, string parkname, string wiFiName, int coverageArea, string functionFlag, string accessFlag, string password, double longitude_WGS84, double latitude_WGS84, string geodata_center, string geoarea)
        {
            ID = iD;
            this.global_id = global_id;
            Name = name;
            AdmArea = admArea;
            District = district;
            ParkName = parkname.Replace(',', '.');
            WiFiName = wiFiName;
            CoverageArea = coverageArea;
            FunctionFlag = functionFlag;
            AccessFlag = accessFlag;
            Password = password;
            Longitude_WGS84 = longitude_WGS84;
            Latitude_WGS84 = latitude_WGS84;
            this.geodata_center = geodata_center;
            this.geoarea = geoarea;
        }

        public WifiPark() { }


        public override string ToString()
        {
            return $"{ID.ToString()};{global_id.ToString()};{Name};{AdmArea};{District};{ParkName};{WiFiName};{CoverageArea.ToString()};{FunctionFlag};{AccessFlag};\"{Password}\";{Longitude_WGS84.ToString()};{Latitude_WGS84.ToString()};\"{this.geodata_center}\";\"{this.geoarea}\";";
        }

    }
}
