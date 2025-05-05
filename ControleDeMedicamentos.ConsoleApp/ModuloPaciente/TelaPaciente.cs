using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente
{
    public class TelaPaciente : TelaBase<Paciente>, ITelaCrud
    {
        private IRepositorioPaciente repositorioPaciente1;
        private IRepositorioPaciente repositorioPaciente2;

        public TelaPaciente(string nomeEntidade, IRepositorioPaciente repositorio)
               : base(nomeEntidade, repositorio)
        {
        }
        public TelaPaciente(IRepositorioPaciente repositorioPaciente1, IRepositorioPaciente repositorioPaciente2)
        {
            this.repositorioPaciente1 = repositorioPaciente1;
            this.repositorioPaciente2 = repositorioPaciente2;
        }

        public override Paciente ObterDados()
        {
            Console.WriteLine("Digite o nome do Paciente: ");
            string nome = Console.ReadLine()!;

            Console.WriteLine("Digite o telefone do Paciente: ");
            string telefone = Console.ReadLine()!;

            Console.WriteLine("Digite o CPF do Paciente: ");
            string cpf = Console.ReadLine()!;

            Console.WriteLine("Digite o CPF do Paciente: ");
            string endereco = Console.ReadLine()!;

            Console.WriteLine("Digite a data de nascimento do Paciente: ");
            DateTime dataNascimento = Convert.ToDateTime(Console.ReadLine());

            Paciente paciente = new Paciente()
            {
                Nome = nome,
                Telefone = telefone,
                CPF = cpf,
                Endereco = endereco,
                DataNascimento = dataNascimento
            };

            return paciente;
        }

        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
                ExibirCabecalho();

            Console.WriteLine();

            List<Paciente> pacientes = repositorio.SelecionarRegistros();

            if (pacientes.Count == 0 || pacientes == null)
            {
                Notificador.ExibirMensagem("Nenhum paciente encontrado.", ConsoleColor.DarkYellow);
                return;
            }

            ExibirCabecalhoTabela();

            foreach (Paciente paciente in pacientes)
            {
                ExibirLinhaTabela(paciente);
            }

            Console.WriteLine();
        }

        protected override void ExibirCabecalhoTabela()
        {
            Console.WriteLine("{0, 10} | {1, 30} | {2, 20} | {3, 20}",
                                "Id", "Nome", "Telefone", "CPF");
        }

        protected override void ExibirLinhaTabela(Paciente paciente)
        {
            Console.WriteLine("{0, 10} | {1, 30} | {2, 20} | {3, 20}",
           paciente.Id, paciente.Nome, paciente.Telefone, paciente.CPF);
        }
    }
}
