using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp
{
    public class RepositorioRequisicaoEntradaEmArquivo : RepositorioBaseEmArquivo<RequisicaoEntrada>, IRepositorioRequisicaoEntrada
    {
        public RepositorioRequisicaoEntradaEmArquivo(ContextoDados contexto) : base(contexto)
        {
        }

        protected override List<RequisicaoEntrada> ObterRegistros()
        {
            throw new NotImplementedException();

            //return contexto.RequisicaoEntrada;
        }


    }
}
