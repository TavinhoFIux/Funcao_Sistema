using MediatR;
using System.Collections.Generic;


namespace FI.AtividadeEntrevista.BLL.Beneficiarios.Query
{
    public class BuscarBeneficiariosPorIdClienteQuery : IRequest<List<DML.Beneficiario>>
    {
        public long IdCliente { get; set; }

        public BuscarBeneficiariosPorIdClienteQuery(long idCliente)
        {
            IdCliente = idCliente;
        }
    }
}
