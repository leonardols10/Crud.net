using CamposDealer.Models;
using CamposDealer.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CamposDealer.Controllers
{
    public class VendasController : Controller
    {
        private readonly IVendaRepositorio _vendaRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IProdutoRepositorio _produtoRepositorio;

        public VendasController(IVendaRepositorio vendaRepositorio, IClienteRepositorio clienteRepositorio, IProdutoRepositorio produtoRepositorio)
        {
            _vendaRepositorio = vendaRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _produtoRepositorio = produtoRepositorio;
        }

        public IActionResult Index()
        {
            List<VendaModel> vendas = _vendaRepositorio.BuscarTodos();

            foreach (var venda in vendas)
            {
                venda.Cliente = _clienteRepositorio.ListarPorId(venda.IdCliente);
                venda.Produto = _produtoRepositorio.ListarPorId(venda.IdProduto);
            }

            return View(vendas);
        }


        public IActionResult Criar()
        {
            ViewBag.Clientes = _clienteRepositorio.BuscarTodos();
            ViewBag.Produtos = _produtoRepositorio.BuscarTodos();
            return View();
        }

        public IActionResult Editar(int id)
        {
            ViewBag.Clientes = _clienteRepositorio.BuscarTodos();
            ViewBag.Produtos = _produtoRepositorio.BuscarTodos(); 
            VendaModel venda = _vendaRepositorio.ListarPorId(id);
            return View(venda);
        }

        public IActionResult DeleteConfirmation(int id)
        {
            VendaModel venda = _vendaRepositorio.ListarPorId(id);
            return View(venda);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _vendaRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Cliente apagado com sucesso";

                }
                else
                {
                    TempData["MensagemErro"] = "Não conseguimos apagar o cliente ";

                }
                return RedirectToAction("Index");

            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao atualizar cliente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(VendaModel venda)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _vendaRepositorio.Adicionar(venda);

                    ProdutoModel produto = _produtoRepositorio.ListarPorId(venda.IdProduto);

                    venda.VlrUnitarioVenda = (decimal)produto.ValorUnitario;

                    venda.VlrTotalVenda = (decimal)(venda.QtdVenda * (float)produto.ValorUnitario);

                    _vendaRepositorio.Atualizar(venda);

                    TempData["MensagemSucesso"] = "Venda cadastrada com sucesso";
                    return RedirectToAction("Index");
                }

                ViewBag.Clientes = _clienteRepositorio.BuscarTodos();
                ViewBag.Produtos = _produtoRepositorio.BuscarTodos();
                return View(venda);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao cadastrar venda, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }





        [HttpPost]
        public IActionResult Alterar(VendaModel venda)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProdutoModel produto = _produtoRepositorio.ListarPorId(venda.IdProduto);

                    venda.VlrUnitarioVenda = (decimal)produto.ValorUnitario;

                    venda.QtdVenda = venda.QtdVenda;

                    venda.VlrTotalVenda = (decimal)(venda.QtdVenda * produto.ValorUnitario);

                    _vendaRepositorio.Atualizar(venda);
                    TempData["MensagemSucesso"] = "Venda alterada com sucesso";
                    return RedirectToAction("Index");
                }

                ViewBag.Clientes = _clienteRepositorio.BuscarTodos();
                ViewBag.Produtos = _produtoRepositorio.BuscarTodos();
                return View("Editar", venda);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao atualizar venda, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }


        public IActionResult ApagarConfirmacao(int id)
        {
            VendaModel Vendas = _vendaRepositorio.ListarPorId(id);
            return View(Vendas);
        }
    }
}
