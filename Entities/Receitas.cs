using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Globalization;
using System.Text.Json;

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
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly DataTransacao { get; set; }
        public Guid CategoriaId { get; set; }
        public Categorias? Categoria { get; set; }
        public Guid ContaId { get; set; }
        public Contas? Conta { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }

    }
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private const string Format = "dd/MM/yyyy";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateOnly.ParseExact(reader.GetString()!, Format, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
        }
    }
}
