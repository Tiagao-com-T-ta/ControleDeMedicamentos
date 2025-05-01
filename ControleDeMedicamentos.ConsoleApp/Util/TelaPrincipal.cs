using GestaoDeEquipamentos.ConsoleApp.Compartilhado;

namespace GestaoDeEquipamentos.ConsoleApp.Util;

public class TelaPrincipal
{
    private char opcaoPrincipal;
    private ContextoDados contexto;


    public TelaPrincipal()
    {
        this.contexto = new ContextoDados(true);
    }

    public void ApresentarMenuPrincipal()
    {
        Console.Clear();

        Console.WriteLine("----------------------------------------");
        Console.WriteLine("|        Gestão de Medicamentos        |");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        Console.WriteLine("1 - Controle de ");
        Console.WriteLine("S - Sair");

        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        opcaoPrincipal = Console.ReadLine()[0];
    }

        public ITelaCrud ObterTela()
    {
        //if (opcaoPrincipal == '1')
            //return new 

        return null;
}
}