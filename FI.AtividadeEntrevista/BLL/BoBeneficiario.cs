using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Inclui um novo beneficiario 
        /// </summary>
        /// <param name="beneficiario">Objeto de cliente</param>
        public Beneficiario Incluir(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            return cli.Incluir(beneficiario);
        }

        public List<Beneficiario> BuscarBeneficiariosPorIdCliente(long idCliente)
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            return cli.BuscarBeneficiariosPorIdCliente(idCliente);
        }

        public void Excluir(long id)
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            cli.Excluir(id);

        }

        public void Alterar(long id, string nome, string cpf)
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            cli.Alterar(id, nome, cpf);
        }

        public bool VerificarCpfCadastrado(long idCliente, string cpf)
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            return cli.VerificarCpfCadastrado(idCliente, cpf);
        }

    }
}
