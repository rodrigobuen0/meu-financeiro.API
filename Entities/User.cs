using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using meu_financeiro.API.Helpers;

namespace meu_financeiro.API.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string PrimeiroNome { get; set; }
        [Required]
        [MaxLength(50)]
        public string UltimoNome { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [JsonIgnore]
        public string HashSenha { get; set; }
        [JsonConverter(typeof(Utils.DateOnlyJsonConverter))]
        public DateOnly? DataNascimento { get; set; }
        [MaxLength(13)]
        public string? Telefone { get; set; }
        [MaxLength(14)]
        public string? Cpf { get; set; }
        //[MaxLength(8)]
        //public string? Cep { get; set; }
        //[MaxLength(50)]
        //public string? Cidade { get; set; }
        //[MaxLength(2)]
        //public string? Estado { get; set; }
        public Sexo? Sexo { get; set; }
        

        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }
        [JsonIgnore]
        public List<Contas> Contas{ get; set; }
        [JsonIgnore]
        public List<Categorias> Categorias { get; set; }
        [JsonIgnore]
        public List<Despesas> Despesas { get; set; }
        [JsonIgnore]
        public List<Receitas> Receitas { get; set; }

    }
}
