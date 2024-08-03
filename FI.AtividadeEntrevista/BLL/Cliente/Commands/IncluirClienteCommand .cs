using MediatR;

namespace FI.AtividadeEntrevista.BLL.Cliente.Commands
{
    public class IncluirClienteCommand : IRequest<long>
    {
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string Email { get; set; }
        public string Estado { get; set; }
        public string Logradouro { get; set; }
        public string Nacionalidade { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }

        public IncluirClienteCommand(string cep, string cidade, string email, string estado, string logradouro,
                                     string nacionalidade, string nome, string sobrenome, string telefone, string cpf)
        {
            CEP = cep;
            Cidade = cidade;
            Email = email;
            Estado = estado;
            Logradouro = logradouro;
            Nacionalidade = nacionalidade;
            Nome = nome;
            Sobrenome = sobrenome;
            Telefone = telefone;
            CPF = cpf;
        }
    }
}