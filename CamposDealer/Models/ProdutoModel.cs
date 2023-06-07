using System.ComponentModel.DataAnnotations;

namespace CamposDealer.Models
{
    public class ProdutoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite a descrição do produto")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Digite o valor do produto")]
        public float ValorUnitario { get; set; }

    }
}
