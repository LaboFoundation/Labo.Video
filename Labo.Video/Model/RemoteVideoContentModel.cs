namespace Labo.Video.Model
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public sealed class RemoteVideoContentModel
    {
        private List<string> m_Tags;

        private List<string> m_Categories;

        public string Title { get; set; }

        public string Description { get; set; }

        public long ViewCount { get; set; }

        public int Duration { get; set; }

        public string Author { get; set; }

        public string Uploader { get; set; }

        public List<string> Tags
        {
            get
            {
                return m_Tags ?? (m_Tags = new List<string>());
            }

            set
            {
                m_Tags = value;
            }
        }

        public string BigPicture { get; set; }

        public string SmallPicture { get; set; }

        public double Rating { get; set; }

        public int VoteCount { get; set; }

        public string EmbedUrl { get; set; }

        public string ProviderUniqueID { get; set; }

        public int? ProviderID { get; set; }

        public string Summary { get; set; }

        public List<string> Categories
        {
            get
            {
                return m_Categories ?? (m_Categories = new List<string>());
            }

            set
            {
                m_Categories = value;
            }
        }

        public DateTime LastUpdateDate { get; set; }
    }
}
