using meu_financeiro.API.Helpers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace meu_financeiro.API.Entities
{
    public class ReajustesSaldoContas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Precision(7, 2)]
        public decimal Valor { get; set; }
        [JsonConverter(typeof(Utils.DateOnlyJsonConverter))]
        public DateOnly DataTransacao { get; set; }
        public Guid UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
    }
}
