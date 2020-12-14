using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AudioBooks.Domain
{
    public class Category
    {
        public Category()
        {
            this.AudioBookCategories = new List<AudioBookCategory>();
        }
        public int Id { get; set; }
        [Column(TypeName = "varchar(500)")]
        public string CategoryName { get; set; }
        public List<AudioBookCategory> AudioBookCategories { get; set; }
    }
}
