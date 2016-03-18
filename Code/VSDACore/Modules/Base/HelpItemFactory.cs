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
                case "Home": helpItems = HomeHelpItems(); break;
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
                                                "Blue: Smart graphing not available\n" +
                                                "Green: Current value is within the expected range\n" +
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

        private static IList<IHelpItem> HomeHelpItems()
        {
            IList<IHelpItem> homeHelpItems = new List<IHelpItem>();

            homeHelpItems.Add(new HelpItem("Codes", "Start here. If you see your \"Check Engine Light\" is turned on, see if there are any codes present and hit clear codes."));
            homeHelpItems.Add(new HelpItem("Data", "Check individual components for unexpected values. Graphs will change colour if an unexpected value is detected."));
            homeHelpItems.Add(new HelpItem("Connection", "Connect to a different ELM327 device or vehicle."));
            homeHelpItems.Add(new HelpItem("Email", "Send an email report of what you see on screen to your mechanic."));
            homeHelpItems.Add(new HelpItem("Help", "View specific hints and tips for how to use each individual module ."));

            return homeHelpItems;
        }
    }
}
