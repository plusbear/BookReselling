using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Catalog.Models
{
    public class Image
    {
        [Column("ImageId")]
        public int Id { get; set; }
        public int ProductId { get; set; }
        [Required]
        public string ImageName { get; set; }
        [Required]
        public string ImageRef { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
    }
}
