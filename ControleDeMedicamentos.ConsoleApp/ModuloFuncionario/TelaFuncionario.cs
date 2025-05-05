using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.Util;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionario
{
    class TelaFuncionario : TelaBase<Funcionario>, ITelaCrud
    {
        public TelaFuncionario(IRepositorioFuncionario repositorio) : base("Funcionario", repositorio)
        {
        }

        public override Funcionario ObterDados()
        {
            Console.WriteLine("Digite o nome do funcionário: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite o telefone do funcionário: ");
            string telefone = Console.ReadLine();

            Console.WriteLine("Digite o CPF do funcionário: ");
            string cpf = Console.ReadLine();

            Funcionario funcionario = new Funcionario(nome, telefone, cpf);

            return funcionario;
        }

        protected override void ExibirCabecalhoTabela()
        {
            Console.WriteLine("{0, 10} | {1, 30} | {2, 20} | {3, 20}",
                                "Id", "Nome", "Telefone", "CPF");
        }

        protected override void ExibirLinhaTabela(Funcionario funcionario)
        {
            Console.WriteLine("{0, 10} | {1, 30} | {2, 20} | {3, 20}",
                                funcionario.Id, funcionario.Nome, funcionario.Telefone, funcionario.CPF);
        }

        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
            ExibirCabecalho();

            Console.WriteLine();

            List<Funcionario> funcionarios = repositorio.SelecionarRegistros();

            if (funcionarios.Count == 0)
            {
                Notificador.ExibirMensagem("Nenhum funcionário encontrado.", ConsoleColor.DarkYellow);

                return;
            }

            ExibirCabecalhoTabela();

            foreach(Funcionario funcionario in funcionarios)
            {
                ExibirLinhaTabela(funcionario);

            }            
            Console.WriteLine();
            Console.ReadLine();

        }


    }
}
