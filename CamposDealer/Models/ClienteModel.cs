using System.ComponentModel.DataAnnotations;

namespace CamposDealer.Models
{
    public class ClienteModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Digite o nome do cliente")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite a cidade do cliente")]
        public string Cidade { get; set; }

    }
}
