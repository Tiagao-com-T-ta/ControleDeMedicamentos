using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp
{
    public class RepositorioFornecedorEmMemoria : IRepositorioFornecedor
    {
        private List<Fornecedor> fornecedores = new List<Fornecedor>();
        private int contadorIds = 0;

        public void CadastrarRegistro(Fornecedor novoRegistro)
        {
            novoRegistro.Id = ++contadorIds;
            fornecedores.Add(novoRegistro);
        }

        public bool EditarRegistro(int idRegistro, Fornecedor registroEditado)
        {
            Fornecedor fornecedorSelecionado = SelecionarRegistroPorId(idRegistro);

            if (fornecedorSelecionado == null)
                return false;

            fornecedorSelecionado.AtualizarRegistro(registroEditado);

            return true;
        }

        public bool ExcluirRegistro(int idRegistro)
        {
            Fornecedor fornecedorSelecionado = SelecionarRegistroPorId(idRegistro);

            if (fornecedorSelecionado == null)
                return false;

            fornecedores.Remove(fornecedorSelecionado);

            return true;
        }

        public Fornecedor SelecionarRegistroPorId(int idRegistro)
        {
            return fornecedores.FirstOrDefault(f => f.Id == idRegistro);
        }

        public List<Fornecedor> SelecionarRegistros()
        {
            return fornecedores;
        }
    }
}