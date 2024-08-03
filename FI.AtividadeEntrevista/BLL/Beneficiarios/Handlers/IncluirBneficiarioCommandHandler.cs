using System.Threading;
using System.Threading.Tasks;
using FI.AtividadeEntrevista.BLL.Beneficiario.Interfaces;
using FI.AtividadeEntrevista.BLL.Beneficiarios.Commands;
using FI.AtividadeEntrevista.BLL.Cliente.interfaces;
using MediatR;


namespace FI.AtividadeEntrevista.BLL.Beneficiarios.Handlers
{
    public class IncluirBeneficiarioCommandHandler : IRequestHandler<IncluirBeneficiarioCommand, Result>
    {
        private readonly IBeneficiarioService _beneficiarioService;
        private readonly IClienteService _clienteService;

        public IncluirBeneficiarioCommandHandler(IBeneficiarioService beneficiarioService, IClienteService clienteService)
        {
            _beneficiarioService = beneficiarioService;
            _clienteService = clienteService;
        }

        public async Task<Result> Handle(IncluirBeneficiarioCommand request, CancellationToken cancellationToken)
        {
            if (request.ClienteCPF == null)
            {
                return Result.FailureResult("Cliente não cadastrado");
            }

            var cliente = _clienteService.BuscarClientePorCpf(request.ClienteCPF);

            if (cliente == null)
            {
                return Result.FailureResult("Cliente não cadastrado");
            }

            if (_beneficiarioService.VerificarCpfCadastrado(cliente.Id, request.BeneficiarioCPF))
            {
                return Result.FailureResult("Beneficiario já cadastrado");
            }

            var beneficiario = new DML.Beneficiario
            {
                IdCliente = cliente.Id,
                Nome = request.BeneficiarioNome,
                CPF = request.BeneficiarioCPF
            };

            var response = await Task.Run(() => _beneficiarioService.Incluir(beneficiario));

            if (response != null)
            {
                return Result.SuccessResult(response);
            }
            else
            {
                return Result.SuccessResult("Erro ao inserir beneficiário");
            }
        }
    }
}