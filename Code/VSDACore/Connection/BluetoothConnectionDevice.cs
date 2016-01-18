namespace VSDACore.Connection
{
    public class BluetoothConnectionDevice : IDevice
    {
        public string Name { get; private set; }

        public string Id { get; private set; }

        public string Address { get; private set; }

        public BluetoothConnectionDevice(string name, string id, string address)
        {
            this.Name = name;
            this.Id = id;
            this.Address = address;
        }
    }
}
