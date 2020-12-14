using System;
using System.Collections.Generic;
using System.Text;

namespace AudioBooks.Model
{
    public class LookupDataCacheModel
    {
        public LookupDataCacheModel()
        {
            this.Authors = new List<LookupItemModel>();
            this.Catgegories = new List<LookupItemModel>();
            this.Publishers = new List<LookupItemModel>();
        }

        public IEnumerable<LookupItemModel> Authors { get; set; }
        public IEnumerable<LookupItemModel> Catgegories { get; set; }
        public IEnumerable<LookupItemModel> Publishers { get; set; }
    }
}
