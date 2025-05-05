using ControleDeMedicamentos.ConsoleApp.ControleDeMedicamentos.ConsoleApp;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamento
{
    public class RepositorioMedicamentoEmArquivo : RepositorioBaseEmArquivo<Medicamento>, IRepositorioMedicamento
    {
        private readonly IRepositorioFornecedor repositorioFornecedor;

        public RepositorioMedicamentoEmArquivo(ContextoDados contexto, IRepositorioFornecedor repositorioFornecedor)
            : base(contexto)
        {
            this.repositorioFornecedor = repositorioFornecedor ?? throw new ArgumentNullException(nameof(repositorioFornecedor));
        }

        protected override List<Medicamento> ObterRegistros()
        {
            if (contexto?.Medicamentos == null)
            {
                return new List<Medicamento>(); 
            }

            var medicamentos = contexto.Medicamentos;

            foreach (var medicamento in medicamentos)
            {
                if (medicamento == null) continue; 

                if (medicamento.Fornecedor == null && medicamento.FornecedorId > 0)
                {
                    if (repositorioFornecedor != null)
                    {
                        medicamento.Fornecedor = repositorioFornecedor.SelecionarRegistroPorId(medicamento.FornecedorId);
                    }
                }
            }

            return medicamentos;
        }
    }
}