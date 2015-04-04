namespace Labo.Video
{
    using System;
    using System.Collections.Generic;

    using Labo.Common.Ioc;
    using Labo.Video.Services;

    public sealed class LaboVideoModule : IIocModule, ILaboVideoModule
    {
        private readonly IDictionary<string, Func<IIocContainerResolver, IVideoInfoRetriever>> m_VideoInfoRetrieverFuncs = new Dictionary<string, Func<IIocContainerResolver, IVideoInfoRetriever>>();

        public void Configure(IIocContainer registry)
        {
            registry.RegisterSingleInstance<IVideoRetrieveService>(x => new VideoRetrieveService(x.GetInstance<IIocContainer>()));

            foreach (KeyValuePair<string, Func<IIocContainerResolver, IVideoInfoRetriever>> videoInfoRetrieverFunc in m_VideoInfoRetrieverFuncs)
            {
                registry.RegisterInstanceNamed(videoInfoRetrieverFunc.Value, videoInfoRetrieverFunc.Key);
            }
        }

        public void RegisterVideoInfoRetriever(Func<IIocContainerResolver, IVideoInfoRetriever> creator, string name)
        {
            m_VideoInfoRetrieverFuncs.Add(name, creator);
        }
    }
}
