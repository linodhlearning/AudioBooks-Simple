using System;
//using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AudioBooks.Domain
{ 
    public class AudioBook
    {  
        public int Id { get; set; }
        [Column(TypeName = "varchar(500)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(1000)")]
        public string QuickSummary { get; set; }
        [Column(TypeName = "varchar(8000)")]
        public string Description { get; set; }  
        public Audio Audio { get; set; }
        public Author Author { get; set; }
        public Publisher Publisher { get; set; } 
        public DateTime PublishedDate { get; set; }

        //[Timestamp]
        //public byte[] RowVersion { get; set; }
    }

}
