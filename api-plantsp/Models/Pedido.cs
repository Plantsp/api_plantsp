using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_plantsp.Models
{
    public class Pedido
    {
        [Display(Name = "IDPED")]
        public int IDPED { get; set; }

        [Display(Name = "IDCLI")]
        public int IDCLI { get; set; }

        [Display(Name = "DATAPEDIDO")]
        public DateTime DATAPEDIDO { get; set; }

        [Display(Name = "TOTALCOMPRA")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TOTALCOMPRA { get; set; }

        [Display(Name = "FORMAPAGAMENTO")]
        public string FORMAPAGAMENTO { get; set; }

        public List<ItemPedido> ITENSPEDIDO { get; set; }
    }

    public class ItemPedido
    {
        [Display(Name = "IDPROD")]
        public int IDPROD { get; set; }

        [Display(Name = "NOMEPROD")]
        public string NOMEPROD { get; set; }

        [Display(Name = "QUANTIDADE")]
        public int QUANTIDADE { get; set; }

        [Display(Name = "VALOR")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal VALOR { get; set; }

        [Display(Name = "SUBTOTAL")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal SUBTOTAL { get; set; }
    }
}
