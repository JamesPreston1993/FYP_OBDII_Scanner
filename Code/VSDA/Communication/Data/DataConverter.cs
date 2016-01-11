using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSDA.Communication.Data
{
    public class DataConverter
    {
        public static string ConvertPID(IPid pid, string response)
        {
            string value = "No Value";

            int A, B, C, D;
            double tempVal;
            switch(pid.PidHex)
            {
                // (A - 128) * (100 / 128)                
                case "06":
                case "07":
                case "08":
                case "09":
                case "2D":
                    A = Convert.ToInt32(Convert.ToByte(response.Substring(0, 2), 16));
                    tempVal = (A - 128) * (100 / 128);
                    value = tempVal.ToString();
                    // Do
                    break;

                // (A * 100) / 255
                case "04":
                case "11":
                case "2C":
                case "2E":
                case "2F":
                case "45":
                case "47":
                case "48":
                case "49":
                case "4A":
                case "4B":
                case "4C":
                case "52":
                case "5A":
                case "5B":
                    A = Convert.ToInt32(Convert.ToByte(response.Substring(0, 2), 16));
                    tempVal = (A * 100) / 255;
                    value = tempVal.ToString();
                    // Do
                    break;

                // A - 40
                case "05":
                case "0F":
                case "46":
                case "5C":
                    A = Convert.ToInt32(Convert.ToByte(response.Substring(0, 2), 16));
                    value = (A - 40).ToString();
                    break;

                // A
                case "0B":
                case "0D":
                case "30":
                case "33":
                    A = Convert.ToInt32(Convert.ToByte(response.Substring(0,2), 16));
                    value = A.ToString();
                    break;

                // A * 3
                case "0A":
                    A = Convert.ToInt32(Convert.ToByte(response.Substring(0, 2), 16));
                    tempVal = A * 3;
                    value = A.ToString();
                    break;

                // (A - 128) / 2
                case "0E":
                    break;

                // ((A * 256) + B) / 4
                case "0C":
                case "32":
                    A = Convert.ToInt32(Convert.ToByte(response.Substring(0, 2), 16));
                    B = Convert.ToInt32(Convert.ToByte(response.Substring(2, 2), 16));
                    tempVal = ((A * 256) + B) / 4;
                    value = tempVal.ToString();
                    break;

                // ((A * 256) + B) / 100
                case "10":
                    break;

                // (A * 256) + B
                case "1F":
                case "21":
                case "31":
                case "4D":
                case "4E":
                    break;

                // ((A * 256) + B) * 0.079
                case "22":
                    break;

               //TODO: Other conversions
            }

            return value;
        }
    }
}
