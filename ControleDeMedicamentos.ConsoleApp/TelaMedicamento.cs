using ControleDeMedicamentos.ConsoleApp.ControleDeMedicamentos.ConsoleApp;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp
{
    public class TelaMedicamento : TelaBase<Medicamento>, ITelaCrud
    {
        private readonly IRepositorioFornecedor repositorioFornecedor;
        private readonly IRepositorioMedicamento repositorioMedicamento;

        public TelaMedicamento(
            IRepositorioMedicamento repositorioMedicamento,
            IRepositorioFornecedor repositorioFornecedor)
            : base("Medicamento", repositorioMedicamento)
        {
            this.repositorioFornecedor = repositorioFornecedor;
            this.repositorioMedicamento = repositorioMedicamento;
        }

        public override Medicamento ObterDados()
        {
            Console.Write("Digite o nome: ");
            string nome = Console.ReadLine();

            Console.Write("Digite a descrição: ");
            string descricao = Console.ReadLine();

            Console.Write("Digite a quantidade: ");
            int quantidade = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\nSelecione um fornecedor:");
            var fornecedores = repositorioFornecedor.SelecionarRegistros();

            foreach (var fornecedor in fornecedores)
            {
                Console.WriteLine($"ID: {fornecedor.Id} | Nome: {fornecedor.Nome}");
            }

            Console.Write("\nDigite o ID do fornecedor: ");
            int idFornecedor = Convert.ToInt32(Console.ReadLine());

            Fornecedor fornecedorSelecionado = repositorioFornecedor.SelecionarRegistroPorId(idFornecedor);

            return new Medicamento(nome, descricao, quantidade, fornecedorSelecionado);
        }

        protected override void ExibirCabecalhoTabela()
        {
            Console.WriteLine("{0,-5} | {1,-30} | {2,-10} | {3,-15} | {4,-20}",
                "ID", "Nome", "Quantidade", "Status", "Fornecedor");
        }

        protected override void ExibirLinhaTabela(Medicamento medicamento)
        {
            ConsoleColor corStatus = medicamento.Quantidade < 20 ? ConsoleColor.Red : ConsoleColor.Green;

            Console.Write("{0,-5} | {1,-30} | {2,-20} | ",
                medicamento.Id, medicamento.Nome, medicamento.Quantidade);

            Console.ForegroundColor = corStatus;
            Console.Write(medicamento.ObterStatusEstoque().PadRight(15));
            Console.ResetColor();

            Console.WriteLine("| {0,-15}", medicamento.Fornecedor.Nome);
        }

        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
                ExibirCabecalho();

            Console.WriteLine();

            List<Medicamento> medicamentos = repositorioMedicamento.SelecionarRegistros();

            if (medicamentos.Count == 0)
            {
                Notificador.ExibirMensagem("Nenhum medicamento cadastrado.", ConsoleColor.DarkYellow);
                return;
            }

            ExibirCabecalhoTabela();

            foreach (Medicamento medicamento in medicamentos)
            {
                ExibirLinhaTabela(medicamento);
            }

            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}