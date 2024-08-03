using System.Collections.Generic;


namespace FI.AtividadeEntrevista.BLL.Beneficiario.Interfaces
{
    public interface IBeneficiarioService
    {
        DML.Beneficiario Incluir(DML.Beneficiario beneficiario);

        List<DML.Beneficiario> BuscarBeneficiariosPorIdCliente(long idCliente);

        void Excluir(long id);

        void Alterar(long id, string nome, string cpf);

        bool VerificarCpfCadastrado(long idCliente, string cpf);
    }
}
