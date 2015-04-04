namespace Labo.Youtube.Services
{
    using System;
    using System.Linq;

    using Google.GData.Client;
    using Google.GData.YouTube;
    using Google.YouTube;

    using Labo.Youtube.Model;
    using Labo.Youtube.Settings;

    public sealed class YoutubeVideoService : IYoutubeVideoService
    {
        private readonly IYoutubeVideoServiceSettings m_YoutubeVideoServiceSettings;

        public YoutubeVideoService(IYoutubeVideoServiceSettings youtubeVideoServiceSettings)
        {
            m_YoutubeVideoServiceSettings = youtubeVideoServiceSettings;
        }

        public VideoContentInfoModel GetVideoContentInfo(string url)
        {
            try
            {
                YouTubeRequestSettings settings = new YouTubeRequestSettings(m_YoutubeVideoServiceSettings.ApplicationName, m_YoutubeVideoServiceSettings.DeveloperKey);
                YouTubeRequest request = new YouTubeRequest(settings);
                Video video = request.Retrieve<Video>(new Uri(GetVideoSearchUrl(url)));
                int duration;
                int.TryParse(video.Media.Duration.Seconds, out duration);
                return new VideoContentInfoModel
                {
                    Keywords = video.Keywords,
                    Private = video.Private,
                    Duration = duration,
                    Uploader = video.Uploader,
                    Author = video.Author,
                    BigPicture = video.Thumbnails.Where(x => x.Width == "320" && x.Height == "180").Select(x => x.Url).FirstOrDefault(),
                    SmallPicture = video.Thumbnails.Where(x => x.Width == "120" && x.Height == "90").Select(x => x.Url).FirstOrDefault(),
                    Tags = video.Tags.Select(x => x.Value).ToList(),
                    Categories = video.Categories.Select(x => x.Label).ToList(),
                    Summary = video.Summary,
                    Description = video.Description ?? string.Empty,
                    Rating = video.RatingAverage,
                    VoteCount = video.YouTubeEntry.Rating == null ? 0 : video.YouTubeEntry.Rating.NumRaters,
                    ViewCount = video.ViewCount,
                    Title = video.Title,
                    VideoUrl = video.WatchPage,
                    VideoId = video.VideoId,
                    LastUpdateDate = video.Updated
                };
            }
            catch (GDataRequestException)
            {
                // TODO: Log
                return null;
            }
        }

        private static string GetVideoSearchUrl(string url)
        {
            Uri uri = new Uri(url);
            string id = System.Web.HttpUtility.ParseQueryString(uri.Query)["v"];
            string defaultVideoUri = YouTubeQuery.DefaultVideoUri.TrimEnd('/');

            return string.Format("{0}/{1}", defaultVideoUri, id);
        }
    }
}
