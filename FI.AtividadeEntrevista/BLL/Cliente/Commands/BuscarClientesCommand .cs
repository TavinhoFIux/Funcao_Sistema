using MediatR;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL.Cliente.Commands
{
    public class BuscarClientesCommand : IRequest<ClienteListResult>
    {
        public int StartIndex { get; set; }
        public int PageSize { get; set; }
        public string Sorting { get; set; }

        public BuscarClientesCommand(int startIndex, int pageSize, string sorting)
        {
            StartIndex = startIndex;
            PageSize = pageSize;
            Sorting = sorting;
        }
    }

    public class ClienteListResult
    {
        public List<DML.Cliente> Clientes { get; set; }
        public int TotalRecordCount { get; set; }

        public ClienteListResult(List<DML.Cliente> clientes, int totalRecordCount)
        {
            Clientes = clientes;
            TotalRecordCount = totalRecordCount;
        }
    }
}
