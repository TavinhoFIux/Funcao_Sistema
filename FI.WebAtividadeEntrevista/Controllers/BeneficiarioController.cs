using FI.AtividadeEntrevista.BLL.Beneficiarios.Commands;
using FI.AtividadeEntrevista.BLL.Beneficiarios.Query;
using FI.AtividadeEntrevista.BLL.Cliente.Query;
using FI.WebAtividadeEntrevista.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebAtividadeEntrevista.Models;

namespace FI.WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        private readonly IMediator _mediator;

        public BeneficiarioController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<ActionResult> IncluirBeneficiario(BeneficiarioModel model)
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

            if (model.ClienteCPF == null)
            {
                return new HttpStatusCodeResult(400, "Cliente não cadastrado");
            }

            var command = new IncluirBeneficiarioCommand
            {
                ClienteCPF = model.ClienteCPF,
                BeneficiarioNome = model.Nome,
                BeneficiarioCPF = model.CPF
            };

            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Json(new { success = true, beneficiario = result.Data });
            }
            else
            {
                return new HttpStatusCodeResult(400, result.Message);
            }
        }

        [HttpGet]
        public async Task<JsonResult> BuscarBeneficiariosPorIdCliente(long idCliente)
        {
            try
            {
                var query = new BuscarBeneficiariosPorIdClienteQuery(idCliente);
                var beneficiarios = await _mediator.Send(query);

                if (beneficiarios != null && beneficiarios.Count > 0)
                {
                    return Json(new { success = true, data = beneficiarios }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Nenhum beneficiário encontrado para o IDCLIENTE fornecido." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Ocorreu um erro: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public async Task<JsonResult> ExcluirBeneficiario(long id)
        {
            try
            {
                var command = new ExcluirBeneficiarioCommand(id);
                var result = await _mediator.Send(command);

                if (result)
                {
                    return Json(new { success = true, message = "Beneficiário excluído com sucesso." });
                }
                else
                {
                    return Json(new { success = false, message = "Ocorreu um erro ao excluir o beneficiário." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Ocorreu um erro: " + ex.Message });
            }
        }


        [HttpPost]
        public async Task<JsonResult> AlterarBeneficiario(BeneficiarioModel model)
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
                var command = new AlterarBeneficiarioCommand(model.Id, model.Nome, model.CPF, model.ClienteCPF);
                var success = await _mediator.Send(command);

                if (success)
                {
                    return Json(new { success = true, message = "Beneficiário atualizado com sucesso." });
                }
                else
                {
                    Response.StatusCode = 400;
                    return Json(new { success = false, message = "Cliente não cadastrado ou beneficiário já cadastrado." });
                }
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
    }
}