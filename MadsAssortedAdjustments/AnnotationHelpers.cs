using System;


namespace MadsAssortedAdjustments

{
    public class Annotation : Attribute
    {
        private string description;
        private string defaultValue;
        private bool startSection;
        private bool endSection;
        private string sectionLabel;

        public Annotation(string description, string defaultValue, bool startSection = false, string sectionLabel = "", bool endSection = false)
        {
            Description = description;
            DefaultValue = defaultValue;
            StartSection = startSection;
            SectionLabel = sectionLabel;
            EndSection = endSection;
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        public string DefaultValue
        {
            get
            {
                return defaultValue;
            }
            set
            {
                defaultValue = value;
            }
        }

        public bool StartSection
        {
            get
            {
                return startSection;
            }
            set
            {
                startSection = value;
            }
        }

        public string SectionLabel
        {
            get
            {
                return sectionLabel;
            }
            set
            {
                sectionLabel = value;
            }
        }

        public bool EndSection
        {
            get
            {
                return endSection;
            }
            set
            {
                endSection = value;
            }
        }
    }
}

