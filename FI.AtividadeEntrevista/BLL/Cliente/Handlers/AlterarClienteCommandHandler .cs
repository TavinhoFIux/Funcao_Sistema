using FI.AtividadeEntrevista.BLL.Cliente.Commands;
using FI.AtividadeEntrevista.BLL.Cliente.interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL.Cliente.Handlers
{
    public class AlterarClienteCommandHandler : IRequestHandler<AlterarClienteCommand, bool>
    {
        private readonly IClienteService _clienteService;

        public AlterarClienteCommandHandler(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public async Task<bool> Handle(AlterarClienteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cliente = new DML.Cliente
                {
                    Id = request.Id,
                    CEP = request.CEP,
                    Cidade = request.Cidade,
                    Email = request.Email,
                    Estado = request.Estado,
                    Logradouro = request.Logradouro,
                    Nacionalidade = request.Nacionalidade,
                    Nome = request.Nome,
                    Sobrenome = request.Sobrenome,
                    Telefone = request.Telefone,
                    CPF = request.CPF
                };

                await Task.Run(() => _clienteService.Alterar(cliente));
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }
    }
}
