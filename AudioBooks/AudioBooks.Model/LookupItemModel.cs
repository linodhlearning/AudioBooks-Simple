using System;
using System.Collections.Generic;
using System.Text;

namespace AudioBooks.Model
{
    public class LookupItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RecordOwnerId { get; set; }
    }
}
