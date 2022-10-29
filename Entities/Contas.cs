using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace meu_financeiro.API.Entities
{
    public class Contas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string Descricao { get; set; }
        public TipoConta TipoConta { get; set; }
        public decimal SaldoAtual { get; set; }
        public Guid UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }

    }
}
