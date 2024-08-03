using MediatR;


namespace FI.AtividadeEntrevista.BLL.Beneficiarios.Commands 
{
    public class ExcluirBeneficiarioCommand : IRequest<bool>
    {
        public long Id { get; set; }

        public ExcluirBeneficiarioCommand(long id)
        {
            Id = id;
        }
    }
}