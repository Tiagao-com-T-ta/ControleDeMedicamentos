using ControleDeMedicamentos.ConsoleApp.ControleDeMedicamentos.ConsoleApp;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamento
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

            List<string> sintomasTratados = new List<string>();
            while (true)
            {
                Console.Write("Digite um sintoma tratado pelo medicamento (ou 'fim' para terminar): ");
                string sintoma = Console.ReadLine();

                if (sintoma.ToLower() == "fim")
                    break;

                if (!string.IsNullOrWhiteSpace(sintoma))
                {
                    sintomasTratados.Add(sintoma.Trim());
                }
                else
                {
                    Console.WriteLine("Sintoma não pode ser vazio.");
                }
            }

            return new Medicamento(nome, descricao, quantidade, fornecedorSelecionado, sintomasTratados);
        }


        protected override void ExibirCabecalhoTabela()
        {
            Console.WriteLine("{0,-5} | {1,-30} | {2,-10} | {3,-15} | {4,-20}",
                "ID", "Nome", "Quantidade", "Status", "Fornecedor");
        }

        protected override void ExibirLinhaTabela(Medicamento medicamento)
        {
            ConsoleColor corStatus = medicamento.Quantidade < 20 ? ConsoleColor.Red : ConsoleColor.Green;

            Console.Write("{0,-5} | {1,-30} | {2,-10} | ",
                medicamento.Id, medicamento.Nome, medicamento.Quantidade);

            Console.ForegroundColor = corStatus;
            Console.Write(medicamento.ObterStatusEstoque().PadRight(15));
            Console.ResetColor();

            if (medicamento.Fornecedor != null)
            {
                Console.WriteLine("| {0,-20}", medicamento.Fornecedor.Nome);
            }
            else
            {
                Console.WriteLine("| Fornecedor não encontrado");
            }
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