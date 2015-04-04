namespace Labo.Video.Services
{
    using System.Collections.Generic;

    using Labo.Video.Model;

    public interface IVideoRetrieveService
    {
        void RegisterRetriver(IVideoInfoRetriever retriever);

        IList<IVideoInfoRetriever> GetRetrivers();

        RemoteVideoContentModel RetrieveVideoInfo(string url);
    }
}