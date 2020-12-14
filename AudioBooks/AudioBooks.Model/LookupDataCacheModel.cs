using System;
using System.Collections.Generic;
using System.Text;

namespace AudioBooks.Model
{
    public class LookupDataCacheModel
    {
        public IEnumerable<LookupItemModel> Authors { get; set; }
        public IEnumerable<LookupItemModel> Catgegories { get; set; }
        public IEnumerable<LookupItemModel> Publishers { get; set; }
    }
}
