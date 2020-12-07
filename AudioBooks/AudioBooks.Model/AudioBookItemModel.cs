using System;

namespace AudioBooks.Model
{
    public class AudioBookItemModel
    {
        public int AudioBookId { get; set; }
        public string CoverImageFileName { get; set; }
        public int AudioBookName { get; set; }
        public string AuthorName { get; set; }
        public string QuickText { get; set; }
        public string AudibleLink { get; set; }


        public string Description { get; set; }
        public string PublisherName { get; set; }
        public DateTime PublishedDate { get; set; } 
        public string Duration { get; set; } 
    }
}
