using CamposDealer.Models;
using System.Collections.Generic;

namespace CamposDealer.Repositorio
{
    public interface IVendaRepositorio
    {
        VendaModel ListarPorId(int id);
        List<VendaModel> BuscarTodos();
        VendaModel Adicionar(VendaModel venda);
        VendaModel Atualizar(VendaModel venda);
        bool Apagar(int id);
    }
}
