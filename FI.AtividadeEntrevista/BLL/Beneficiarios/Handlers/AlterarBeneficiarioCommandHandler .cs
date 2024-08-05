using FI.AtividadeEntrevista.BLL.Beneficiario.Interfaces;
using FI.AtividadeEntrevista.BLL.Beneficiarios.Commands;
using FI.AtividadeEntrevista.BLL.Cliente.interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace FI.AtividadeEntrevista.BLL.Beneficiarios.Handlers
{
    public class AlterarBeneficiarioCommandHandler : IRequestHandler<AlterarBeneficiarioCommand, bool>
    {
        private readonly IClienteService _clienteService;
        private readonly IBeneficiarioService _beneficiarioService;

        public AlterarBeneficiarioCommandHandler(IClienteService clienteService, IBeneficiarioService beneficiarioService)
        {
            _clienteService = clienteService;
            _beneficiarioService = beneficiarioService;
        }

        public async Task<bool> Handle(AlterarBeneficiarioCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cliente = _clienteService.BuscarClientePorCpf(request.ClienteCPF);

                if (cliente is null)
                {
                    return false;
                }

                var beneficariosCadastrados = _beneficiarioService.BuscarBeneficiariosPorIdCliente(cliente.Id);
                if (beneficariosCadastrados.Any(b => b.Nome == request.BeneficiarioNome && b.CPF == request.BeneficiarioCPF))
                {
                    return false;
                }

                await Task.Run(() => _beneficiarioService.Alterar(request.Id, request.BeneficiarioNome, request.BeneficiarioCPF));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}