using System;

namespace Gleisbelegung.App.Data
{
    public class Settings
    {
        public int FontSize { get; set; }

        public static Settings LoadSettings()
        {
            // todo: save and load the settings from somewhere.
            return new Settings();
        }
    }
}