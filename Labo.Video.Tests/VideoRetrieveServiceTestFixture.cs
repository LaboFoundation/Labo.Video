namespace Labo.Video.Tests
{
    using System;
    using System.Collections.Generic;

    using Labo.Common.Ioc;
    using Labo.Video.Model;
    using Labo.Video.Services;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class VideoRetrieveServiceTestFixture
    {
        [Test]
        public void RegisterRetriever_MustRegisterSingleInstanceNamedWithAsseblyQualifiedName()
        {
            IIocContainer iocContainer = Substitute.For<IIocContainer>();
            IVideoInfoRetriever videoInfoRetriever = Substitute.For<IVideoInfoRetriever>();
            iocContainer.WhenForAnyArgs(y => y.RegisterSingleInstanceNamed(null, null)).Do(
                x =>
                    {
                        Assert.AreEqual(videoInfoRetriever.GetType().AssemblyQualifiedName, x.Arg<string>());
                        Assert.AreEqual(videoInfoRetriever, x.Arg<Func<IIocContainerResolver, IVideoInfoRetriever>>().Invoke(iocContainer));
                    });

            VideoRetrieveService service = new VideoRetrieveService(iocContainer);
            service.RegisterRetriver(videoInfoRetriever);
        }

        [Test]
        public void RegisterRetriever_ContainerRegisterSingleInstanceNamedMustNotCalledMoreThanOnceWhenRetrieverIsAlreadyRegistered()
        {
            IIocContainer iocContainer = Substitute.For<IIocContainer>();
            iocContainer.IsRegistered<IVideoInfoRetriever>(null).ReturnsForAnyArgs(x => true);

            IVideoInfoRetriever videoInfoRetriever = Substitute.For<IVideoInfoRetriever>();

            VideoRetrieveService service = new VideoRetrieveService(iocContainer);
            service.RegisterRetriver(videoInfoRetriever);

            iocContainer.DidNotReceiveWithAnyArgs().RegisterSingleInstanceNamed<IVideoInfoRetriever>(null, null);
        }

        [Test]
        public void RetrieveVideoInfo_MustNotReturnNullWhenVideoInfoRetrieverCheckVideoUrlMethodReturnsTrue()
        {
            IVideoInfoRetriever youtubeVideoInfoRetriever = Substitute.For<IVideoInfoRetriever>();
            
            const string wwwYoutubeCom = "www.youtube.com";
            youtubeVideoInfoRetriever.CheckVideoUrl(wwwYoutubeCom).Returns(x => true); 
            youtubeVideoInfoRetriever.RetrieveVideoInfo(wwwYoutubeCom).Returns(x => new RemoteVideoContentModel());

            List<IVideoInfoRetriever> videoInfoRetrievers = new List<IVideoInfoRetriever> { youtubeVideoInfoRetriever };

            IIocContainer iocContainer = Substitute.For<IIocContainer>();
            iocContainer.GetAllInstances<IVideoInfoRetriever>().Returns(x => videoInfoRetrievers);

            VideoRetrieveService service = new VideoRetrieveService(iocContainer);
            Assert.IsNotNull(service.RetrieveVideoInfo(wwwYoutubeCom));
        }

        [Test]
        public void RetrieveVideoInfo_MustReturnNullWhenVideoInfoRetrieverParseUrlReturnsNull()
        {
            IVideoInfoRetriever youtubeVideoInfoRetriever = Substitute.For<IVideoInfoRetriever>();

            const string wwwYoutubeCom = "www.youtube.com";
            youtubeVideoInfoRetriever.CheckVideoUrl(wwwYoutubeCom).Returns(x => true);
            youtubeVideoInfoRetriever.RetrieveVideoInfo(wwwYoutubeCom).Returns(x => null);

            List<IVideoInfoRetriever> videoInfoRetrievers = new List<IVideoInfoRetriever> { youtubeVideoInfoRetriever };

            IIocContainer iocContainer = Substitute.For<IIocContainer>();
            iocContainer.GetAllInstances<IVideoInfoRetriever>().Returns(x => videoInfoRetrievers);

            VideoRetrieveService service = new VideoRetrieveService(iocContainer);
            Assert.IsNull(service.RetrieveVideoInfo(wwwYoutubeCom));
        }

        [Test]
        public void RetrieveVideoInfo_MustReturnNullWhenIocContainerReturnsEmptyRetrieverList()
        {
            IIocContainer iocContainer = Substitute.For<IIocContainer>();
            iocContainer.GetAllInstances<IVideoInfoRetriever>().Returns(x => new List<IVideoInfoRetriever>());

            VideoRetrieveService service = new VideoRetrieveService(iocContainer);
            Assert.IsNull(service.RetrieveVideoInfo("www.youtube.com"));
        }

        [Test]
        public void RetrieveVideoInfo_MustReturnNullWhenVideoInfoRetrieverCheckVideoUrlMethodReturnsFalse()
        {
            IVideoInfoRetriever youtubeVideoInfoRetriever = Substitute.For<IVideoInfoRetriever>();
            const string wwwYoutubeCom = "www.youtube.com";
            youtubeVideoInfoRetriever.CheckVideoUrl(wwwYoutubeCom).Returns(x => true);
            youtubeVideoInfoRetriever.RetrieveVideoInfo(wwwYoutubeCom).Returns(x => new RemoteVideoContentModel());

            List<IVideoInfoRetriever> videoInfoRetrievers = new List<IVideoInfoRetriever> { youtubeVideoInfoRetriever };

            IIocContainer iocContainer = Substitute.For<IIocContainer>();
            iocContainer.GetAllInstances<IVideoInfoRetriever>().Returns(x => videoInfoRetrievers);

            VideoRetrieveService service = new VideoRetrieveService(iocContainer);
            Assert.IsNull(service.RetrieveVideoInfo("www.vimeo.com"));
        }

        [Test]
        public void RetrieveVideoInfo()
        {
            IVideoInfoRetriever youtubeVideoInfoRetriever = Substitute.For<IVideoInfoRetriever>();
            const string wwwYoutubeCom = "www.youtube.com";
            youtubeVideoInfoRetriever.CheckVideoUrl(wwwYoutubeCom).Returns(x => true);
            youtubeVideoInfoRetriever.RetrieveVideoInfo(wwwYoutubeCom).Returns(x => new RemoteVideoContentModel());

            IVideoInfoRetriever vimeoVideoInfoRetriever = Substitute.For<IVideoInfoRetriever>();
            const string wwwVimeoCom = "www.vimeo.com";
            vimeoVideoInfoRetriever.CheckVideoUrl(wwwVimeoCom).Returns(x => false);
            vimeoVideoInfoRetriever.RetrieveVideoInfo(wwwVimeoCom).Returns(x => new RemoteVideoContentModel());

            IVideoInfoRetriever dailymotionVideoInfoRetriever = Substitute.For<IVideoInfoRetriever>();
            const string wwwDailymotionCom = "www.dailymotion.com";
            dailymotionVideoInfoRetriever.CheckVideoUrl(wwwDailymotionCom).Returns(x => true);
            dailymotionVideoInfoRetriever.RetrieveVideoInfo(wwwDailymotionCom).Returns(x => new RemoteVideoContentModel());

            List<IVideoInfoRetriever> videoInfoRetrievers = new List<IVideoInfoRetriever> { youtubeVideoInfoRetriever, vimeoVideoInfoRetriever, dailymotionVideoInfoRetriever };

            IIocContainer iocContainer = Substitute.For<IIocContainer>();
            iocContainer.GetAllInstances<IVideoInfoRetriever>().Returns(x => videoInfoRetrievers);

            VideoRetrieveService service = new VideoRetrieveService(iocContainer);
            Assert.IsNotNull(service.RetrieveVideoInfo(wwwYoutubeCom));
            Assert.IsNull(service.RetrieveVideoInfo(wwwVimeoCom));
            Assert.IsNotNull(service.RetrieveVideoInfo(wwwDailymotionCom));
        }

        [Test]
        public void RetrieveVideoInfo_VideoProviderIdMustBeSetWhenVideoInfoCanBeRetrievedByRetriever()
        {
            IVideoInfoRetriever youtubeVideoInfoRetriever = Substitute.For<IVideoInfoRetriever>();
            const string wwwYoutubeCom = "www.youtube.com";
            youtubeVideoInfoRetriever.CheckVideoUrl(wwwYoutubeCom).Returns(x => true);
            youtubeVideoInfoRetriever.RetrieveVideoInfo(wwwYoutubeCom).Returns(x => new RemoteVideoContentModel());
            youtubeVideoInfoRetriever.ProviderID.Returns(1);

            IVideoInfoRetriever vimeoVideoInfoRetriever = Substitute.For<IVideoInfoRetriever>();
            const string wwwVimeoCom = "www.vimeo.com";
            vimeoVideoInfoRetriever.CheckVideoUrl(wwwVimeoCom).Returns(x => true);
            vimeoVideoInfoRetriever.RetrieveVideoInfo(wwwVimeoCom).Returns(x => new RemoteVideoContentModel());
            vimeoVideoInfoRetriever.ProviderID.Returns(2);

            IVideoInfoRetriever dailymotionVideoInfoRetriever = Substitute.For<IVideoInfoRetriever>();
            const string wwwDailymotionCom = "www.dailymotion.com";
            dailymotionVideoInfoRetriever.CheckVideoUrl(wwwDailymotionCom).Returns(x => true);
            dailymotionVideoInfoRetriever.RetrieveVideoInfo(wwwDailymotionCom).Returns(x => new RemoteVideoContentModel());
            dailymotionVideoInfoRetriever.ProviderID.Returns(2);

            List<IVideoInfoRetriever> videoInfoRetrievers = new List<IVideoInfoRetriever> { youtubeVideoInfoRetriever, vimeoVideoInfoRetriever, dailymotionVideoInfoRetriever };

            IIocContainer iocContainer = Substitute.For<IIocContainer>();
            iocContainer.GetAllInstances<IVideoInfoRetriever>().Returns(x => videoInfoRetrievers);

            VideoRetrieveService service = new VideoRetrieveService(iocContainer);
            Assert.AreEqual(youtubeVideoInfoRetriever.ProviderID, service.RetrieveVideoInfo(wwwYoutubeCom).ProviderID);
            Assert.AreEqual(vimeoVideoInfoRetriever.ProviderID, service.RetrieveVideoInfo(wwwVimeoCom).ProviderID);
            Assert.AreEqual(dailymotionVideoInfoRetriever.ProviderID, service.RetrieveVideoInfo(wwwDailymotionCom).ProviderID);
        }
    }
}
