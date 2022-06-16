namespace Gleisbelegung.App.Common
{
    public class ComputerPlatforms
    {
        public const string WINDOWS = "Windows";
        public const string LINUX = "X11";
        public const string MACOSX = "OSX";
        public const string ANDROID = "Android";
        public const string IOS = "iOS";
        public const string WEB = "HTML5";
        public const string SERVER = "Server";
        public const string WINDOWS_UWP = "UWP";

        public static bool IsDesktopPlatform(string platform)
        {
            switch (platform)
            {
                case WINDOWS:
                case LINUX:
                case MACOSX:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsMobilePlatform(string platform)
        {
            switch (platform)
            {
                case ANDROID:
                case IOS:
                    return true;
                default:
                    return false;
            }
        }
    }
}