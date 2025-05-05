using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPrescricao
{
    public class RepositorioPrescricaoEmArquivo : RepositorioBaseEmArquivo<Prescricao>, IRepositorioPrescricao
    {
        public RepositorioPrescricaoEmArquivo(ContextoDados contexto) : base(contexto)
        {
        }

        protected override List<Prescricao> ObterRegistros()
        {
            return contexto.Prescricoes;
        }

        public List<Prescricao> ObterPrescricoesPorPeriodo(DateTime inicio, DateTime fim)
        {
            return contexto.Prescricoes
                .Where(p => p.Data >= inicio && p.Data <= fim)
                .ToList();
        }
    }
}