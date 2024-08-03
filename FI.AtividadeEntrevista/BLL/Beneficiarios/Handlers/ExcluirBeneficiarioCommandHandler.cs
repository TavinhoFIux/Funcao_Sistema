using FI.AtividadeEntrevista.BLL.Beneficiario.Interfaces;
using FI.AtividadeEntrevista.BLL.Beneficiarios.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class ExcluirBeneficiarioCommandHandler : IRequestHandler<ExcluirBeneficiarioCommand, bool>
{
    private readonly IBeneficiarioService _beneficiarioService;

    public ExcluirBeneficiarioCommandHandler(IBeneficiarioService beneficiarioService)
    {
        _beneficiarioService = beneficiarioService;
    }

    public async Task<bool> Handle(ExcluirBeneficiarioCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await Task.Run(() =>_beneficiarioService.Excluir(request.Id));
            return true;
        }
        catch
        {
            return false;
        }
    }
}