using CamposDealer.Data;
using CamposDealer.Models;
using System.Collections.Generic;
using System.Linq;

namespace CamposDealer.Repositorio
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly BancoContext _bancoContext;

        public ClienteRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public ClienteModel ListarPorId(int id)
        {
            return _bancoContext.Cliente.FirstOrDefault(x => x.Id == id);
        }

        public List<ClienteModel> BuscarTodos()
        {
            return _bancoContext.Cliente.ToList();
        }
        public ClienteModel Adicionar(ClienteModel cliente)
        {
            _bancoContext.Cliente.Add(cliente);
            _bancoContext.SaveChanges();
            return cliente;
        }

        public ClienteModel Atualizar(ClienteModel cliente)
        {
            ClienteModel clienteDB = ListarPorId(cliente.Id);
            if (clienteDB == null) throw new System.Exception("Erro ao atualizar o Cliente");
            clienteDB.Nome = cliente.Nome;
            clienteDB.Cidade = cliente.Cidade;

            _bancoContext.Cliente.Update(clienteDB);
            _bancoContext.SaveChanges();
            return clienteDB;

        }

        public bool Apagar(int id)
        {
            ClienteModel clienteDB = ListarPorId(id);
            if (clienteDB == null) throw new System.Exception("Erro ao apagar o Cliente");
            _bancoContext.Cliente.Remove(clienteDB);
            _bancoContext.SaveChanges();
            return true;
        }
    }
}
