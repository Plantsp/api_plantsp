using System.ComponentModel.DataAnnotations;

namespace api_plantsp.Models
{
    public class Login
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "O campo email é obrigatório")]
        public string EMAIL { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "O campo senha é obrigatório")]
        public string SENHA { get; set; }
    }
}
