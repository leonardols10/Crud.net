using System;
using System.ComponentModel.DataAnnotations;

namespace CamposDealer.Models
{
    public class VendaModel
    {
        [Key]
        public int IdVenda { get; set; }
        [Required(ErrorMessage = "Escolha o nome do cliente")]
        public int IdCliente { get; set; }
        [Required(ErrorMessage = "Escolha o produto ")]
        public int IdProduto { get; set; }
        [Required(ErrorMessage = "Escolha q quantidade  da venda")]
        public int QtdVenda { get; set; }
        [Required(ErrorMessage = "Escolha o valor do item")]
        public decimal VlrUnitarioVenda { get; set; }
        public DateTime DthVenda { get; set; }
        [Required(ErrorMessage = "Erro ao calcular valor total da venda")]
        public decimal VlrTotalVenda { get; set; }

        public ClienteModel Cliente { get; set; }
        public ProdutoModel Produto { get; set; }

    }
}
