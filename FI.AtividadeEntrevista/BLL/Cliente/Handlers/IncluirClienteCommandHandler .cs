using FI.AtividadeEntrevista.BLL.Beneficiario.Interfaces;
using FI.AtividadeEntrevista.BLL.Cliente.Commands;
using FI.AtividadeEntrevista.BLL.Cliente.interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL.Cliente.Handlers 
{
    public class IncluirClienteCommandHandler : IRequestHandler<IncluirClienteCommand, long>
    {
        private readonly IClienteService _clienteService;

        private readonly IBeneficiarioService _beneficiarioService;

        public IncluirClienteCommandHandler(IClienteService clienteService, IBeneficiarioService beneficiarioService)
        {
            _clienteService = clienteService;
            _beneficiarioService = beneficiarioService;
        }

        public async Task<long> Handle(IncluirClienteCommand request, CancellationToken cancellationToken)
        {
            if (_clienteService.VerificarExistencia(request.Cliente.CPF))
            {
                throw new InvalidOperationException("Cliente já cadastrado.");
            }

            long clienteId = _clienteService.Incluir(request.Cliente);

            if (request.Beneficiarios != null && request.Beneficiarios.Any())
            {
                foreach (var beneficiario in request.Beneficiarios)
                {
                    beneficiario.IdCliente = clienteId;

                    if (_beneficiarioService.VerificarCpfCadastrado(clienteId, beneficiario.CPF))
                    {
                        throw new InvalidOperationException("Beneficario já cadastrado.");
                    }

                    _beneficiarioService.Incluir(beneficiario);
                }
            }

            return await Task.Run(() => clienteId);
        }
    }
}