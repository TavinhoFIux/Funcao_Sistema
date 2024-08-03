using MediatR;

namespace FI.AtividadeEntrevista.BLL.Beneficiarios.Commands
{
    public class AlterarBeneficiarioCommand : IRequest<bool>
    {
        public long Id { get; set; }
        public string BeneficiarioNome { get; set; }
        public string BeneficiarioCPF { get; set; }
        public string ClienteCPF { get; set; }

        public AlterarBeneficiarioCommand(long id, string beneficiarioNome, string beneficiarioCPF, string clienteCPF)
        {
            Id = id;
            BeneficiarioNome = beneficiarioNome;
            BeneficiarioCPF = beneficiarioCPF;
            ClienteCPF = clienteCPF;
        }
    }
}