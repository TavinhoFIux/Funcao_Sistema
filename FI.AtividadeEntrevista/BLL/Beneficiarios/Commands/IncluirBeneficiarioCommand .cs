using MediatR;


namespace FI.AtividadeEntrevista.BLL.Beneficiarios.Commands
{
    public class IncluirBeneficiarioCommand : IRequest<Result>
    {
        public string ClienteCPF { get; set; }
        public string BeneficiarioCPF { get; set; }
        public string BeneficiarioNome { get; set; }
    }
}