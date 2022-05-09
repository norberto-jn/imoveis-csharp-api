using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace moveis_api.Models
{
    [Table("address")]
    public class AddressModel
    {
        [Key]
        public int code { get; set; }
        public string? cep { get; set; }
        public string? logradouro { get; set; }
        public string? bairro { get; set; }
        public string? localidade { get; set; }
        public string? uf { get; set; }

    }

}