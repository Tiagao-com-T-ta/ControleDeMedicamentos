using ControleDeMedicamentos.ConsoleApp.ControleDeMedicamentos.ConsoleApp;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente
{
    internal class RepositorioPacienteEmArquivo : IRepositorioPaciente
    {
        private  ContextoDados contexto;
        private List<Paciente> registros;
        public RepositorioPacienteEmArquivo(ContextoDados contexto)
        {
            this.contexto = contexto;
            this.registros = contexto.Pacientes;
        }

        public void CadastrarRegistro(Paciente novoRegistro)
        {
            novoRegistro.Id = GerarNovoId();
            registros.Add(novoRegistro);
            contexto.Salvar();
        }

        public bool EditarRegistro(int idRegistro, Paciente registroEditado)
        {
            Paciente pacienteExistente = SelecionarRegistroPorId(idRegistro);

            if (pacienteExistente == null)
                return false;

            pacienteExistente.AtualizarRegistro(registroEditado);
            contexto.Salvar();
            return true;    
        }

        public bool ExcluirRegistro(int idRegistro)
        {
            Paciente pacienteExistente = SelecionarRegistroPorId(idRegistro);

            if (pacienteExistente == null) { return false; }

            registros.Remove(pacienteExistente);
            contexto.Salvar();
            return true;
        }

        public Paciente SelecionarRegistroPorId(int idRegistro)
        {
            return registros.FirstOrDefault(p => p.Id == idRegistro)!;
        }

        public List<Paciente> SelecionarRegistros()
        {
            return registros;
        }

        private int GerarNovoId()
        {
            if (registros.Count == 0)
                return 1;

            return registros.Max(p => p.Id) + 1;
        }
    }
}