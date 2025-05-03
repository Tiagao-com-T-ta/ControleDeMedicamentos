using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp
{
    class TelaFornecedor : TelaBase<Fornecedor>, ITelaCrud
    {
        public TelaFornecedor(IRepositorioFornecedor repositorio) : base("Fornecedor", repositorio)
        {
        }

        public override Fornecedor ObterDados()
        {
            Console.Write("Digite o nome: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o Telefone: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite o CNPJ: ");
            string cnpj = Console.ReadLine();

            Fornecedor fornecedor = new Fornecedor(nome, telefone, cnpj);
            return fornecedor;

        }

        protected override void ExibirCabecalhoTabela()
        {
            Console.WriteLine("{0, 10} | {1, 30} | {2, 20} | {3, 20} ", "Id", "Nome", "Telefone", "CNPJ");
        }

        protected override void ExibirLinhaTabela(Fornecedor fornecedor)
        {
            Console.WriteLine("{0, 10} | {1, 30} | {2, 20} | {3, 20} ", fornecedor.Id, fornecedor.Nome, fornecedor.Telefone, fornecedor.CNPJ);
        }
        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
                ExibirCabecalho();

            Console.WriteLine();

            List<Fornecedor> fornecedores = repositorio.SelecionarRegistros();

            if (fornecedores.Count == 0)
            {
                Notificador.ExibirMensagem("Nenhum fornecedor cadastrado.", ConsoleColor.DarkYellow);
                return;
            }

            ExibirCabecalhoTabela();

            foreach (Fornecedor fornecedor in fornecedores)
            {
                ExibirLinhaTabela(fornecedor);
            }

            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
