using System;
using System.Collections.Generic;
using System.Text;

namespace AudioBooks.Domain
{
    public class AudioBookCategory
    /*EF Core Many to many*/
    {
        //public string CategoryName
        //{
        //    get { return this.Category?.CategoryName; }
        //}
        public int AudioBookId { get; set; }
        public int CategoryId { get; set; }
        public AudioBook AudioBook { get; set; }

        public Category Category { get; set; }
    }
}
