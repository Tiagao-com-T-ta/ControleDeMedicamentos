using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPrescricao
{
    public interface IRepositorioPrescricao : IRepositorio<Prescricao>
    {
        List<Prescricao> ObterPrescricoesPorPeriodo(DateTime inicio, DateTime fim);
    }
}