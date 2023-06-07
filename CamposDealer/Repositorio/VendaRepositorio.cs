using CamposDealer.Data;
using CamposDealer.Models;
using System.Collections.Generic;
using System.Linq;

namespace CamposDealer.Repositorio
{
    public class VendaRepositorio : IVendaRepositorio
    {
        private readonly BancoContext _bancoContext;

        public VendaRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public VendaModel ListarPorId(int id)
        {
            return _bancoContext.Venda.FirstOrDefault(x => x.IdVenda == id);
        }

        public List<VendaModel> BuscarTodos()
        {
            return _bancoContext.Venda.ToList();
        }

        public VendaModel Adicionar(VendaModel venda)
        {
            _bancoContext.Venda.Add(venda);
            _bancoContext.SaveChanges();
            return venda;
        }

        public VendaModel Atualizar(VendaModel venda)
        {
            VendaModel vendaDB = ListarPorId(venda.IdVenda);
            if (vendaDB == null) throw new System.Exception("Erro ao atualizar a Venda");

            vendaDB.IdCliente = venda.IdCliente;
            vendaDB.IdProduto = venda.IdProduto;
            vendaDB.QtdVenda = venda.QtdVenda;
            vendaDB.VlrUnitarioVenda = venda.VlrUnitarioVenda;
            vendaDB.DthVenda = venda.DthVenda;
            vendaDB.VlrTotalVenda = venda.VlrTotalVenda;

            _bancoContext.Venda.Update(vendaDB);
            _bancoContext.SaveChanges();
            return vendaDB;
        }

        public bool Apagar(int id)
        {
            VendaModel vendaDB = ListarPorId(id);
            if (vendaDB == null) throw new System.Exception("Erro ao apagar a Venda");

            _bancoContext.Venda.Remove(vendaDB);
            _bancoContext.SaveChanges();
            return true;
        }
    }
}
