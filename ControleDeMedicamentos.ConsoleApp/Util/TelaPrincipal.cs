using ControleDeMedicamentos.ConsoleApp;
using ControleDeMedicamentos.ConsoleApp.ControleDeMedicamentos.ConsoleApp;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricao;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;

namespace GestaoDeEquipamentos.ConsoleApp.Util;

public class TelaPrincipal
{
    private char opcaoPrincipal;
    private ContextoDados contexto;
    private TelaFornecedor telaFornecedor;
    private TelaMedicamento telaMedicamento;
    private TelaPrescricao telaPrescricao;
    private readonly TelaRecomendacao _telaRecomendacao;


    public TelaPrincipal()
    {
        this.contexto = new ContextoDados(true);

        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);
        IRepositorioMedicamento repositorioMedicamento = new RepositorioMedicamentoEmArquivo(contexto);
        IRepositorioPrescricao repositorioPrescricao = new RepositorioPrescricaoEmArquivo(contexto);

        telaFornecedor = new TelaFornecedor(repositorioFornecedor);
        telaMedicamento = new TelaMedicamento(repositorioMedicamento, repositorioFornecedor);
        telaPrescricao = new TelaPrescricao(repositorioPrescricao, repositorioMedicamento);

        RecomendacaoMedicamentoService recomendacaoService = new RecomendacaoMedicamentoService(contexto);

        _telaRecomendacao = new TelaRecomendacao(recomendacaoService);
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
        Console.WriteLine("4 - Sistema de Recomendação");

        Console.WriteLine("S - Sair");

        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        opcaoPrincipal = Console.ReadLine()[0];
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
            ExecutarMenuRecomendacao();
            return null;

}
    private void ExecutarMenuRecomendacao()
    {
        char opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("=== SISTEMA DE RECOMENDAÇÃO ===");
            Console.WriteLine("1 - Treinar modelo com medicamentos atuais");
            Console.WriteLine("2 - Recomendar medicamento para sintomas");
            Console.WriteLine("3 - Verificar disponibilidade de medicamento");
            Console.WriteLine("4 - Voltar ao menu principal");
            Console.Write("\nEscolha uma opção: ");

            opcao = Console.ReadKey().KeyChar;
            Console.WriteLine();

            switch (opcao)
            {
                case '1':
                    _telaRecomendacao.Treinar(); 
                    break;
                case '2':
                    _telaRecomendacao.Recomendacao(); 
                    break;
                case '3':
                    Console.WriteLine("Funcionalidade de Verificar Disponibilidade ainda não implementada.");
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            if (opcao != '4')
            {
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }

        } while (opcao != '4');
    }
}