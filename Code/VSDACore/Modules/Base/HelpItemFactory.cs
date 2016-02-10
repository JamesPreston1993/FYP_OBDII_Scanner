using System.Collections.Generic;

namespace VSDACore.Modules.Base
{
    public class HelpItemFactory
    {
        public static IList<IHelpItem> GetHelpItems(IModule module)
        {
            IList<IHelpItem> helpItems = new List<IHelpItem>();

            switch(module.Name)
            {
                case "Codes": helpItems = CodesHelpItems(); break;
                case "Data": helpItems = DataHelpItems(); break;
                case "Connection": helpItems = ConnectionHelpItems(); break;
            }

            return helpItems;
        }

        private static IList<IHelpItem> CodesHelpItems()
        {
            IList<IHelpItem> codeHelpItems = new List<IHelpItem>();
            
            codeHelpItems.Add(new HelpItem("Clear Codes", "Clear codes will remove all current codes and turn off the \"Check Engine\" light. If the light turns back on, then there is an underlying issue that must be fixed by a mechanic"));

            return codeHelpItems;
        }

        private static IList<IHelpItem> DataHelpItems()
        {
            IList<IHelpItem> dataHelpItems = new List<IHelpItem>();
            dataHelpItems.Add(new HelpItem("Gathering Samples", "Please allow up to five seconds for each sample to be retrieved when gathering data"));
            dataHelpItems.Add(new HelpItem("Graph Colours", "Some graphs will change colour based on their value. If the graph is red or orange for an extendend period of time, you should seek the advice of a mechanic\n\n" +
                                                "Blue: Current value is within the expected range\n" +
                                                "Orange: Current value indicates a potential issue but more information is required\n" +
                                                "Red: Current value indicates a serious problem"));

            return dataHelpItems;
        }

        private static IList<IHelpItem> ConnectionHelpItems()
        {
            IList<IHelpItem> connectionHelpItems = new List<IHelpItem>();

            connectionHelpItems.Add(new HelpItem("What device do I need?", "You will need an ELM327 Bluetooth device (version 1.3 or later)"));
            connectionHelpItems.Add(new HelpItem("Setup", "1) Enable Bluetooth on your PC / tablet\n" +
                                                          "2) Pair the ELM327 device with your PC / tablet / phone in your device settings\n" +
                                                          "3) Select your ELM327 device from the device list"));

            return connectionHelpItems;
        }
    }
}
