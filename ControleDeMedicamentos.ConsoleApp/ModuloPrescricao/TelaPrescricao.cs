using ControleDeMedicamentos.ConsoleApp.ControleDeMedicamentos.ConsoleApp;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPrescricao
{
    public class TelaPrescricao : TelaBase<Prescricao>, ITelaCrud
    {
        private readonly IRepositorioPrescricao repositorioPrescricao;
        private readonly IRepositorioMedicamento repositorioMedicamento;

        public TelaPrescricao(
            IRepositorioPrescricao repositorioPrescricao,
            IRepositorioMedicamento repositorioMedicamento)
            : base("Prescrição Médica", repositorioPrescricao)
        {
            this.repositorioPrescricao = repositorioPrescricao;
            this.repositorioMedicamento = repositorioMedicamento;
        }

        public override Prescricao ObterDados()
        {
            Console.Write("Digite o CRM do médico (6 dígitos): ");
            string crm = Console.ReadLine();

            Console.Write("Digite a data da prescrição (dd/mm/aaaa): ");
            DateTime data = Convert.ToDateTime(Console.ReadLine());

            List<ItemPrescricao> itens = new List<ItemPrescricao>();
            bool adicionarMais = true;

            while (adicionarMais)
            {
                Console.WriteLine("\nMedicamentos disponíveis:");
                var medicamentos = repositorioMedicamento.SelecionarRegistros();
                foreach (var med in medicamentos)
                {
                    Console.WriteLine($"ID: {med.Id} | {med.Nome} | Estoque: {med.Quantidade}");
                }

                Console.Write("\nDigite o ID do medicamento: ");
                int idMedicamento = Convert.ToInt32(Console.ReadLine());

                Console.Write("Digite a dosagem: ");
                string dosagem = Console.ReadLine();

                Console.Write("Digite o período de uso: ");
                string periodo = Console.ReadLine();

                Console.Write("Digite a quantidade: ");
                int quantidade = Convert.ToInt32(Console.ReadLine());

                itens.Add(new ItemPrescricao
                {
                    MedicamentoId = idMedicamento,
                    Dosagem = dosagem,
                    Periodo = periodo,
                    Quantidade = quantidade
                });

                Console.Write("\nDeseja adicionar outro medicamento? (S/N): ");
                adicionarMais = Console.ReadLine().ToUpper() == "S";
            }

            var novaPrescricao = new Prescricao(crm, data, itens);
            string erroEstoque = novaPrescricao.ValidarEstoque(repositorioMedicamento);

            if (!string.IsNullOrEmpty(erroEstoque))
            {
                Notificador.ExibirMensagem(erroEstoque, ConsoleColor.Red);
                return null;
            }

            return novaPrescricao;
        }

        protected override void ExibirCabecalhoTabela()
        {
            Console.WriteLine("{0,-5} | {1,-10} | {2,-12} | {3,-8} | {4,-20}",
                "ID", "CRM Médico", "Data", "Validada", "Qtd Medicamentos");
        }

        protected override void ExibirLinhaTabela(Prescricao prescricao)
        {
            Console.WriteLine("{0,-5} | {1,-10} | {2,-12:dd/MM/yyyy} | {3,-8} | {4,-20}",
                prescricao.Id,
                prescricao.CrmMedico,
                prescricao.Data,
                prescricao.Validada ? "Sim" : "Não",
                prescricao.Medicamentos.Count);
        }

        public void ValidarPrescricao()
        {
            VisualizarRegistros(true);
            Console.Write("\nDigite o ID da prescrição a validar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            var prescricao = repositorioPrescricao.SelecionarRegistroPorId(id);
            if (prescricao == null)
            {
                Notificador.ExibirMensagem("Prescrição não encontrada.", ConsoleColor.Red);
                return;
            }

            string erro = prescricao.ValidarEstoque(repositorioMedicamento);
            if (!string.IsNullOrEmpty(erro))
            {
                Notificador.ExibirMensagem(erro, ConsoleColor.Red);
                return;
            }

            prescricao.Validada = true;
            repositorioPrescricao.EditarRegistro(id, prescricao);

            foreach (var item in prescricao.Medicamentos)
            {
                var medicamento = repositorioMedicamento.SelecionarRegistroPorId(item.MedicamentoId);
                medicamento.Quantidade -= item.Quantidade;
                repositorioMedicamento.EditarRegistro(medicamento.Id, medicamento);
            }

            Notificador.ExibirMensagem("Prescrição validada e estoque atualizado!", ConsoleColor.Green);
        }

        public void GerarRelatorio()
        {
            Console.Write("Digite a data inicial (dd/mm/aaaa): ");
            DateTime inicio = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Digite a data final (dd/mm/aaaa): ");
            DateTime fim = Convert.ToDateTime(Console.ReadLine());

            var prescricoes = repositorioPrescricao.ObterPrescricoesPorPeriodo(inicio, fim);

            Console.WriteLine("\nRELATÓRIO DE PRESCRIÇÕES");
            Console.WriteLine($"Período: {inicio:dd/MM/yyyy} a {fim:dd/MM/yyyy}");
            Console.WriteLine("--------------------------------------------------");

            foreach (var prescricao in prescricoes)
            {
                Console.WriteLine($"\nPrescrição #{prescricao.Id}");
                Console.WriteLine($"CRM Médico: {prescricao.CrmMedico}");
                Console.WriteLine($"Data: {prescricao.Data:dd/MM/yyyy}");
                Console.WriteLine($"Status: {(prescricao.Validada ? "Validada" : "Pendente")}");

                Console.WriteLine("\nMedicamentos prescritos:");
                foreach (var item in prescricao.Medicamentos)
                {
                    var medicamento = repositorioMedicamento.SelecionarRegistroPorId(item.MedicamentoId);
                    Console.WriteLine($"- {medicamento.Nome}: {item.Quantidade} un. | {item.Dosagem} | {item.Periodo}");
                }
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
                ExibirCabecalho();

            Console.WriteLine();

            List<Prescricao> prescricoes = repositorioPrescricao.SelecionarRegistros();

            if (prescricoes.Count == 0)
            {
                Notificador.ExibirMensagem("Nenhuma prescrição cadastrada.", ConsoleColor.DarkYellow);
                return;
            }

            ExibirCabecalhoTabela();

            foreach (Prescricao prescricao in prescricoes)
            {
                ExibirLinhaTabela(prescricao);
            }

            Console.WriteLine("\nDetalhes das Prescrições:");
            Console.WriteLine("--------------------------------------------------");

            foreach (Prescricao prescricao in prescricoes)
            {
                Console.WriteLine($"\nPrescrição #{prescricao.Id}");
                Console.WriteLine($"CRM Médico: {prescricao.CrmMedico}");
                Console.WriteLine($"Data: {prescricao.Data:dd/MM/yyyy}");
                Console.WriteLine($"Status: {(prescricao.Validada ? "Validada" : "Pendente")}");
                Console.WriteLine("Medicamentos prescritos:");

                foreach (var item in prescricao.Medicamentos)
                {
                    var medicamento = repositorioMedicamento.SelecionarRegistroPorId(item.MedicamentoId);
                    Console.WriteLine($"- {medicamento.Nome}: {item.Quantidade} un. | Dosagem: {item.Dosagem} | Período: {item.Periodo}");
                }

                Console.WriteLine("--------------------------------------------------");
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
        protected override void ExibirOpcoesExtrasDoMenu()
        {
            Console.WriteLine("5 - Visualizar Relatórios Filtrados");
            Console.WriteLine("6 - Validar Prescrição");
        }
        public void VisualizarRelatoriosFiltrados()
        {
            Console.Clear();
            Console.WriteLine("=== Relatórios de Prescrições ===\n");

            Console.Write("Filtrar por Data (dd/MM/yyyy) [Enter para ignorar]: ");
            string dataFiltroStr = Console.ReadLine();

            Console.Write("Filtrar por Nome do Paciente [Enter para ignorar]: ");
            string pacienteFiltro = Console.ReadLine();

            Console.Write("Filtrar por Nome do Medicamento [Enter para ignorar]: ");
            string medicamentoFiltro = Console.ReadLine();

            DateTime? dataFiltro = null;
            if (DateTime.TryParse(dataFiltroStr, out DateTime dataConvertida))
                dataFiltro = dataConvertida;

            List<Prescricao> prescricoes = repositorio.SelecionarRegistros();

            var prescricoesFiltradas = prescricoes.Where(p =>
                (dataFiltro == null || p.Data.Date == dataFiltro.Value.Date) &&
                (string.IsNullOrWhiteSpace(pacienteFiltro) || p.CrmMedico.Contains(pacienteFiltro, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrWhiteSpace(medicamentoFiltro) || p.Medicamentos.Any(i => repositorioMedicamento.SelecionarRegistroPorId(i.MedicamentoId).Nome.Contains(medicamentoFiltro, StringComparison.OrdinalIgnoreCase)))
            ).ToList();

            if (prescricoesFiltradas.Count == 0)
            {
                Notificador.ExibirMensagem("Nenhuma prescrição encontrada com os filtros informados.", ConsoleColor.Yellow);
                return;
            }

            foreach (var p in prescricoesFiltradas)
            {
                Console.WriteLine(p);
                Console.WriteLine("------------------------------------");
            }

            Console.ReadLine();
        }

    }
}