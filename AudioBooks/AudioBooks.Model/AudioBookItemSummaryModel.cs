namespace AudioBooks.Model
{
    public class AudioBookItemSummaryModel
    {
        public int AudioBookId { get; set; }
        public string CoverImageFileName { get; set; }
        public string AudioBookName { get; set; }
        public string AuthorName { get; set; }
        public string QuickText { get; set; }
        public string AudibleLink { get; set; }
    }
}
