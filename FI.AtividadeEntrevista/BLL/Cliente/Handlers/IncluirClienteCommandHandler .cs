using FI.AtividadeEntrevista.BLL.Cliente.Commands;
using FI.AtividadeEntrevista.BLL.Cliente.interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL.Cliente.Handlers 
{
    public class IncluirClienteCommandHandler : IRequestHandler<IncluirClienteCommand, long>
    {
        private readonly IClienteService _clienteService;

        public IncluirClienteCommandHandler(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public async Task<long> Handle(IncluirClienteCommand request, CancellationToken cancellationToken)
        {
            if (_clienteService.VerificarExistencia(request.CPF))
            {
                throw new InvalidOperationException("Cliente já cadastrado.");
            }

            var cliente = new DML.Cliente
            {
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

            return await Task.Run(() => _clienteService.Incluir(cliente));
        }
    }
}