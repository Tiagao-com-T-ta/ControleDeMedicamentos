
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricao;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.Util;

namespace ControleDeMedicamentos.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TelaPrincipal telaPrincipal = new TelaPrincipal();

            while (true)
            {
                telaPrincipal.ApresentarMenuPrincipal();

                ITelaCrud telaSelecionada = telaPrincipal.ObterTela();

                char opcaoEscolhida = telaSelecionada.ApresentarMenu();

                switch (opcaoEscolhida)
                {
                    case '1': telaSelecionada.CadastrarRegistro(); break;

                    case '2': telaSelecionada.EditarRegistro(); break;

                    case '3': telaSelecionada.ExcluirRegistro(); break;

                    case '4': telaSelecionada.VisualizarRegistros(true); break;

                    case '5':
                        {
                            if (telaSelecionada is TelaPrescricao telaPrescricao)
                                telaPrescricao.VisualizarRelatoriosFiltrados();
                            break;
                        }
                    case '6':
                        {
                            if (telaSelecionada is TelaPrescricao telaPrescricao)
                                telaPrescricao.ValidarPrescricao();
                            break;
                        }

                    case 's' or 'S': break;

                    default: break;
                }
            }
        }
    }
}