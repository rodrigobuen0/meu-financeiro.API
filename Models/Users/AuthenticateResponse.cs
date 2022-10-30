using meu_financeiro.API.Entities;
using System.Text.Json.Serialization;

namespace meu_financeiro.API.Models.Users
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string Usuario { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }

        public AuthenticateResponse(User user, string jwtToken, string refreshToken)
        {
            Id = user.Id;
            PrimeiroNome = user.PrimeiroNome;
            UltimoNome = user.UltimoNome;
            Usuario = user.Email;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}
