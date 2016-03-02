using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSDACore.Modules.Codes
{
    public static class CodeFactory
    {
        public static ICode CreateCode(string codeName)
        {
            string description = GetCodeDescription(codeName);

            if(!description.Equals(string.Empty))
            {
                return new Code(codeName, description);
            }

            return new Code(codeName);
        }

        private static string GetCodeDescription(string codeName)
        {
            string description = string.Empty;

            switch(codeName)
            {
                case "P0100": description = "Mass or Volume Air Flow Circuit Malfunction"; break;
                case "P0101": description = "Mass or Volume Air Flow Circuit Range/Performance Problem"; break;
                case "P0102": description = "Mass or Volume Air Flow Circuit Low Input"; break;
                case "P0103": description = "Mass or Volume Air Flow Circuit High Input"; break;
                case "P0104": description = "Mass or Volume Air Flow Circuit Intermittent"; break;
                case "P0105": description = "Manifold Absolute Pressure/Barometric Pressure Circuit Malfunction"; break;
                case "P0106": description = "Manifold Absolute Pressure/Barometric Pressure Circuit Range/Performance Problem"; break;
                case "P0107": description = "Manifold Absolute Pressure/Barometric Pressure Circuit Low Input"; break;
                case "P0108": description = "Manifold Absolute Pressure/Barometric Pressure Circuit High Input"; break;
                case "P0109": description = "Manifold Absolute Pressure/Barometric Pressure Circuit Intermittent"; break;
                case "P0110": description = "Intake Air Temperature Circuit Malfunction"; break;
                case "P0111": description = "Intake Air Temperature Circuit Range/Performance Problem"; break;
                case "P0112": description = "Intake Air Temperature Circuit Low Input"; break;
                case "P0113": description = "Intake Air Temperature Circuit High Input"; break;
                case "P0114": description = "Intake Air Temperature Circuit Intermittent"; break;
                case "P0115": description = "Engine Coolant Temperature Circuit Malfunction"; break;
                case "P0116": description = "Engine Coolant Temperature Circuit Range/Performance Problem"; break;
                case "P0117": description = "Engine Coolant Temperature Circuit Low Input"; break;
                case "P0118": description = "Engine Coolant Temperature Circuit High Input"; break;
                case "P0119": description = "Engine Coolant Temperature Circuit Intermittent"; break;
                case "P0120": description = "Throttle/Petal Position Sensor/Switch A Circuit Malfunction"; break;
                case "P0121": description = "Throttle/Petal Position Sensor/Switch A Circuit Range/Performance Problem"; break;
                case "P0122": description = "Throttle/Petal Position Sensor/Switch A Circuit Low Input"; break;
                case "P0123": description = "Throttle/Petal Position Sensor/Switch A Circuit High Input"; break;
                case "P0124": description = "Throttle/Petal Position Sensor/Switch A Circuit Intermittent"; break;
                case "P0125": description = "Insufficient Coolant Temperature for Closed Loop Fuel Control"; break;
                case "P0126": description = "Insufficient Coolant Temperature for Stable Operation"; break;
                case "P0130": description = "Oxygen Sensor Circuit Malfunction (Bank 1 Sensor 1)"; break;
                case "P0131": description = "Oxygen Sensor Circuit Low Voltage (Bank 1 Sensor 1)"; break;
                case "P0132": description = "Oxygen Sensor Circuit High Voltage (Bank 1 Sensor 1)"; break;
                case "P0133": description = "Oxygen Sensor Circuit Slow Response (Bank 1 Sensor 1)"; break;
                case "P0134": description = "Oxygen Sensor Circuit No Activity Detected (Bank 1 Sensor 1)"; break;
                case "P0135": description = "Oxygen Sensor Heater Circuit Malfunction (Bank 1 Sensor 1)"; break;
                case "P0136": description = "Oxygen Sensor Circuit Malfunction (Bank 1 Sensor 2)"; break;
                case "P0137": description = "Oxygen Sensor Circuit Low Voltage (Bank 1 Sensor 2)"; break;
                case "P0138": description = "Oxygen Sensor Circuit High Voltage (Bank 1 Sensor 2)"; break;
                case "P0139": description = "Oxygen Sensor Circuit Slow Response (Bank 1 Sensor 2)"; break;
                case "P0140": description = "Oxygen Sensor Circuit No Activity Detected (Bank 1 Sensor 2)"; break;
                case "P0141": description = "Oxygen Sensor Heater Circuit Malfunction (Bank 1 Sensor 2)"; break;
                case "P0142": description = "Oxygen Sensor Circuit Malfunction (Bank 1 Sensor 3)"; break;
                case "P0143": description = "Oxygen Sensor Circuit Low Voltage (Bank 1 Sensor 3)"; break;
                case "P0144": description = "Oxygen Sensor Circuit High Voltage (Bank 1 Sensor 3)"; break;
                case "P0145": description = "Oxygen Sensor Circuit Slow Response (Bank 1 Sensor 3)"; break;
                case "P0146": description = "Oxygen Sensor Circuit No Activity Detected (Bank 1 Sensor 3)"; break;
                case "P0147": description = "Oxygen Sensor Heater Circuit Malfunction (Bank 1 Sensor 3)"; break;
                case "P0150": description = "Oxygen Sensor Circuit Malfunction (Bank 2 Sensor 1)"; break;
                case "P0151": description = "Oxygen Sensor Circuit Low Voltage (Bank 2 Sensor 1)"; break;
                case "P0152": description = "Oxygen Sensor Circuit High Voltage (Bank 2 Sensor 1)"; break;
                case "P0153": description = "Oxygen Sensor Circuit Slow Response (Bank 2 Sensor 1)"; break;
                case "P0154": description = "Oxygen Sensor Circuit No Activity Detected (Bank 2 Sensor 1)"; break;
                case "P0155": description = "Oxygen Sensor Heater Circuit Malfunction (Bank 2 Sensor 1)"; break;
                case "P0156": description = "Oxygen Sensor Circuit Malfunction (Bank 2 Sensor 2)"; break;
                case "P0157": description = "Oxygen Sensor Circuit Low Voltage (Bank 2 Sensor 2)"; break;
                case "P0158": description = "Oxygen Sensor Circuit High Voltage (Bank 2 Sensor 2)"; break;
                case "P0159": description = "Oxygen Sensor Circuit Slow Response (Bank 2 Sensor 2)"; break;
                case "P0160": description = "Oxygen Sensor Circuit No Activity Detected (Bank 2 Sensor 2)"; break;
                case "P0161": description = "Oxygen Sensor Heater Circuit Malfunction (Bank 2 Sensor 2)"; break;
                case "P0162": description = "Oxygen Sensor Circuit Malfunction (Bank 2 Sensor 3)"; break;
                case "P0163": description = "Oxygen Sensor Circuit Low Voltage (Bank 2 Sensor 3)"; break;
                case "P0164": description = "Oxygen Sensor Circuit High Voltage (Bank 2 Sensor 3)"; break;
                case "P0165": description = "Oxygen Sensor Circuit Slow Response (Bank 2 Sensor 3)"; break;
                case "P0166": description = "Oxygen Sensor Circuit No Activity Detected (Bank 2 Sensor 3)"; break;
                case "P0167": description = "Oxygen Sensor Heater Circuit Malfunction (Bank 2 Sensor 3)"; break;
                default: description = string.Empty; break;
            }

            return description;

        }
    }
}
