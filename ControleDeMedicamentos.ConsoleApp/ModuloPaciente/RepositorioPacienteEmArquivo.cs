using ControleDeMedicamentos.ConsoleApp.ControleDeMedicamentos.ConsoleApp;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente
{
    public class RepositorioPacienteEmArquivo : RepositorioBaseEmArquivo<Paciente>, IRepositorioPaciente
    {
        public RepositorioPacienteEmArquivo(ContextoDados contexto) : base(contexto)
        {

        }

        public bool CartaoSUSJaExiste(string cartaoSUS)
        {
            return contexto.Pacientes.Any(p => p.CartaoSUS == cartaoSUS);
        }

        protected override List<Paciente> ObterRegistros()
        {
            return contexto.Pacientes;
        }
    }
}
