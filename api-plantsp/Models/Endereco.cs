using System.ComponentModel.DataAnnotations;
namespace api_plantsp.Models
{
    public class Endereco
    {

        [Display(Name = "IDEND")]
        public int IDEND { get; set; }

        [Display(Name = "CEP")]
        public string? CEP { get; set; }

        [Display(Name = "CIDADE")]
        public string? CIDADE { get; set; }

        [Display(Name = "UF")]
        public string? UF { get; set; }

        [Display(Name = "LOGRADOURO")]
        public string? LOGRADOURO { get; set; }

        [Display(Name = "BAIRRO")]
        public string? BAIRRO { get; set; }

        [Display(Name = "NUMERO")]
        public string? NUMERO { get; set; }

        [Display(Name = "COMPLEMENTO")]
        public string? COMPLEMENTO { get; set; }


        [Display(Name = "IDCLI")]
        public int IDCLI { get; set; }
    }
}
