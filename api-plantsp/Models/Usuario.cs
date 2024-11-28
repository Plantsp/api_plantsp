using System.ComponentModel.DataAnnotations;
namespace api_plantsp.Models

{
    public class Usuario
    {
        [Display(Name = "Id")]
        public int? IDCLI { get; set; }


        [Display(Name = "Nome completo")]
        //[Required(ErrorMessage = "O campo nome é obrigatório")]
        public string NOME { get; set; }

        [Display(Name = "Email")]
        //[Required(ErrorMessage = "O campo email é obrigatório")]
        public string EMAIL { get; set; }

        [Display(Name = "Senha")]
        //[Required(ErrorMessage = "O campo senha é obrigatório")]
        public string SENHA { get; set; }

        [Display(Name = "Sexo")]
        public string? SEXO { get; set; }

        [Display(Name = "CPF")]
        public string? CPF { get; set; }

        [Display(Name = "Telefone")]
        public string? TELEFONE { get; set; }

        [Display(Name = "Data de Nascimento")]
        public string? DATANASC { get; set; }
    }
}
