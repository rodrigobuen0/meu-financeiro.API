using meu_financeiro.API.Helpers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace meu_financeiro.API.Entities
{
    public class Transferencias
    {
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string? Observacao { get; set; }
        [Precision(7, 2)]
        public decimal Valor { get; set; }
        public Guid ContaEntradaId { get; set; }
        public Guid ContaSaidaId { get; set; }
        [JsonConverter(typeof(Utils.DateOnlyJsonConverter))]
        public DateOnly Data { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Contas? ContaEntrada { get; set; }
        public Contas? ContaSaida { get; set; }
    }
}
