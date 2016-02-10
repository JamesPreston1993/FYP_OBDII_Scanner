namespace VSDACore.Modules.Base
{
    public class HelpItem : IHelpItem
    {
        public string Title { get; private set; }

        public string Description { get; private set; }

        public HelpItem(string title, string description)
        {
            this.Title = title;
            this.Description = description;
        }
    }
}
