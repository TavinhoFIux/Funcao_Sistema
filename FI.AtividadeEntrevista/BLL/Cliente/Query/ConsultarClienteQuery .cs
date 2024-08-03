using MediatR;


namespace FI.AtividadeEntrevista.BLL.Cliente.Query
{
    public class ConsultarClienteQuery : IRequest<DML.Cliente>
    {
        public long Id { get; set; }

        public ConsultarClienteQuery(long id)
        {
            Id = id;
        }
    }
}
