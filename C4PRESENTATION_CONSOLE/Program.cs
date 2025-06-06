using C4PRESENTATION_CONSOLE.Dapper;
using C4PRESENTATION_CONSOLE.DTOs;
using C4PRESENTATION_CONSOLE.Interfaces.IRepositories;
using C4PRESENTATION_CONSOLE.Interfaces.IServices;
using C4PRESENTATION_CONSOLE.Mapster;
using C4PRESENTATION_CONSOLE.Repositories;
using C4PRESENTATION_CONSOLE.Services;
using C4PRESENTATION_CONSOLE.SQLite;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using C4PRESENTATION_CONSOLE.FluentValidation;

#region CONFIGURAÇÃO

var serviceCollection = new ServiceCollection();

serviceCollection.AddScoped<SQLiteContext>();
serviceCollection.AddScoped<SQLiteConfig>();
serviceCollection.AddScoped<DapperContext>();
serviceCollection.AddTransient<IClienteRepository, ClienteRepository>();
serviceCollection.AddTransient<IClienteService, ClienteService>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var _serviceSQLite = serviceProvider.GetService<SQLiteConfig>();
var _service = serviceProvider.GetService<IClienteService>();

#endregion

#region MENU

if (_serviceSQLite != null)
{
    try
    {
        _serviceSQLite.Iniciar();
    }
    catch (Exception ex)
    {
        throw new Exception($"Erro SQLite: {ex.Message}");
    }
}

Console.WriteLine("fcouto1331 - P2025_FluentValidation\n");
if (_service != null)
{
    try
    {
        bool cond = true;
        while (cond)
        {
            Console.Clear();

            Console.WriteLine("1 - Listar");
            Console.WriteLine("2 - Criar");
            Console.WriteLine("3 - Alterar");
            Console.WriteLine("4 - Excluir");
            Console.WriteLine("0 - Sair");
            Console.Write("Menu: ");

            var menu = Console.ReadLine();
            switch (menu)
            {
                case "1":
                    ClienteApp.Listar(_service);
                    break;
                case "2":
                    ClienteApp.Criar(_service);
                    break;
                case "3":
                    ClienteApp.Alterar(_service);
                    break;
                case "4":
                    ClienteApp.Excluir(_service);
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    throw new Exception("Opção inválida");
            }

            Console.WriteLine("\n1 - Menu");
            Console.WriteLine("0 - Sair");
            Console.Write("SubMenu: ");
            cond = Console.ReadLine() == "1" ? true : false;
        }
    }
    catch (Exception ex)
    {
        throw new Exception($"Erro: {ex.Message}");
    }
}

Environment.Exit(0);

#endregion

#region COMANDOS

public class ClienteApp
{
    public static void Listar(IClienteService _service)
    {
        Console.WriteLine("\nListar");
        List<ClienteDTO> cliente = Mapper.ToClienteDTO(_service.Listar());
        foreach (var item in cliente)
        {
            Console.WriteLine($"Id: {item.Id}, Nome: {item.Nome}, IdGuid: {item.IdGuid}, Telefone: {item.Telefone}, Cidade: {item.Cidade}");
        }
    }

    public static void Criar(IClienteService _service)
    {
        Console.WriteLine("\nCriar");

        Console.Write("Nome: ");
        string? nome = Console.ReadLine();
        Console.Write("Telefone: ");
        string? telefone = Console.ReadLine();
        Console.Write("Cidade: ");
        string? cidade = Console.ReadLine();

        ClienteDTO cliente = new ClienteDTO { Id = 0, Nome = nome, IdGuid = Guid.NewGuid(), Telefone = telefone, Cidade = cidade };

        // INI - FluentValidation
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddValidatorsFromAssemblyContaining<ClienteValidator>(); 
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var _serviceClienteValidator = serviceProvider.GetService<IValidator<ClienteDTO>>(); 
        var validationResult = _serviceClienteValidator?.Validate(cliente);
        if (validationResult != null && !validationResult.IsValid)
        {
            Console.WriteLine($"\nFluentValidation");
            foreach (var error in validationResult.Errors)
            {
                Console.WriteLine($"Validação: {error.ErrorMessage}");
            }
            throw new Exception("FluentValidation");
        }
        // FIM 

        _service.Criar(Mapper.ToCliente(cliente));
        Listar(_service);
    }

    public static void Alterar(IClienteService _service)
    {
        Console.WriteLine("\nAlterar");
        
        Console.Write("Id:");
        int id = Convert.ToInt32(Console.ReadLine());
        ClienteDTO cliente = Mapper.ToClienteDTO(_service.PegarPorId(id));
        Console.WriteLine($"Id: {cliente.Id}, Nome: {cliente.Nome}, IdGuid: {cliente.IdGuid}, Telefone: {cliente.Telefone}, Cidade: {cliente.Cidade}");

        Console.Write("Nome: ");
        string? nome = Console.ReadLine();
        Console.Write("Telefone: ");
        string? telefone = Console.ReadLine();
        Console.Write("Cidade: ");
        string? cidade = Console.ReadLine();

        ClienteDTO cliente2 = new ClienteDTO { Id = cliente.Id, Nome = nome, IdGuid = cliente.IdGuid, Telefone = telefone, Cidade = cidade };

        // INI - FluentValidation
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddValidatorsFromAssemblyContaining<ClienteValidator>();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var _serviceClienteValidator = serviceProvider.GetService<IValidator<ClienteDTO>>();
        var validationResult = _serviceClienteValidator?.Validate(cliente2);
        if (validationResult != null && !validationResult.IsValid)
        {
            Console.WriteLine($"\nFluentValidation");
            foreach (var error in validationResult.Errors)
            {
                Console.WriteLine($"Validação: {error.ErrorMessage}");
            }
            throw new Exception("FluentValidation");
        }
        // FIM 

        _service.Alterar(Mapper.ToCliente(cliente2), cliente.Id);
        Listar(_service);
    }

    public static void Excluir(IClienteService _service)
    {
        Console.WriteLine("\nExcluir");
        Console.Write("Id:");
        int id = Convert.ToInt32(Console.ReadLine());
        _service.Excluir(id);
        Listar(_service);
    }
}

#endregion
