using CamposDealer.Models;
using CamposDealer.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CamposDealer.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;

        public ProdutoController(IProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        public IActionResult Index()
        {
            List<ProdutoModel> produtos = _produtoRepositorio.BuscarTodos();
            return View(produtos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(ProdutoModel produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _produtoRepositorio.Adicionar(produto);
                    TempData["MensagemSucesso"] = "Produto cadastrado com sucesso";
                    return RedirectToAction("Index");
                }
                return View(produto);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao cadastrar produto, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Editar(int id)
        {
            ProdutoModel produto = _produtoRepositorio.ListarPorId(id);
            return View(produto);
        }

        [HttpPost]
        public IActionResult Editar(ProdutoModel produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _produtoRepositorio.Atualizar(produto);
                    TempData["MensagemSucesso"] = "Produto alterado com sucesso";
                    return RedirectToAction("Index");
                }
                return View(produto);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao atualizar produto, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            ProdutoModel produto = _produtoRepositorio.ListarPorId(id);
            return View(produto);
        }

        [HttpPost]
        public IActionResult Apagar(int id)
        {
            try
            {
                _produtoRepositorio.Apagar(id);
                TempData["MensagemSucesso"] = "Produto apagado com sucesso";
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao apagar produto, detalhe do erro: {erro.Message}";
            }
            return RedirectToAction("Index");
        }
    }
}
