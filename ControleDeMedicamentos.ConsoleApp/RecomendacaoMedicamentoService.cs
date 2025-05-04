using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class RecomendacaoMedicamentoService
{
    private readonly ContextoDados _contexto;

    public RecomendacaoMedicamentoService(ContextoDados contexto)
    {
        _contexto = contexto;
    }

    public void TreinarModelo()
    {
        if (_contexto.Medicamentos == null || !_contexto.Medicamentos.Any())
            throw new InvalidOperationException("Nenhum medicamento cadastrado para treinamento.");

        var medicamentosComSintomas = _contexto.Medicamentos
        .Where(m => m.SintomasTratados != null && m.SintomasTratados.Any(s => !string.IsNullOrWhiteSpace(s)));

        if (!medicamentosComSintomas.Any())
            throw new InvalidOperationException("Nenhum medicamento com sintomas válidos definidos.");

        string tempCsvPath = Path.Combine(Path.GetTempPath(), "temp_treinamento.csv");

        try
        {
            using (var writer = new StreamWriter(tempCsvPath))
            {
                writer.WriteLine("Sintomas,MedicamentoRecomendado");

                foreach (var med in medicamentosComSintomas)
                {
                    var sintomas = med.SintomasTratados
                        .Select(s => s.Trim())
                        .Where(s => !string.IsNullOrEmpty(s))
                        .Distinct();

                    foreach (var sintoma in sintomas)
                    {
                        writer.WriteLine($"{EscapeCsv(sintoma)},{EscapeCsv(med.Nome)}");
                    }
                }
            }

            Console.WriteLine($"Arquivo de treinamento gerado com sucesso: {tempCsvPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao gerar o arquivo de treinamento: {ex.Message}");
        }
    }

    public Medicamento RecomendarMedicamento(List<string> sintomasPaciente)
    {
        if (sintomasPaciente == null || sintomasPaciente.Count == 0)
            throw new ArgumentException("Nenhum sintoma informado.");

        var medicamentos = _contexto.Medicamentos;

        if (medicamentos == null || medicamentos.Count == 0)
            throw new InvalidOperationException("Nenhum medicamento cadastrado.");

        Medicamento melhorMedicamento = null;
        int maiorNumeroDeCorrespondencias = 0;

        foreach (var medicamento in medicamentos)
        {
            if (medicamento.SintomasTratados == null)
                continue;

            int correspondencias = medicamento.SintomasTratados
                .Count(s => sintomasPaciente.Any(sp => string.Equals(sp.Trim(), s.Trim(), StringComparison.OrdinalIgnoreCase)));

            if (correspondencias > maiorNumeroDeCorrespondencias)
            {
                melhorMedicamento = medicamento;
                maiorNumeroDeCorrespondencias = correspondencias;
            }
        }

        return melhorMedicamento;
    }

    private string EscapeCsv(string value)
    {
        if (value == null) return "\"\"";
        return $"\"{value.Replace("\"", "\"\"")}\"";
    }
}
