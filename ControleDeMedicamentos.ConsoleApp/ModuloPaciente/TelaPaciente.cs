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
        private IRepositorioPaciente repositorioPaciente;


        public TelaPaciente(IRepositorioPaciente repositorioPaciente) : base("Paciente", repositorioPaciente)
        {
            this.repositorioPaciente = repositorioPaciente;
        }


        public override Paciente ObterDados()
        {
            Console.WriteLine("Digite o nome do Paciente: ");
            string nome = Console.ReadLine()!;

            Console.WriteLine("Digite o telefone do Paciente: ");
            string telefone = Console.ReadLine()!;

            Console.WriteLine("Digite o CPF do Paciente: ");
            string cpf = Console.ReadLine()!;

            Console.WriteLine("Digite o ID do Cartão SUS do Paciente: ");
            string cartaoSUS = Console.ReadLine()!;

            Console.WriteLine("Digite o endereço do Paciente: ");
            string endereco = Console.ReadLine()!;

            Console.WriteLine("Digite a data de nascimento do Paciente: ");
            DateTime dataNascimento = Convert.ToDateTime(Console.ReadLine());

            Paciente paciente = new Paciente(nome, cpf, dataNascimento, telefone, endereco, cartaoSUS);

            if (repositorioPaciente.CartaoSUSJaExiste(paciente.CartaoSUS))
            {
                Notificador.ExibirMensagem("Cartão do SUS já cadastrado para outro paciente.", ConsoleColor.Red);
                return null!;
            }

            return paciente;
        }

        public override void EditarRegistro()
        {
            ExibirCabecalho();

            Console.WriteLine($"Editando {nomeEntidade}...");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine();

            VisualizarRegistros(false);

            Console.Write("Digite o ID do registro que deseja editar: ");
            int idRegistro = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Paciente pacienteExistente = repositorioPaciente.SelecionarRegistroPorId(idRegistro);

            if (pacienteExistente == null)
            {
                Notificador.ExibirMensagem("Paciente não encontrado.", ConsoleColor.Red);
                return;
            }

            Paciente pacienteEditado = ObterDados();

            if (pacienteEditado == null)
            {
                return;
            }

            string erros = pacienteEditado.Validar();

            if (erros.Length > 0)
            {
                Notificador.ExibirMensagem(erros, ConsoleColor.Red);
                return;
            }

            if (pacienteEditado.CartaoSUS != pacienteExistente.CartaoSUS &&
                repositorioPaciente.CartaoSUSJaExiste(pacienteEditado.CartaoSUS))
            {
                Notificador.ExibirMensagem("Este Cartão SUS já está associado a outro paciente.", ConsoleColor.Red);
                return;
            }

            bool conseguiuEditar = repositorioPaciente.EditarRegistro(idRegistro, pacienteEditado);

            if (!conseguiuEditar)
            {
                Notificador.ExibirMensagem("Houve um erro durante a edição do registro...", ConsoleColor.Red);
                return;
            }

            Notificador.ExibirMensagem("O registro foi editado com sucesso!", ConsoleColor.Green);
        }

        public override void ExcluirRegistro()
        {
            ExibirCabecalho();

            Console.WriteLine($"Excluindo {nomeEntidade}...");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine();

            VisualizarRegistros(false);

            Console.Write("Digite o ID do registro que deseja excluir: ");
            int idRegistro = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Paciente pacienteExistente = repositorioPaciente.SelecionarRegistroPorId(idRegistro);

            if (pacienteExistente == null)
            {
                Notificador.ExibirMensagem("Paciente não encontrado.", ConsoleColor.Red);
                return;
            }

            // Verificar se o paciente tem vínculos (por exemplo, registros clínicos)
            if (ExisteVinculosComRegistrosClinicos(pacienteExistente))
            {
                Notificador.ExibirMensagem("Não é possível excluir o paciente, pois ele tem registros clínicos vinculados.", ConsoleColor.Red);
                return;
            }

            bool conseguiuExcluir = repositorioPaciente.ExcluirRegistro(idRegistro);

            if (!conseguiuExcluir)
            {
                Notificador.ExibirMensagem("Houve um erro durante a exclusão do registro...", ConsoleColor.Red);
                return;
            }

            Notificador.ExibirMensagem("O paciente foi excluído com sucesso!", ConsoleColor.Green);
        }

        private bool ExisteVinculosComRegistrosClinicos(Paciente paciente)
        {
            // Verifique se o paciente tem alguma propriedade, lista ou campo de registros clínicos
            // por exemplo, vamos supor que cada paciente tenha uma lista de "RegistrosClinicos"

            if (paciente.RegistrosClinicos != null && paciente.RegistrosClinicos.Count > 0)
            {
                return true;  
            }

            return false; 
        }

        protected override void ExibirCabecalhoTabela()
        {
            Console.WriteLine("{0, 10} | {1, 30} | {2, 20} | {3, 20} | {4, 20}",
                                "Id", "Nome", "Telefone", "CPF", "ID - SUS");
        }

        protected override void ExibirLinhaTabela(Paciente paciente)
        {
            Console.WriteLine("{0, 10} | {1, 30} | {2, 20} | {3, 20} | {4, 20}",
           paciente.Id, paciente.Nome, paciente.Telefone, paciente.CPF, paciente.CartaoSUS);
        }

        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
                ExibirCabecalho();

            Console.WriteLine();

            List<Paciente> pacientes = repositorio.SelecionarRegistros();

            if (pacientes.Count == 0)
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
            Console.ReadLine();
        }

    }
}
