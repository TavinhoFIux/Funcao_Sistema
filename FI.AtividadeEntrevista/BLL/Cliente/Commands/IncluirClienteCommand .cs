using MediatR;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL.Cliente.Commands
{
    public class IncluirClienteCommand : IRequest<long>
    {
        public DML.Cliente Cliente { get; set; }

        public List<DML.Beneficiario> Beneficiarios { get; set; }

        public IncluirClienteCommand(DML.Cliente cliente, List<DML.Beneficiario> beneficiarios)
        {
            Cliente = cliente;
            Beneficiarios = beneficiarios ?? new List<DML.Beneficiario>();
        }
    }
}