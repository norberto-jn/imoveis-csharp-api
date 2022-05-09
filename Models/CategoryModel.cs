using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace moveis_api.Models
{
    [Table("category")]
    public class CategoryModel
    {
        [Key]
        public int code { get; set; }

        public string? name { get; set; }

        public List<ProductModel>? product;


    }

}