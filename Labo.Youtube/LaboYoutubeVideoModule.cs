namespace Labo.Youtube
{
    using System;
    using System.Collections.Generic;

    using Labo.Common.Ioc;
    using Labo.Video;
    using Labo.Video.Model;
    using Labo.Video.Services;
    using Labo.Youtube.Model;
    using Labo.Youtube.Services;
    using Labo.Youtube.Settings;

    public sealed class LaboYoutubeVideoModule : IIocModule
    {
        private readonly ILaboVideoModule m_LaboVideoModule;

        private readonly IYoutubeVideoServiceSettings m_YoutubeVideoServiceSettings;

        public LaboYoutubeVideoModule(ILaboVideoModule laboVideoModule, IYoutubeVideoServiceSettings youtubeVideoServiceSettings = null)
        {
            m_LaboVideoModule = laboVideoModule;
            m_YoutubeVideoServiceSettings = youtubeVideoServiceSettings;
        }

        public void Configure(IIocContainer registry)
        {
            registry.RegisterSingleInstance<IYoutubeVideoService>(x => new YoutubeVideoService(m_YoutubeVideoServiceSettings ?? new DefaultYoutubeVideoServiceSettings("zenliblabs", "AI39si4MiDp3EXjn58P53f7Eckw8a_UmenxQeaWGg9IxmmTE-UCMObHTIi4ItiCP6hciYRf2bXLFpiUiOUDqMf91-jB_QugUJg")));

            m_LaboVideoModule.RegisterVideoInfoRetriever(x => new YoutubeVideoInfoRetriever(x.GetInstance<IYoutubeVideoService>()), "Youtube");
            // m_LaboVideoModule.RegisterVideoInfoRetriever(x => new StubYoutubeVideoInfoRetriever(), "Stub");
        }

        private sealed class StubYoutubeVideoInfoRetriever : IVideoInfoRetriever
        {
            public bool CheckVideoUrl(string url)
            {
                return true;
            }

            public RemoteVideoContentModel RetrieveVideoInfo(string url)
            {
                return new RemoteVideoContentModel
                {
                    Duration = 60,
                    Uploader = "tester",
                    Author = "tester",
                    BigPicture = "https://i.ytimg.com/vi/bgQ4M1TofDg/mqdefault.jpg",
                    SmallPicture = "https://i.ytimg.com/vi/bgQ4M1TofDg/default.jpg",
                    Tags = new List<string>(),
                    Description = "Momento de la liberación de Sandino Bucio , declaración de lo acontecido y testimonio de su madre.",
                    Rating = 4,
                    VoteCount = 20,
                    ViewCount = 500,
                    Title = "Liberación y declaración de Sandino Bucio y su madre / 29 nov 2014",
                    EmbedUrl = "https://www.youtube.com/watch?v=bgQ4M1TofDg&feature=youtube_gdata_player",
                    ProviderUniqueID = "bgQ4M1TofDg1",
                    ProviderID = ProviderID,
                    LastUpdateDate = DateTime.Now,
                    Summary = "Liberación y declaración de Sandino Bucio y su madre / 29 nov 2014"
                };
            }

            public int ProviderID
            {
                get { return 1; }
            }
        }
    }
}
