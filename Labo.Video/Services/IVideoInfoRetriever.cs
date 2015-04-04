namespace Labo.Video.Services
{
    using Labo.Video.Model;

    public interface IVideoInfoRetriever
    {
        bool CheckVideoUrl(string url);

        RemoteVideoContentModel RetrieveVideoInfo(string url);

        int ProviderID { get; }
    }
}