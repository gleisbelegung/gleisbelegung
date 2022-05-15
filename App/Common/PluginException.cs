using System;

namespace Gleisbelegung.App.Common
{
    public class PluginException : Exception
    {
        private readonly DateTime OccuredAt;

        public PluginException(string message) : base(message)
        {
            OccuredAt = DateTime.Now;
        }

        public override string ToString()
        {
            return $"[{OccuredAt.ToString("dd.MM.yyyy HH:mm:ss")}] {base.ToString()}";
        }
    }
}