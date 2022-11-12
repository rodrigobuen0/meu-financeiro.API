﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Globalization;
using System.Text.Json;
using meu_financeiro.API.Helpers;

namespace meu_financeiro.API.Entities
{
    public class Receitas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string? Descricao { get; set; }
        public decimal Valor { get; set; }
        [JsonConverter(typeof(Utils.DateOnlyJsonConverter))]
        public DateOnly DataTransacao { get; set; }
        public Guid CategoriaId { get; set; }
        public Categorias? Categoria { get; set; }
        public Guid ContaId { get; set; }
        public Contas? Conta { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }

    }
}
