using ControleDeMedicamentos.ConsoleApp.ControleDeMedicamentos.ConsoleApp;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp
{
    public class TelaRequisicaoEntrada : TelaBase<RequisicaoEntrada>, ITelaCrud
    {
        private readonly IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada;

        private readonly IRepositorioMedicamento repositorioMedicamento;

        private readonly IRepositorioFuncionario repositorioFuncionario;
        public TelaRequisicaoEntrada(IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada, 
            IRepositorioMedicamento repositorioMedicamento, IRepositorioFuncionario repositorioFuncionario) : base("Requisição de Entrada", repositorioRequisicaoEntrada)
        {
            this.repositorioRequisicaoEntrada = repositorioRequisicaoEntrada;
            this.repositorioFuncionario = repositorioFuncionario;
            this.repositorioMedicamento = repositorioMedicamento;
        }

        public override RequisicaoEntrada ObterDados()
        {
            Console.Write("Digite a data (DD/MM/AAAA): ");
            DateTime data = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Selecione o medicamento: ");

            List<Medicamento> medicamentos = repositorioMedicamento.SelecionarRegistros();

            foreach (Medicamento m in medicamentos)
                Console.WriteLine($"ID:{m.Id} | Nome: {m.Nome}");

            Console.Write("Digite o ID do medicamento: ");
            int idMedicamento = Convert.ToInt32((Console.ReadLine()));
            Medicamento medicamento = repositorioMedicamento.SelecionarRegistroPorId(idMedicamento);

            if (medicamento == null)
            {
                Notificador.ExibirMensagem("Medicamento não encontrado.", ConsoleColor.Red);
                return null!;
            }

            Console.WriteLine("Selecione o funcionário: ");

            List<Funcionario> funcionarios = repositorioFuncionario.SelecionarRegistros();

            foreach (Funcionario f in funcionarios)
                Console.WriteLine($"ID:{f.Id} | Nome: {f.Nome}");

            Console.Write("Selecione o ID do funcionário: ");
            int idFuncionario = Convert.ToInt32((Console.ReadLine()));
            Funcionario funcionario = repositorioFuncionario.SelecionarRegistroPorId(idFuncionario);

            if (funcionario == null)
            {
                Notificador.ExibirMensagem("Funcionário não encontrado.", ConsoleColor.Red);
                return null;
            }

            Console.Write("Digite a quantidade a ser adicionada: ");
            int quantidade = Convert.ToInt32((Console.ReadLine()));

            medicamento.Quantidade += quantidade;
            RequisicaoEntrada novaRequisicao = new RequisicaoEntrada (medicamento, funcionario, quantidade, DateTime.Now);

            return novaRequisicao;

        }

        public override void VisualizarRegistros(bool exibirTitulo)
        {
            throw new NotImplementedException();
        }

        protected override void ExibirCabecalhoTabela()
        {
            throw new NotImplementedException();
        }

        protected override void ExibirLinhaTabela(RequisicaoEntrada registro)
        {
            throw new NotImplementedException();
        }
    }
}
