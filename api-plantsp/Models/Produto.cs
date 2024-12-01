using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace api_plantsp.Models
{
    public class Produto
    {
        [Display(Name = "IDPROD")]
        public int IDPROD { get; set; }

        [Display(Name = "NOME")]
        public string? NOME { get; set; }

        [Display(Name = "DESCRICAO")]
        public string? DESCRICAO { get; set; }

        [Display(Name = "VALOR")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal? VALOR { get; set; }

        [Display(Name = "DESCONTO")]
        [Column(TypeName = "decimal(3,2)")]
        public decimal? DESCONTO { get; set; }

        [Display(Name = "CATEGORIA")]
        public string? CATEGORIA { get; set; }

        [Display(Name = "AVALIACAO")]
        [Column(TypeName = "decimal(3,2)")]
        public decimal? AVALIACAO { get; set; }

        [Display(Name = "DESCRICAODET")]
        public string? DESCRICAODET { get; set; }

        [Display(Name = "IMAGEM")]
        public string? IMAGEM { get; set; }

    }
}
