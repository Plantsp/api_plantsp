using System.ComponentModel.DataAnnotations;

namespace api_plantsp.Models
{
    public class Pedido
    {
        [Display(Name = "IDPED")]
        public string IDPED { get; set; }

        [Display(Name = "IDPROD")]
        public string? IDPROD { get; set; }

        [Display(Name = "IDCLI")]
        public string? IDCLI { get; set; }

        [Display(Name = "QTD")]
        public string? QTD { get; set; }
    }
}
