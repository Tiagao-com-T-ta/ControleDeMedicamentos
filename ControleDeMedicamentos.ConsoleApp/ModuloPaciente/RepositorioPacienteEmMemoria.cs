using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.Util;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente
{
    public class RepositorioPacienteEmMemoria : IRepositorioPaciente
    {
        private List<Paciente> pacientes = new List<Paciente>();
        private int ContadorIds = 0;

        public void CadastrarRegistro(Paciente novoRegistro)
        {
            novoRegistro.Id = ++ContadorIds;
            pacientes.Add(novoRegistro);           
        }

        public bool EditarRegistro(int idRegistro, Paciente registroEditado)
        {
            Paciente pacienteExistente = SelecionarRegistroPorId(idRegistro);

            if (pacienteExistente == null)
                return false;

            pacienteExistente.AtualizarRegistro(registroEditado);

            return true;
        }

        public bool ExcluirRegistro(int idRegistro)
        {
            Paciente pacienteExistente = SelecionarRegistroPorId(idRegistro);

            if (pacienteExistente == null)
                return false;

            pacientes.Remove(pacienteExistente);

            for (int i = 0; i < pacientes.Count; i++)
                pacientes[i].Id = i + 1;

            ContadorIds = pacientes.Count;

            return true;
        }

        public Paciente SelecionarRegistroPorId(int idRegistro)
        {
            return pacientes.FirstOrDefault(p => p.Id == idRegistro)!;
        }

        public List<Paciente> SelecionarRegistros()
        {
            return pacientes;
        }

        public bool CartaoSUSJaExiste(string cartaoSUS)
        {
            return pacientes.Any(p => p.CartaoSUS == cartaoSUS);
        }

    }
}
