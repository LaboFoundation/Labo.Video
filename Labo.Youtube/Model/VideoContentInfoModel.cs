namespace Labo.Youtube.Model
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public sealed class VideoContentInfoModel
    {
        private List<string> m_Tags;

        private List<string> m_Categories;

        public string Keywords { get; set; }

        public bool Private { get; set; }

        public int Duration { get; set; }

        public string Uploader { get; set; }

        public string Author { get; set; }

        public string BigPicture { get; set; }

        public string SmallPicture { get; set; }

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

        public string Description { get; set; }

        public double Rating { get; set; }

        public int VoteCount { get; set; }

        public int ViewCount { get; set; }

        public string Title { get; set; }

        public Uri VideoUrl { get; set; }

        public string VideoId { get; set; }

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