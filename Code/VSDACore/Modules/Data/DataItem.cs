namespace VSDACore.Modules.Data
{
    public class DataItem : IDataItem
    {
        public double Value { get; private set; }

        public string StringValue { get; private set; }

        public ValueType Type { get; set; }

        public DataItem(string stringValue)
        {
            this.Value = double.NaN;
            this.StringValue = stringValue;
        }

        public DataItem(double value, string stringValue)
        {
            this.Value = value;
            this.StringValue = stringValue;
        }
    }
}
