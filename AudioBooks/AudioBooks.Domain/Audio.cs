using System.ComponentModel.DataAnnotations.Schema;

namespace AudioBooks.Domain
{
    public class Audio
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(500)")]
        public string CoverImage { get; set; }
        public int DurationInMinutes { get; set; }
        [Column(TypeName = "varchar(500)")]
        public string AudibleLink { get; set; }
        public int AudioBookId { get; set; }
    }
}
