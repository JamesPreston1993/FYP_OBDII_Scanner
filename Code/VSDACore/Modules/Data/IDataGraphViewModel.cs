namespace VSDACore.Modules.Data
{
    public interface IDataGraphViewModel : IDataViewModel
    {
        double CursorPosition { get; }

        double MaxPossibleValue { get; }

        double MinPossibleValue { get; }
    }
}
