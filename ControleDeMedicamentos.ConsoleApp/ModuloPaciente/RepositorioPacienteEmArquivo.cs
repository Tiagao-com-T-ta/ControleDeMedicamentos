using ControleDeMedicamentos.ConsoleApp.ControleDeMedicamentos.ConsoleApp;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente
{
    internal class RepositorioPacienteEmArquivo : IRepositorioPaciente
    {
        private ContextoDados contexto;

        public RepositorioPacienteEmArquivo(ContextoDados contexto)
        {
            this.contexto = contexto;
        }

        public void CadastrarRegistro(Paciente novoRegistro)
        {
            throw new NotImplementedException();
        }

        public bool EditarRegistro(int idRegistro, Paciente registroEditado)
        {
            throw new NotImplementedException();
        }

        public bool ExcluirRegistro(int idRegistro)
        {
            throw new NotImplementedException();
        }

        public Paciente SelecionarRegistroPorId(int idRegistro)
        {
            throw new NotImplementedException();
        }

        public List<Paciente> SelecionarRegistros()
        {
            throw new NotImplementedException();
        }
    }
}