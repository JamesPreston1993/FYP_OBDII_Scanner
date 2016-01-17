namespace VSDACore.Modules.Codes
{
    public interface ICode
    {
        string Name { get; }

        string Description { get; }

        string Cause { get; set; }

        string Solution { get; set; }
    }
}
