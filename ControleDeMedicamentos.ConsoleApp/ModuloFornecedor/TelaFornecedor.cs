

using ControleDeMedicamentos.ConsoleApp.ControleDeMedicamentos.ConsoleApp;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.Util;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedor
{
    public class TelaFornecedor : TelaBase<Fornecedor>, ITelaCrud
    {
        private IRepositorioFornecedor repositorioFornecedor;
        private IRepositorioMedicamento repositorioMedicamento;

        public TelaFornecedor(IRepositorioFornecedor repositorioFornecedor, IRepositorioMedicamento repositorioMedicamento)
            : base("Fornecedor", repositorioFornecedor)
        {
            this.repositorioFornecedor = repositorioFornecedor;
            this.repositorioMedicamento = repositorioMedicamento;
        }

    
        protected override bool PodeCadastrar(Fornecedor fornecedor)
        {
            if (repositorioFornecedor.CnpjDuplicado(fornecedor.CNPJ))
            {
                Notificador.ExibirMensagem("CNPJ já cadastrado para outro fornecedor.", ConsoleColor.Red);
                return false;
            }

            return true;
        }

        protected override bool PodeExcluir(int id)
        {
            bool temVinculo = repositorioMedicamento
                .SelecionarRegistros()
                .Any(m => m.Fornecedor.Id == id);

            if (temVinculo)
            {
                Notificador.ExibirMensagem("Fornecedor vinculado a medicamentos. Exclusão não permitida.", ConsoleColor.Red);
                return false;
            }

            return true;
        }

        public override Fornecedor ObterDados()
        {
            Console.Write("Digite o nome: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o Telefone: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite o CNPJ: ");
            string cnpj = Console.ReadLine();

            Fornecedor fornecedor = new Fornecedor(nome, telefone, cnpj);
            return fornecedor;

        }

        protected override void ExibirCabecalhoTabela()
        {
            Console.WriteLine("{0, 10} | {1, 30} | {2, 20} | {3, 20} ", "Id", "Nome", "Telefone", "CNPJ");
        }

        protected override void ExibirLinhaTabela(Fornecedor fornecedor)
        {
            Console.WriteLine("{0, 10} | {1, 30} | {2, 20} | {3, 20} ", fornecedor.Id, fornecedor.Nome, fornecedor.Telefone, fornecedor.CNPJ);
        }
        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
                ExibirCabecalho();

            Console.WriteLine();

            List<Fornecedor> fornecedores = repositorio.SelecionarRegistros();

            if (fornecedores.Count == 0)
            {
                Notificador.ExibirMensagem("Nenhum fornecedor cadastrado.", ConsoleColor.DarkYellow);
                return;
            }

            ExibirCabecalhoTabela();

            foreach (Fornecedor fornecedor in fornecedores)
            {
                ExibirLinhaTabela(fornecedor);
            }

            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
