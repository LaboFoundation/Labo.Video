namespace Labo.Video.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Labo.Common.Ioc;
    using Labo.Video.Model;

    internal sealed class VideoRetrieveService : IVideoRetrieveService
    {
        private readonly IIocContainer m_IocContainer;

        public VideoRetrieveService(IIocContainer iocContainer)
        {
            m_IocContainer = iocContainer;
        }

        public void RegisterRetriver(IVideoInfoRetriever retriever)
        {
            if (retriever == null)
            {
                throw new ArgumentNullException("retriever");
            }

            Type type = retriever.GetType();
            string key = type.AssemblyQualifiedName;
            if (!m_IocContainer.IsRegistered<IVideoInfoRetriever>(key))
            {
                m_IocContainer.RegisterSingleInstanceNamed(x => retriever, key);
            }
        }

        public IList<IVideoInfoRetriever> GetRetrivers()
        {
            return m_IocContainer.GetAllInstances<IVideoInfoRetriever>().ToList();
        }

        public RemoteVideoContentModel RetrieveVideoInfo(string url)
        {
            IList<IVideoInfoRetriever> videoInfoRetrievers = GetRetrivers();
            for (int i = 0; i < videoInfoRetrievers.Count; i++)
            {
                IVideoInfoRetriever videoInfoRetriever = videoInfoRetrievers[i];
                if (videoInfoRetriever.CheckVideoUrl(url))
                {
                    RemoteVideoContentModel videoInfo = videoInfoRetriever.RetrieveVideoInfo(url);
                    if (videoInfo != null)
                    {
                        videoInfo.ProviderID = videoInfoRetriever.ProviderID;
                        return videoInfo;
                    }
                }
            }

            return null;
        }
    }
}
