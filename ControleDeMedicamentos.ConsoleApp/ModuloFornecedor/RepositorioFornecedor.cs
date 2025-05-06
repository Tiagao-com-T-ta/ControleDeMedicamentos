using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedor
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
        public bool CnpjDuplicado(string cnpj, int idDesconsiderar = 0)
        {
            foreach (var fornecedor in SelecionarRegistros())
            {
                if (fornecedor.CNPJ == cnpj && fornecedor.Id != idDesconsiderar)
                    return true;
            }

            return false;
        }
        public bool CnpjDuplicado(string cnpj)
        {
            return ObterRegistros().Any(f => f.CNPJ == cnpj);
        }
    }

}
