using FI.AtividadeEntrevista.BLL.Cliente.Commands;
using FI.AtividadeEntrevista.BLL.Cliente.interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL.Cliente.Handlers
{
    public class BuscarClientesCommandHandler : IRequestHandler<BuscarClientesCommand, ClienteListResult>
    {
        private readonly IClienteService _clienteService;

        public BuscarClientesCommandHandler(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public Task<ClienteListResult> Handle(BuscarClientesCommand request, CancellationToken cancellationToken)
        {
            string campo = string.Empty;
            string crescente = string.Empty;
            string[] array = request.Sorting.Split(' ');

            if (array.Length > 0)
                campo = array[0];

            if (array.Length > 1)
                crescente = array[1];

            int qtd;
            List<DML.Cliente> clientes = _clienteService.Pesquisa(request.StartIndex, request.PageSize, campo,
                crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

            return Task.FromResult(new ClienteListResult(clientes, qtd));
        }
    }
}
