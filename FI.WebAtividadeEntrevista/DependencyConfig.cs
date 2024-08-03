using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.BLL.Beneficiario.Interfaces;
using FI.AtividadeEntrevista.BLL.Beneficiarios;
using FI.AtividadeEntrevista.BLL.Cliente.interfaces;
using MediatR;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using System.Linq;
using System;
using System.Reflection;
using System.Web.Mvc;
using System.Collections.Generic;
using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.BLL.Cliente.Commands;
using FI.AtividadeEntrevista.BLL.Cliente.Handlers;
using FI.AtividadeEntrevista.BLL.Beneficiarios.Commands;
using FI.AtividadeEntrevista.BLL.Beneficiarios.Query;
using FI.AtividadeEntrevista.BLL.Beneficiarios.Handlers;
using FI.AtividadeEntrevista.BLL.Cliente.Query;
using FI.AtividadeEntrevista.DAL.Clientes;
using FI.AtividadeEntrevista.DAL.Beneficiarios;

public static class DependencyConfig
{
    public static void RegisterServices()
    {
        var container = new Container();

        // Registrar Mediator
        container.RegisterSingleton<IMediator, Mediator>();


        // Registrar os handlers
        var assemblies = new[] { Assembly.GetExecutingAssembly() };
        var handlerRegistrations = container.GetTypesToRegister(
            typeof(IRequestHandler<,>),
            assemblies,
            new TypesToRegisterOptions
            {
                IncludeGenericTypeDefinitions = true,
                IncludeComposites = false,
            });

        container.Register(typeof(IRequestHandler<,>), handlerRegistrations);
        container.Register<IRequestHandler<IncluirBeneficiarioCommand, Result>, IncluirBeneficiarioCommandHandler>();
        container.Register<IRequestHandler<ExcluirBeneficiarioCommand, bool>, ExcluirBeneficiarioCommandHandler>();
        container.Register<IRequestHandler<AlterarBeneficiarioCommand, bool>, AlterarBeneficiarioCommandHandler>();
        container.Register<IRequestHandler<BuscarBeneficiariosPorIdClienteQuery, List<Beneficiario>>, BuscarBeneficiariosPorIdClienteQueryHandler>();
        container.Register<IRequestHandler<IncluirClienteCommand, long>, IncluirClienteCommandHandler>();
        container.Register<IRequestHandler<AlterarClienteCommand, bool>, AlterarClienteCommandHandler>();
        container.Register<IRequestHandler<BuscarClientesCommand, ClienteListResult>, BuscarClientesCommandHandler>();
        container.Register<IRequestHandler<ConsultarClienteQuery, Cliente>, ConsultarClienteQueryHandler>();


        container.Register(() => (ServiceFactory)(type => container.GetInstance(type)), Lifestyle.Singleton);

        container.Collection.Register(typeof(IPipelineBehavior<,>), Enumerable.Empty<Type>());

        container.Register<IBeneficiarioRepository, BeneficiarioRepository>(Lifestyle.Singleton);
        container.Register<IClienteRepository, ClienteRepository>(Lifestyle.Singleton);
        container.Register<IBeneficiarioService, BeneficiarioService>(Lifestyle.Singleton);
        container.Register<IClienteService, ClienteService>(Lifestyle.Singleton);

        container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

        container.Verify();

        DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
    }
}