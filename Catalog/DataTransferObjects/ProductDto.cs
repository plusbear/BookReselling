using System.Collections.Generic;

namespace Catalog.DataTransferObjects
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SellerId { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<string> ImageRefs { get; set; }
    }
}
