using ControleDeMedicamentos.ConsoleApp;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp
{
    public class RepositorioFornecedorEmArquivo : RepositorioBaseEmArquivo<Fornecedor>, IRepositorioFornecedor
    {
        public RepositorioFornecedorEmArquivo(ContextoDados contexto) : base(contexto)
        {
        }

        protected override List<Fornecedor> ObterRegistros()
        {
            return contexto.Fornecedores;
        }

        
    }
}
