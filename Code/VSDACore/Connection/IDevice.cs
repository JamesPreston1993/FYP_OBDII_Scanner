namespace VSDACore.Connection
{
    public interface IDevice
    {
        string Name { get; }

        string Id { get; }

        string Address { get; }
    }
}
