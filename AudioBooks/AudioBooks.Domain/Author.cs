using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AudioBooks.Domain
{
    public class Author
    { 
        public int Id { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string AuthorName { get; set; }
        public List<AudioBook> AudioBooks { get; set; }

        public string RecordOwnerId { get; set; }
    }
}
