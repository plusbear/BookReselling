namespace Catalog.RequestFeatures
{
    public class ProductParameters : RequestParameters
    {
        public int? CategoryId { get; set; }

        public bool IsCategorySet => CategoryId != null;
    }
}
