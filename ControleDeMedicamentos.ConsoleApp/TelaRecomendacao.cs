using System;
using System.Collections.Generic;

public class TelaRecomendacao
{
    private readonly RecomendacaoMedicamentoService _servicoRecomendacao;

    public TelaRecomendacao(RecomendacaoMedicamentoService servicoRecomendacao)
    {
        _servicoRecomendacao = servicoRecomendacao;
    }

    public void Recomendacao()
    {
        Console.WriteLine("\n--- Recomendação de Medicamento ---");

        List<string> sintomasPaciente = new List<string>();

        while (true)
        {
            Console.Write("Digite um sintoma (ou 'fim' para terminar): ");
            string sintoma = Console.ReadLine();

            if (sintoma.ToLower() == "fim")
                break;

            if (!string.IsNullOrWhiteSpace(sintoma))
                sintomasPaciente.Add(sintoma.Trim());
        }

        if (sintomasPaciente.Count == 0)
        {
            Console.WriteLine("Nenhum sintoma informado.");
            return;
        }

        var recomendado = _servicoRecomendacao.RecomendarMedicamento(sintomasPaciente);

        if (recomendado != null)
        {
            Console.WriteLine($"\nMedicamento recomendado: {recomendado.Nome}");
        }
        else
        {
            Console.WriteLine("\nNenhum medicamento encontrado com os sintomas informados.");
        }
    }

    public void Treinar()
    {
        Console.WriteLine("\n--- Treinando modelo com medicamentos cadastrados ---");

        try
        {
            _servicoRecomendacao.TreinarModelo();
            Console.WriteLine("Treinamento concluído.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro durante o treinamento: {ex.Message}");
        }
    }
}
