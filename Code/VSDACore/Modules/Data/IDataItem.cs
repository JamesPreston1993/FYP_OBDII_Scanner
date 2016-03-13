namespace VSDACore.Modules.Data
{
    public interface IDataItem
    {
        double Value { get; }

        string StringValue { get; }

        ValueType Type { get; set; }
    }

    public enum ValueType
    {
        Default,
        Normal,
        Caution,
        Danger
    }
}
