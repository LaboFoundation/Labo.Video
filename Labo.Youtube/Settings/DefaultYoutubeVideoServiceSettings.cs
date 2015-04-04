namespace Labo.Youtube.Settings
{
    public sealed class DefaultYoutubeVideoServiceSettings : IYoutubeVideoServiceSettings
    {
        public string ApplicationName { get; private set; }

        public string DeveloperKey { get; private set; }

        public DefaultYoutubeVideoServiceSettings(string applicationName, string developerKey)
        {
            DeveloperKey = developerKey;
            ApplicationName = applicationName;
        }
    }
}