using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicao;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeMedicamentos.ConsoleApp.Repositorio
{
    public class RepositorioRequisicaoSaidaEmArquivo : RepositorioEmArquivo<RequisicaoSaida>
    {
        public RepositorioRequisicaoSaidaEmArquivo(ContextoDados contexto) : base(contexto)
        {
        }

        protected override List<RequisicaoSaida> ObterRegistrosDoContexto()
        {
            return contexto.requisicoesSaida;
        }

        public List<RequisicaoSaida> ObterRequisicoesPorPaciente(int idPaciente)
        {
            return SelecionarRegistros()
                .Where(r => r.Paciente != null && r.Paciente.Id == idPaciente)
                .ToList();
        }
    }
}