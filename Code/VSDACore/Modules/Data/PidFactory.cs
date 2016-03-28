using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSDACore.Modules.Data
{
    public class PidFactory
    {
        public static IPid CreatePid(string pidHex)
        {
            string name = "Unsupported pid";
            double minValue = 0;
            double maxValue = 0;

            switch (pidHex)
            {
                // 01 - 1F             
                //case "01": name = "Monitor status since DTCs cleared"; break;
                //case "02": name = "Freeze DTC"; break;
                //case "03": name = "Fuel system status"; break;
                case "04": name = "Calculated engine load value"; minValue = 0; maxValue = 100; break;
                case "05": name = "Engine coolant temperature"; minValue = -40; maxValue = 215; break;
                case "06": name = "Short term fuel % trim—Bank 1"; minValue = -100; maxValue = 99.22; break;
                case "07": name = "Long term fuel % trim—Bank 1"; minValue = -100; maxValue = 99.22; break;
                case "08": name = "Short term fuel % trim—Bank 2"; minValue = -100; maxValue = 99.22; break;
                case "09": name = "Long term fuel % trim—Bank 2"; minValue = -100; maxValue = 99.22; break;
                case "0A": name = "Fuel pressure"; minValue = 0; maxValue = 765; break;
                case "0B": name = "Intake manifold absolute pressure"; minValue = 0; maxValue = 255; break;                
                case "0C": name = "Engine RPM"; minValue = 0; maxValue = 16384; break;
                case "0D": name = "Vehicle speed"; minValue = 0; maxValue = 255; break;
                case "0E": name = "Timing advance"; minValue = -64; maxValue = 63.5; break;
                case "0F": name = "Intake air temperature"; minValue = -40; maxValue = 215; break;
                case "10": name = "MAF air flow rate"; minValue = 0; maxValue = 655.35; break;
                case "11": name = "Throttle position"; minValue = 0; maxValue = 100; break;
                //case "12": name = "Commanded secondary air status"; break;
                //case "13": name = "Oxygen sensors present"; break;                
                case "14": name = "Bank 1, Sensor 1: Oxygen sensor voltage"; minValue = 0; maxValue = 1.275; break;
                case "15": name = "Bank 1, Sensor 2: Oxygen sensor voltage"; minValue = 0; maxValue = 1.275; break;
                case "16": name = "Bank 1, Sensor 3: Oxygen sensor voltage"; minValue = 0; maxValue = 1.275; break;
                case "17": name = "Bank 1, Sensor 4: Oxygen sensor voltage"; minValue = 0; maxValue = 1.275; break;
                case "18": name = "Bank 2, Sensor 1: Oxygen sensor voltage"; minValue = 0; maxValue = 1.275; break;
                case "19": name = "Bank 2, Sensor 2: Oxygen sensor voltage"; minValue = 0; maxValue = 1.275; break;
                case "1A": name = "Bank 2, Sensor 3: Oxygen sensor voltage"; minValue = 0; maxValue = 1.275; break;
                case "1B": name = "Bank 2, Sensor 4: Oxygen sensor voltage"; minValue = 0; maxValue = 1.275; break;
                //case "1C": name = "OBD standards this vehicle conforms to"; break;
                //case "1D": name = "Oxygen sensors present"; break;
                //case "1E": name = "Auxiliary input status"; break;
                case "1F": name = "Run time since engine start"; minValue = 0; maxValue = 65535; break;
                
                // 21 - 3F
                case "21": name = "Distance traveled with malfunction indicator lamp (MIL) on"; minValue = 0; maxValue = 65535; break;
                case "22": name = "Fuel rail Pressure (relative to manifold vacuum)"; minValue = 0; maxValue = 5177.265; break;
                case "23": name = "Fuel rail Pressure (diesel, or gasoline direct inject)"; minValue = 0; maxValue = 655350; break;
                case "24": name = "Oxygen Sensor 1: Voltage"; minValue = 0; maxValue = 8; break;
                case "25": name = "Oxygen Sensor 2: Voltage"; minValue = 0; maxValue = 8; break;
                case "26": name = "Oxygen Sensor 3: Voltage"; minValue = 0; maxValue = 8; break;
                case "27": name = "Oxygen Sensor 4: Voltage"; minValue = 0; maxValue = 8; break;
                case "28": name = "Oxygen Sensor 5: Voltage"; minValue = 0; maxValue = 8; break;
                case "29": name = "Oxygen Sensor 6: Voltage"; minValue = 0; maxValue = 8; break;
                case "2A": name = "Oxygen Sensor 7: Voltage"; minValue = 0; maxValue = 8; break;
                case "2B": name = "Oxygen Sensor 8: Voltage"; minValue = 0; maxValue = 8; break;
                case "2C": name = "Commanded EGR"; minValue = 0; maxValue = 100; break;
                case "2D": name = "EGR Error"; minValue = -100; maxValue = 99.22; break;
                case "2E": name = "Commanded evaporative purge"; minValue = 0; maxValue = 100; break;
                case "2F": name = "Fuel Level Input"; minValue = 0; maxValue = 100; break;
                case "30": name = "# of warm-ups since codes cleared"; minValue = 0; maxValue = 255; break;
                case "31": name = "Distance traveled since codes cleared"; minValue = 0; maxValue = 65535; break;
                case "32": name = "Evap. System Vapor Pressure"; minValue = -8192; maxValue = 8192; break;
                case "33": name = "Barometric pressure"; minValue = 0; maxValue = 255; break;
                case "34": name = "Oxygen Sensor 1: Current"; minValue = -128; maxValue = 128; break;
                case "35": name = "Oxygen Sensor 2: Current"; minValue = -128; maxValue = 128; break;
                case "36": name = "Oxygen Sensor 3: Current"; minValue = -128; maxValue = 128; break;
                case "37": name = "Oxygen Sensor 4: Current"; minValue = -128; maxValue = 128; break;
                case "38": name = "Oxygen Sensor 5: Current"; minValue = -128; maxValue = 128; break;
                case "39": name = "Oxygen Sensor 6: Current"; minValue = -128; maxValue = 128; break;
                case "3A": name = "Oxygen Sensor 7: Current"; minValue = -128; maxValue = 128; break;
                case "3B": name = "Oxygen Sensor 8: Current"; minValue = -128; maxValue = 128; break;                
                case "3C": name = "Catalyst Temperature Bank 1, Sensor 1"; minValue = -40; maxValue = 6513.5; break;
                case "3D": name = "Catalyst Temperature Bank 2, Sensor 1"; minValue = -40; maxValue = 6513.5; break;
                case "3E": name = "Catalyst Temperature Bank 1, Sensor 2"; minValue = -40; maxValue = 6513.5; break;
                case "3F": name = "Catalyst Temperature Bank 2, Sensor 2"; minValue = -40; maxValue = 6513.5; break;
                default: return null;
                
                /*
                case "41": name = "Monitor status this drive cycle"; break;
                case "42": name = "Control module voltage"; break;
                case "43": name = "Absolute load value"; break;
                case "44": name = "Fuel/Air commanded equivalence ratio"; break;
                case "45": name = "Relative throttle position"; break;
                case "46": name = "Ambient air temperature"; break;
                case "47": name = "Absolute throttle position B"; break;
                case "48": name = "Absolute throttle position C"; break;
                case "49": name = "Accelerator pedal position D"; break;
                case "4A": name = "Accelerator pedal position E"; break;
                case "4B": name = "Accelerator pedal position F"; break;
                case "4C": name = "Commanded throttle actuator"; break;
                case "4D": name = "Time run with MIL on"; break;
                case "4E": name = "Time since trouble codes cleared"; break;
                // Problem Area Start
                case "4F": name = "Maximum value for equivalence ratio, oxygen sensor voltage, oxygen sensor current, and intake manifold absolute pressure"; break;
                // Problem Area End
                case "50": name = "Maximum value for air flow rate from mass air flow sensor"; break;
                case "51": name = "Fuel Type"; break;
                case "52": name = "Ethanol fuel %"; break;
                case "53": name = "Absolute Evap system Vapor Pressure"; break;
                case "54": name = "Evap system vapor pressure"; break;
                case "55": name = "Short term secondary oxygen sensor trim bank 1 and bank 3"; break;
                case "56": name = "Long term secondary oxygen sensor trim bank 1 and bank 3"; break;
                case "57": name = "Short term secondary oxygen sensor trim bank 2 and bank 4"; break;
                case "58": name = "Long term secondary oxygen sensor trim bank 2 and bank 4"; break;
                case "59": name = "Fuel rail pressure (absolute)"; break;
                case "5A": name = "Relative accelerator pedal position"; break;
                case "5B": name = "Hybrid battery pack remaining life"; break;
                case "5C": name = "Engine oil temperature"; break;
                case "5D": name = "Fuel injection timing"; break;
                case "5E": name = "Engine fuel rate"; break;
                case "5F": name = "Emission requirements to which vehicle is designed"; break;
                */
            }
            
            IPid pid = new Pid(pidHex, name, minValue, maxValue);
            return pid;
        }
    }
}
