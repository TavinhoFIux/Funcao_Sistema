using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using MediatR;
using FI.AtividadeEntrevista.BLL.Cliente.Commands;
using FI.AtividadeEntrevista.BLL.Cliente.Query;
using FI.WebAtividadeEntrevista.Models;
using System.Web.UI.WebControls;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IMediator _mediator;

        public ClienteController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Incluir(ClienteModelIncluir model)
        {
            if (!ModelState.IsValid)
            {
                var erros = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }

            try
            {
                var cliente = new FI.AtividadeEntrevista.DML.Cliente
                {
                    CEP = model.Cliente.CEP,
                    Cidade = model.Cliente.Cidade,
                    Email = model.Cliente.Email,
                    Estado = model.Cliente.Estado,
                    Logradouro = model.Cliente.Logradouro,
                    Nacionalidade = model.Cliente.Nacionalidade,
                    Nome = model.Cliente.Nome,
                    Sobrenome = model.Cliente.Sobrenome,
                    Telefone = model.Cliente.Telefone,
                    CPF = model.Cliente.CPF
                };

                var beneficiarios = model.Beneficiarios?.Select(b => new FI.AtividadeEntrevista.DML.Beneficiario
                {
                    Nome = b.Nome,
                    CPF = b.CPF
                }).ToList() ?? new List<FI.AtividadeEntrevista.DML.Beneficiario>();

                var command = new IncluirClienteCommand(cliente, beneficiarios);

                long clienteId = await _mediator.Send(command);

                return Json(new { success = true, message = "Cadastro efetuado com sucesso", id = clienteId });
            }
            catch (InvalidOperationException ex)
            {
                Response.StatusCode = 400;
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Ocorreu um erro: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Alterar(ClienteModel model)
        {
            if (!ModelState.IsValid)
            {
                var erros = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }

            try
            {
                var command = new AlterarClienteCommand(
                    model.Id,
                    model.CEP,
                    model.Cidade,
                    model.Email,
                    model.Estado,
                    model.Logradouro,
                    model.Nacionalidade,
                    model.Nome,
                    model.Sobrenome,
                    model.Telefone,
                    model.CPF
                );

                bool sucesso = await _mediator.Send(command);

                return Json(new { success = sucesso, message = sucesso ? "Cadastro alterado com sucesso" : "Falha ao alterar cadastro" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Ocorreu um erro: " + ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Alterar(long id)
        {
            var query = new ConsultarClienteQuery(id);
            var cliente = await _mediator.Send(query);

            if (cliente == null)
            {
                return HttpNotFound();
            }

            var model = new ClienteModel
            {
                Id = cliente.Id,
                CEP = cliente.CEP,
                Cidade = cliente.Cidade,
                Email = cliente.Email,
                Estado = cliente.Estado,
                Logradouro = cliente.Logradouro,
                Nacionalidade = cliente.Nacionalidade,
                Nome = cliente.Nome,
                Sobrenome = cliente.Sobrenome,
                Telefone = cliente.Telefone,
                CPF = cliente.CPF
            };

            return View(model);
        }


        [HttpPost]
        public async Task<JsonResult> ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var command = new BuscarClientesCommand(jtStartIndex, jtPageSize, jtSorting);
                var result = await _mediator.Send(command);

                return Json(new { Result = "OK", Records = result.Clientes, TotalRecordCount = result.TotalRecordCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

    }
}