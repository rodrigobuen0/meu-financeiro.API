using System.ComponentModel.DataAnnotations;

namespace meu_financeiro.API.Models.Users
{
    public class AuthenticateRequest
    {
        [Required]
        public string Usuario { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}
