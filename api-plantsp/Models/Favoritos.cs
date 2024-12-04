using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api_plantsp.Models
{
    public class Favoritos
    {
        [Display(Name = "IDFAV")]
        public int IDFAV { get; set; }

        [Display(Name = "IDCLI")]
        public int IDCLI { get; set; }
        public List<Produto>? ITENSFAV { get; set; }
    }
}
