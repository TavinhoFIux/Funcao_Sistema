using System.Collections.Generic;


namespace FI.AtividadeEntrevista.BLL.Cliente.interfaces
{
    public interface IClienteService
    {

        long Incluir(DML.Cliente cliente);


        void Alterar(DML.Cliente cliente);

        DML.Cliente Consultar(long id);

        void Excluir(long id);

        DML.Cliente BuscarClientePorCpf(string cpf);

        List<DML.Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd);

        bool VerificarExistencia(string CPF);
    }
}
