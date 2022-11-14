namespace shigLeBot
{
    internal static class Platform
    {
        public static readonly string platform;

        static Platform()
        {
            platform = Environment.GetEnvironmentVariable("Platform") ?? "Local";
        }
    }
}
