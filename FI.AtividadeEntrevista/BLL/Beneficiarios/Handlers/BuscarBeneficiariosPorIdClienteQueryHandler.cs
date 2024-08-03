using FI.AtividadeEntrevista.BLL.Beneficiario.Interfaces;
using FI.AtividadeEntrevista.BLL.Beneficiarios.Query;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace FI.AtividadeEntrevista.BLL.Beneficiarios.Handlers
{
    public class BuscarBeneficiariosPorIdClienteQueryHandler : IRequestHandler<BuscarBeneficiariosPorIdClienteQuery, List<DML.Beneficiario>>
    {
        private readonly IBeneficiarioService _beneficiarioService;

        public BuscarBeneficiariosPorIdClienteQueryHandler(IBeneficiarioService beneficiarioService)
        {
            _beneficiarioService = beneficiarioService;
        }

        public async Task<List<DML.Beneficiario>> Handle(BuscarBeneficiariosPorIdClienteQuery request, CancellationToken cancellationToken)
        {
            return await Task.Run(() => _beneficiarioService.BuscarBeneficiariosPorIdCliente(request.IdCliente));
        }
    }
}