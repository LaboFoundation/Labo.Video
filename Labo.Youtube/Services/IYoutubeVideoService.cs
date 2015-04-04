namespace Labo.Youtube.Services
{
    using Labo.Youtube.Model;

    public interface IYoutubeVideoService
    {
        VideoContentInfoModel GetVideoContentInfo(string url);
    }
}