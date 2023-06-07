    using CamposDealer.Models;
    using CamposDealer.Repositorio;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    namespace CamposDealer.Controllers
    {
        public class ClienteController : Controller
        {
            private readonly IClienteRepositorio _clienteRepositorio;

            public ClienteController(IClienteRepositorio clienteRepositorio)
            {
                _clienteRepositorio = clienteRepositorio;
            }

            public IActionResult Index()
            {
                List<ClienteModel> clientes = _clienteRepositorio.BuscarTodos();
                return View(clientes);
            }

            public IActionResult Criar()
            {
                return View();
            }

            public IActionResult Editar(int id)
            {
            ClienteModel cliente = _clienteRepositorio.ListarPorId(id);
                return View(cliente);
            }

            public IActionResult ApagarConfirmacao(int id)
            {
            ClienteModel cliente = _clienteRepositorio.ListarPorId(id);
            return View(cliente);
            }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _clienteRepositorio.Apagar(id);
                    if(apagado)
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
        public IActionResult Criar(ClienteModel cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _clienteRepositorio.Adicionar(cliente);
                    TempData["MensagemSucesso"] = "Cliente cadastrado com sucesso";
                    return RedirectToAction("Index");
                }
                return View(cliente);

            }
            catch (System.Exception erro ) {
                TempData["MensagemErro"] = $"Erro ao cadastrar cliente, detalhe do erro: {erro.Message}";

                return RedirectToAction("Index");
            }
        }
        public IActionResult Alterar(ClienteModel cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _clienteRepositorio.Atualizar(cliente);
                    TempData["MensagemSucesso"] = "Cliente alterado com sucesso";
                    return RedirectToAction("Index");
                }
                return View("Editar", cliente);

            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao atualizar cliente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");

            }

        }
    }
}
