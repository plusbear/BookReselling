using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Catalog.Models
{
    public class Product
    {
        [Column("ProductId")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Продукт должен иметь название"), MaxLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Продукт должен иметь описание")]
        public string Description { get; set; }
        public int SellerId { get; set; }
        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
        public IEnumerable<Image> Images { get; set; }
    }
}
