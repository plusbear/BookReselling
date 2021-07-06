using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.RequestFeatures
{
    public class ProductParameters : RequestParameters
    {
        public int? CategoryId { get; set; }

        public bool IsCategorySet => CategoryId != null;
    }
}
