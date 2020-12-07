using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AudioBooks.Domain
{
    public class Publisher
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string PublisherName { get; set; }
        public List<AudioBook> AudioBooks { get; set; }
    }
}
