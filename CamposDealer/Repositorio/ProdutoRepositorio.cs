using CamposDealer.Data;
using CamposDealer.Models;
using System.Collections.Generic;
using System.Linq;

namespace CamposDealer.Repositorio
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly BancoContext _bancoContext;

        public ProdutoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public ProdutoModel ListarPorId(int id)
        {
            return _bancoContext.Produto.FirstOrDefault(x => x.Id == id);
        }

        public List<ProdutoModel> BuscarTodos()
        {
            return _bancoContext.Produto.ToList();
        }

        public ProdutoModel Adicionar(ProdutoModel produto)
        {
            _bancoContext.Produto.Add(produto);
            _bancoContext.SaveChanges();
            return produto;
        }

        public ProdutoModel Atualizar(ProdutoModel produto)
        {
            ProdutoModel produtoDB = ListarPorId(produto.Id);
            if (produtoDB == null)
            {
                throw new System.Exception("Erro ao atualizar o Produto");
            }

            produtoDB.Descricao = produto.Descricao;
            produtoDB.ValorUnitario = produto.ValorUnitario;

            _bancoContext.Produto.Update(produtoDB);
            _bancoContext.SaveChanges();
            return produtoDB;
        }

        public bool Apagar(int id)
        {
            ProdutoModel produtoDB = ListarPorId(id);
            if (produtoDB == null)
            {
                throw new System.Exception("Erro ao apagar o Produto");
            }

            _bancoContext.Produto.Remove(produtoDB);
            _bancoContext.SaveChanges();
            return true;
        }
    }
}
