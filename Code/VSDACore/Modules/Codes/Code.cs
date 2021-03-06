﻿namespace VSDACore.Modules.Codes
{
    public class Code : ICode
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Cause { get; set; }

        public string Solution { get; set; }

        public Code(string name)
        {
            this.Name = name;
            this.Description = "Unknown Code";
            this.Cause = "Unknown";
            this.Solution = "Unknown";
        }

        public Code(string name, string description)
        {
            this.Name = name;
            this.Description = description;
            this.Cause = "Unknown";
            this.Solution = "Unknown";
        }
    }
}
