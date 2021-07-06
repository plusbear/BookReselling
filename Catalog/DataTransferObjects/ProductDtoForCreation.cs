using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.DataTransferObjects
{
    public class ProductDtoForCreation
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int SellerId { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<string> Images { get; set; }
    }
}
