using System.ComponentModel.DataAnnotations.Schema;

namespace AudioBooks.Domain
{
    [Table("Audio", Schema = "dbo")]

    public class Audio
    {
        //  [System.ComponentModel.DataAnnotations.Key, Column("AudioId"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName = "varchar(500)")]
        public string CoverImage { get; set; }
        public int DurationInMinutes { get; set; }
        [Column(TypeName = "varchar(500)")]
        public string AudibleLink { get; set; }
        public int AudioBookId { get; set; }
    }
}
