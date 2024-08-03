using FI.AtividadeEntrevista.BLL.Cliente.interfaces;
using FI.AtividadeEntrevista.BLL.Cliente.Query;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL.Cliente.Handlers
{
    public class ConsultarClienteQueryHandler : IRequestHandler<ConsultarClienteQuery, DML.Cliente>
    {
        private readonly IClienteService _clienteService;

        public ConsultarClienteQueryHandler(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public Task<DML.Cliente> Handle(ConsultarClienteQuery request, CancellationToken cancellationToken)
        {
            var cliente = _clienteService.Consultar(request.Id);

            return Task.FromResult(cliente);
        }
    }
}
