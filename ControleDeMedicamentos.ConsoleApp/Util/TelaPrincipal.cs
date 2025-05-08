using ControleDeMedicamentos.ConsoleApp.ControleDeMedicamentos.ConsoleApp;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricao;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using System.Runtime.CompilerServices;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicao;

namespace GestaoDeEquipamentos.ConsoleApp.Util;

public class TelaPrincipal
{
    private char opcaoPrincipal;
    private ContextoDados contexto;
    private TelaFornecedor telaFornecedor;
    private TelaMedicamento telaMedicamento;
    private TelaPrescricao telaPrescricao;
    private TelaFuncionario telaFuncionario;
    private TelaPaciente telaPaciente;
    private TelaRequisicaoSaida telaRequisicaoSaida;


    public TelaPrincipal()
    {
        this.contexto = new ContextoDados(true);

        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);
        IRepositorioMedicamento repositorioMedicamento = new RepositorioMedicamentoEmArquivo(contexto);
        IRepositorioPrescricao repositorioPrescricao = new RepositorioPrescricaoEmArquivo(contexto);
        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);
        IRepositorio<RequisicaoSaida> repositorioRequisicaoSaida = new RepositorioRequisicaoSaidaEmMemoria(contexto);

        telaFornecedor = new TelaFornecedor(repositorioFornecedor);
        telaMedicamento = new TelaMedicamento(repositorioMedicamento, repositorioFornecedor);
        telaPrescricao = new TelaPrescricao(repositorioPrescricao, repositorioMedicamento);
        telaPaciente = new TelaPaciente(repositorioPaciente, (RepositorioPrescricaoEmArquivo)repositorioPrescricao);
        telaRequisicaoSaida = new TelaRequisicaoSaida(contexto, repositorioPaciente, "Requisição de Saída", repositorioRequisicaoSaida);

        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contexto);
        telaFuncionario = new TelaFuncionario(repositorioFuncionario);

    }

    public void ApresentarMenuPrincipal()
    {
        Console.Clear();

        Console.WriteLine("----------------------------------------");
        Console.WriteLine("|        Gestão de Medicamentos        |");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        Console.WriteLine("1 - Controle de Fornecedor");
        Console.WriteLine("2 - Controle de Medicamentos");
        Console.WriteLine("3 - Prescrições Médicas");
        Console.WriteLine("4 - Controle de Funcionários");
        Console.WriteLine("5 - Controle de Pacientes");
        Console.WriteLine("6 - Requisições de Saída");
        Console.WriteLine("S - Sair");

        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        opcaoPrincipal = Console.ReadLine()![0];
    }

    public ITelaCrud ObterTela()
    {
        if (opcaoPrincipal == '1')
            return telaFornecedor;
        else if (opcaoPrincipal == '2')
            return telaMedicamento;
        else if (opcaoPrincipal == '3')
            return telaPrescricao;
        else if (opcaoPrincipal == '4')
            return telaFuncionario;
        else if (opcaoPrincipal == '5')
            return telaPaciente;
        else if (opcaoPrincipal == '6')
            return telaRequisicaoSaida;

        return null!;
    }
}

