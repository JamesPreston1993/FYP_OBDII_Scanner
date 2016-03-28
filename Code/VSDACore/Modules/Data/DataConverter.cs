using System;

namespace VSDACore.Modules.Data
{
    public class DataConverter
    {
        public static IDataItem ConvertPID(IPid pid, string request)
        {
            double value = double.NaN;
            string stringValue = "No Value";

            int A, B, C, D;

            switch (pid.PidHex)
            {
                // Bitwise Converted
                case "01":
                    string binary = Convert.ToString(Convert.ToInt32(request, 16), 2).PadLeft(4, '0');
                    string milOnOff = string.Empty;
                    int numDTCs = 0;

                    if (binary.StartsWith("1"))
                        milOnOff = "ON";
                    else
                        milOnOff = "OFF";

                    numDTCs = Convert.ToInt32(Convert.ToByte(binary.Substring(1, 7), 16));

                    stringValue = string.Format("MIL: {0} DTCs: {1}", milOnOff, numDTCs);
                    break;

                case "1C":
                    A = Convert.ToInt32(Convert.ToByte(request.Substring(0, 2), 16));
                    switch (A)
                    {
                        case 1: stringValue = "OBD-II (CARB)"; break;
                        case 2: stringValue = "OBD-II (EPA)"; break;
                        case 3: stringValue = "OBD and OBD-II"; break;
                        case 4: stringValue = "OBD-I"; break;
                        case 5: stringValue = "Not OBD compliant"; break;
                        case 6: stringValue = "EOBD"; break;
                        case 7: stringValue = "EOBD and OBD-II"; break;
                        case 8: stringValue = "EOBD and OBD-I"; break;
                        case 9: stringValue = "EOBD, OBD-I and OBD-II"; break;
                        case 10: stringValue = "JOBD"; break;
                        case 11: stringValue = "JOBD and OBD-II"; break;
                        case 12: stringValue = "JOBD and EOBD"; break;
                        case 13: stringValue = "JOBD, EOBD and OBD-II"; break;
                        default: stringValue = "Unknown"; break;
                    }
                    break;


                // (A - 128) * (100 / 128)                
                case "06":
                case "07":
                case "08":
                case "09":
                case "2D":
                    A = Convert.ToInt32(Convert.ToByte(request.Substring(0, 2), 16));
                    value = (A - 128) * (100 / 128);
                    stringValue = value.ToString();
                    break;

                // (A * 100) / 255
                case "04":
                case "11":
                case "2C":
                case "2E":
                case "2F":
                    A = Convert.ToInt32(Convert.ToByte(request.Substring(0, 2), 16));
                    value = (A * 100) / 255;
                    stringValue = value.ToString();
                    // Do
                    break;

                // A - 40
                case "05":
                case "0F":
                    A = Convert.ToInt32(Convert.ToByte(request.Substring(0, 2), 16));
                    value = A - 40;
                    stringValue = value.ToString();
                    break;

                // A
                case "0B":
                case "0D":
                case "30":
                case "33":
                    A = Convert.ToInt32(Convert.ToByte(request.Substring(0, 2), 16));
                    value = A;
                    stringValue = value.ToString();
                    break;

                // A * 3
                case "0A":
                    A = Convert.ToInt32(Convert.ToByte(request.Substring(0, 2), 16));
                    value = A * 3;
                    stringValue = A.ToString();
                    break;

                // (A - 128) / 2
                case "0E":
                    A = Convert.ToInt32(Convert.ToByte(request.Substring(0, 2), 16));
                    value = (A - 128) / 2;
                    stringValue = A.ToString();
                    break;

                // ((A * 256) + B) / 4
                case "0C":
                case "32":
                    A = Convert.ToInt32(Convert.ToByte(request.Substring(0, 2), 16));
                    B = Convert.ToInt32(Convert.ToByte(request.Substring(2, 2), 16));
                    value = ((A * 256) + B) / 4;
                    stringValue = value.ToString();
                    break;

                // ((A * 256) + B) / 100
                case "10":
                    A = Convert.ToInt32(Convert.ToByte(request.Substring(0, 2), 16));
                    B = Convert.ToInt32(Convert.ToByte(request.Substring(2, 2), 16));
                    value = ((A * 256) + B) / 100;
                    stringValue = value.ToString();
                    break;

                // (A * 256) + B
                case "1F":
                case "21":
                case "31":
                    A = Convert.ToInt32(Convert.ToByte(request.Substring(0, 2), 16));
                    B = Convert.ToInt32(Convert.ToByte(request.Substring(2, 2), 16));
                    value = (A * 256) + B;
                    stringValue = value.ToString();
                    break;

                // ((A * 256) + B) * 0.079
                case "22":
                    A = Convert.ToInt32(Convert.ToByte(request.Substring(0, 2), 16));
                    B = Convert.ToInt32(Convert.ToByte(request.Substring(2, 2), 16));
                    value = ((A * 256) + B) * 0.079;
                    stringValue = value.ToString();
                    break;

                // A / 200
                case "14":
                case "15":
                case "16":
                case "17":
                case "18":
                case "19":
                case "1A":
                case "1B":
                    A = Convert.ToInt32(Convert.ToByte(request.Substring(0, 2), 16));
                    value = A / 200;
                    stringValue = value.ToString();
                    break;

                // (8 / 65536) * (256 * C + D)
                case "24":
                case "25":
                case "26":
                case "27":
                case "28":
                case "29":
                case "2A":
                case "2B":
                    C = Convert.ToInt32(Convert.ToByte(request.Substring(4, 2), 16));
                    D = Convert.ToInt32(Convert.ToByte(request.Substring(6, 2), 16));
                    value = (8 / 65536) * (256 * C + D);
                    stringValue = value.ToString();
                    break;

                // ((256 * C + D) / 256) - 128
                case "34":
                case "35":
                case "36":
                case "37":
                case "38":
                case "39":
                case "3A":
                case "3B":
                    C = Convert.ToInt32(Convert.ToByte(request.Substring(4, 2), 16));
                    D = Convert.ToInt32(Convert.ToByte(request.Substring(6, 2), 16));
                    value = ((256 * C + D) / 256) - 128;
                    stringValue = value.ToString();
                    break;

                // 10 * ((256 * A) + B)
                case "23":
                    A = Convert.ToInt32(Convert.ToByte(request.Substring(0, 2), 16));
                    B = Convert.ToInt32(Convert.ToByte(request.Substring(2, 2), 16));
                    value = 10 * ((256 * A) + B);
                    stringValue = value.ToString();
                    break;

                // (((256 * A) + B) / 10) - 40
                case "3C":
                case "3D":
                case "3E":
                case "3F":
                    A = Convert.ToInt32(Convert.ToByte(request.Substring(0, 2), 16));
                    B = Convert.ToInt32(Convert.ToByte(request.Substring(2, 2), 16));
                    value = 10 * (((256 * A) + B) / 10) - 40;
                    stringValue = value.ToString();
                    break;

            }

            IDataItem dataItem = new DataItem(value, stringValue);
            dataItem.Type = GetValueType(pid, dataItem);
            return dataItem;
        }

        private static ValueType GetValueType(IPid pid, IDataItem item)
        {
            ValueType type = ValueType.Default;
            switch (pid.PidHex)
            {
                case "0C":
                    if (item.Value < 800)
                        type = ValueType.Caution;
                    else if (item.Value > 5500)
                        type = ValueType.Danger;
                    else
                        type = ValueType.Normal;
                    break;
                case "21":
                    if (item.Value >= 50)
                        type = ValueType.Danger;
                    else if (item.Value <= 5)
                        type = ValueType.Caution;
                    else
                        type = ValueType.Normal;
                    break;
            }
            return type;
        }
    }
}
