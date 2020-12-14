using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioBooks.Web.Models
{
    public class LookupDataCacheViewModel  
    {
        public Model.LookupDataCacheModel  DataModel { get; private set; } 

        public LookupDataCacheViewModel( Model.LookupDataCacheModel model)
    {
        this.DataModel = model;
    }
}
}
