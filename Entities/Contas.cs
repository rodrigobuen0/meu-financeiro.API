using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
        public User User { get; set; }

    }
}
