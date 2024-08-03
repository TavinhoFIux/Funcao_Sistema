using FI.AtividadeEntrevista.BLL.Beneficiario.Interfaces;
using FI.AtividadeEntrevista.DAL.Beneficiarios;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL.Beneficiarios
{
    public class BeneficiarioService : IBeneficiarioService
    {
        private readonly IBeneficiarioRepository _beneficiarioRepository;

        public BeneficiarioService(IBeneficiarioRepository beneficiarioRepository)
        {
            _beneficiarioRepository = beneficiarioRepository;
        }

        public DML.Beneficiario Incluir(DML.Beneficiario beneficiario)
        {
            return _beneficiarioRepository.Incluir(beneficiario);
        }

        public List<DML.Beneficiario> BuscarBeneficiariosPorIdCliente(long idCliente)
        {
            return _beneficiarioRepository.BuscarBeneficiariosPorIdCliente(idCliente);
        }

        public void Excluir(long id)
        {
            _beneficiarioRepository.Excluir(id);
        }

        public void Alterar(long id, string nome, string cpf)
        {
            _beneficiarioRepository.Alterar(id, nome, cpf);
        }

        public bool VerificarCpfCadastrado(long idCliente, string cpf)
        {
            return _beneficiarioRepository.VerificarCpfCadastrado(idCliente, cpf);
        }

    }
}
