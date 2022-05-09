using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace moveis_api.Models
{
    [Table("product")]
    public class ProductModel
    {
        [Key]
        public int code { get; set; }

        public string? name { get; set; }

        public string? image { get; set; }

        public string? whatsapp { get; set; }

        public double value { get; set; }

        public int categoryCode { get; set; }

        public int addressCode { get; set; }
        public CategoryModel? category { get; set; }

        public AddressModel? address { get; set; }


    }

}