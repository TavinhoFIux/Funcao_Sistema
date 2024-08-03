using System.Collections.Generic;

namespace FI.AtividadeEntrevista.DAL.Clientes
{
    public interface IClienteRepository
    {
        long Incluir(DML.Cliente cliente);
        DML.Cliente Consultar(long id);
        bool VerificarExistencia(string cpf);
        List<DML.Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd);
        DML.Cliente BuscarClientePorCpf(string cpf);
        void Alterar(DML.Cliente cliente);
        void Excluir(long id);
    }
}

