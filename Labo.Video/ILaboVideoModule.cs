namespace Labo.Video
{
    using System;

    using Labo.Common.Ioc;
    using Labo.Video.Services;

    public interface ILaboVideoModule
    {
        void RegisterVideoInfoRetriever(Func<IIocContainerResolver, IVideoInfoRetriever> creator, string name);
    }
}