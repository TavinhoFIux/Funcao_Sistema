using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.DAL.Beneficiarios
{
    public interface IBeneficiarioRepository
    {
        Beneficiario Incluir(Beneficiario beneficiario);
        List<Beneficiario> BuscarBeneficiariosPorIdCliente(long idCliente);
        void Excluir(long id);
        void Alterar(long id, string nome, string cpf);
        bool VerificarCpfCadastrado(long idCliente, string cpf);
    }
}
