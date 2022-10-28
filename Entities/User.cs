﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

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
        public List<Contas> Contas{ get; set; }
        public List<Categorias> Categorias { get; set; }
        public List<Despesas> Despesas { get; set; }
        public List<Receitas> Receitas { get; set; }

    }
}