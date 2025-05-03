using ControleDeMedicamentos.ConsoleApp.ControleDeMedicamentos.ConsoleApp;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp
{
    public class RepositorioMedicamentoEmArquivo : RepositorioBaseEmArquivo<Medicamento>, IRepositorioMedicamento
    {
        public RepositorioMedicamentoEmArquivo(ContextoDados contexto) : base(contexto)
        {
        }

        protected override List<Medicamento> ObterRegistros()
        {
            return contexto.Medicamentos;
        }
    }
}