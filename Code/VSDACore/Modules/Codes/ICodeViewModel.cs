namespace VSDACore.Modules.Codes
{
    public interface ICodeViewModel
    {
        ICode CodeModel { get; }

        string Name { get; }

        string Description { get; }

        string Cause { get; }

        string Solution { get; }
    }
}
