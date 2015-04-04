namespace Labo.Youtube.Services
{
    using System;

    using Labo.Video.Model;
    using Labo.Video.Services;
    using Labo.Youtube.Model;

    internal sealed class YoutubeVideoInfoRetriever : IVideoInfoRetriever
    {
        private readonly IYoutubeVideoService m_YoutubeVideoService;

        public YoutubeVideoInfoRetriever(IYoutubeVideoService youtubeVideoService)
        {
            m_YoutubeVideoService = youtubeVideoService;
        }

        public bool CheckVideoUrl(string url)
        {
            return !string.IsNullOrWhiteSpace(url) && url.Contains("www.youtube.com");
        }

        public RemoteVideoContentModel RetrieveVideoInfo(string url)
        {
            VideoContentInfoModel video = m_YoutubeVideoService.GetVideoContentInfo(url);
            if (video == null)
            {
                return null;
            }

            return new RemoteVideoContentModel
                {
                    Duration = video.Duration,
                    Uploader = video.Uploader,
                    Author = video.Author,
                    BigPicture = video.BigPicture,
                    SmallPicture = video.SmallPicture,
                    Tags = video.Tags,
                    Description = video.Description,
                    Summary = video.Summary,
                    Rating = video.Rating,
                    VoteCount = video.VoteCount,
                    ViewCount = video.ViewCount,
                    Title = video.Title,
                    EmbedUrl = video.VideoUrl.ToStringInvariant(),
                    Categories = video.Categories,
                    LastUpdateDate = video.LastUpdateDate,
                    ProviderUniqueID = video.VideoId,
                    ProviderID = ProviderID
                };
        }

        public int ProviderID
        {
            get { return 1; }
        }
    }
}
