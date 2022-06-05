using System.ComponentModel;

namespace Gleisbelegung.App
{
    public class Version
    {
        private const int Major = 0;
        private const int Minor = 1;
        private const int Patch = 6;

        public static string ToString()
        {
            return $"{Major}.{Minor}.{Patch}";
        }
    }
}