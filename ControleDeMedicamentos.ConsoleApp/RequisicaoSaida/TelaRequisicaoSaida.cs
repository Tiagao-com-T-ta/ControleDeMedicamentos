using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ControleDeMedicamentos.ConsoleApp;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricao;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.Util;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicao
{
    public class TelaRequisicaoSaida : TelaBase<RequisicaoSaida>, ITelaCrud
    {

        private readonly List<RequisicaoSaida> requisicoes = new();

        public TelaRequisicaoSaida(ContextoDados contexto, IRepositorioPaciente repositorioPaciente, string entidade, IRepositorio<RequisicaoSaida> repositorio) : base(entidade, repositorio)
        {
        }

        public override RequisicaoSaida ObterDados()
        {
            throw new NotImplementedException();
        }

        public void RegistrarRequisicao(RepositorioPacienteEmMemoria repositorioPacienteEmMemoria, RepositorioPrescricaoEmArquivo repositorioPrescricaoEmArquivo, RepositorioMedicamentoEmArquivo repositorioMedicamentoEmArquivo)
        {
            Console.Clear();
            Console.WriteLine("Registro de Requisição de Saída");
            Console.WriteLine("------------------------------");

            Console.Write("Digite o ID do paciente: ");
            int idPaciente = Convert.ToInt32(Console.ReadLine());
            Paciente paciente = repositorioPacienteEmMemoria.SelecionarRegistroPorId(idPaciente);
            if (paciente == null)
            {
                Notificador.ExibirMensagem("Paciente não encontrado.", ConsoleColor.Red);
                return;
            }

            var prescricoes = repositorioPrescricaoEmArquivo.SelecionarRegistros()
                .Where(p => p.Paciente != null && p.Paciente.Id == paciente.Id && p.Validada)
                .ToList();

            if (prescricoes.Count == 0)
            {
                Notificador.ExibirMensagem("Nenhuma prescrição validada encontrada para este paciente.", ConsoleColor.Red);
                return;
            }

            Console.WriteLine("Prescrições válidas:");
            foreach (var p in prescricoes)
                Console.WriteLine($"ID: {p.Id} | Data: {p.Data:dd/MM/yyyy}");

            Console.Write("Digite o ID da prescrição: ");
            int idPrescricao = Convert.ToInt32(Console.ReadLine());
            Prescricao prescricao = repositorioPrescricaoEmArquivo.SelecionarRegistroPorId(idPrescricao);

            if (prescricao == null || prescricao.Paciente.Id != paciente.Id)
            {
                Notificador.ExibirMensagem("Prescrição inválida para o paciente.", ConsoleColor.Red);
                return;
            }

            var itens = new List<ItemRequisicao>();

            foreach (var itemPrescricao in prescricao.Medicamentos)
            {
                var medicamento = repositorioMedicamentoEmArquivo.SelecionarRegistroPorId(itemPrescricao.MedicamentoId);

                if (medicamento == null)
                    continue;

                Console.Write($"Requisitar {itemPrescricao.Quantidade} de {medicamento.Nome}? (S/N): ");
                if (Console.ReadLine()!.ToUpper() != "S")
                    continue;

                if (itemPrescricao.Quantidade > medicamento.Quantidade)
                {
                    Notificador.ExibirMensagem($"Estoque insuficiente para {medicamento.Nome}.", ConsoleColor.Red);
                    return;
                }

                itens.Add(new ItemRequisicao
                {
                    Medicamento = medicamento,
                    Quantidade = itemPrescricao.Quantidade
                });
            }

            if (itens.Count == 0)
            {
                Notificador.ExibirMensagem("Nenhum medicamento foi requisitado.", ConsoleColor.Red);
                return;
            }

            foreach (var item in itens)
            {
                item.Medicamento.Quantidade -= item.Quantidade;
                repositorioMedicamentoEmArquivo.EditarRegistro(item.Medicamento.Id, item.Medicamento);
            }

            var requisicao = new RequisicaoSaida
            {
                Id = requisicoes.Count + 1,
                Data = DateTime.Now,
                Paciente = paciente,
                Prescricao = prescricao,
                Itens = itens
            };

            requisicoes.Add(requisicao);
            Notificador.ExibirMensagem("Requisição registrada com sucesso!", ConsoleColor.Green);
        }

        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (requisicoes.Count == 0)
            {
                Notificador.ExibirMensagem("Nenhuma requisição registrada.", ConsoleColor.DarkYellow);
                return;
            }

            Console.WriteLine("\nLista de todas as requisições:");
            foreach (var r in requisicoes)
            {
                Console.WriteLine($"\n#{r.Id} | {r.Data:dd/MM/yyyy} | Paciente: {r.Paciente.Nome} | Prescrição ID: {r.Prescricao.Id}");
                foreach (var item in r.Itens)
                    Console.WriteLine($"- {item.Medicamento.Nome}: {item.Quantidade} unidades");
            }
        }

        protected override void ExibirCabecalhoTabela()
        {
            Console.WriteLine("{0, 10} | {1, 30} | {2, 20} | {3, 20} | {4, 20}",
                               "Id", "Nome", "Data", "Prescrição", "Itens");
        }

        protected override void ExibirLinhaTabela(RequisicaoSaida registro)
        {
            Console.WriteLine($"ID: {registro.Id} | Data: {registro.Data:dd/MM/yyyy} | Paciente: {registro.Paciente?.Nome} | Prescrição ID: {registro.Prescricao?.Id} | Itens: {registro.Itens.Count}");
        }
    }
}
