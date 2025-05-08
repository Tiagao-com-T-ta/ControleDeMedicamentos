using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicao;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeMedicamentos.ConsoleApp.Repositorio
{
    public class RepositorioRequisicaoSaidaEmMemoria : RepositorioEmArquivo<RequisicaoSaida>
    {
        public RepositorioRequisicaoSaidaEmMemoria(ContextoDados contexto) : base(contexto)
        {
        }

        public override List<RequisicaoSaida> ObterRegistros()
        {
            return registros;
        }

        public List<RequisicaoSaida> ObterRequisicoesPorPaciente(int idPaciente)
        {
            return registros.Where(r => r.Paciente != null && r.Paciente.Id == idPaciente).ToList();
        }
    }
}