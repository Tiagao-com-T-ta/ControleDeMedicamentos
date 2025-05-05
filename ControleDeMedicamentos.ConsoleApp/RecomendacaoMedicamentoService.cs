using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Data;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class RecomendacaoMedicamentoService
{
    private readonly ContextoDados _contexto;
    private readonly MLContext _mlContext;
    private ITransformer _modeloTreinado;
    private PredictionEngine<SintomaMedicamento, SintomaPredicao> _motorDePredicao;

    public RecomendacaoMedicamentoService(ContextoDados contexto)
    {
        _contexto = contexto;
        _mlContext = new MLContext();
    }

    public void TreinarModelo()
    {
        if (_contexto.Medicamentos == null || !_contexto.Medicamentos.Any())
            throw new InvalidOperationException("Nenhum medicamento cadastrado para treinamento.");

        var medicamentosComSintomas = _contexto.Medicamentos
            .Where(m => m.SintomasTratados != null && m.SintomasTratados.Any(s => !string.IsNullOrWhiteSpace(s)))
            .SelectMany(m => m.SintomasTratados.Select(s => new SintomaMedicamento
            {
                Sintomas = s.Trim(),
                MedicamentoRecomendado = m.Nome
            }))
            .ToList();

        if (!medicamentosComSintomas.Any())
            throw new InvalidOperationException("Nenhum dado válido para treinamento.");

        var dadosTreino = _mlContext.Data.LoadFromEnumerable(medicamentosComSintomas);

        var pipeline = _mlContext.Transforms.Conversion
                .MapValueToKey("Label", nameof(SintomaMedicamento.MedicamentoRecomendado))
            .Append(_mlContext.Transforms.Text
                .FeaturizeText("Features", nameof(SintomaMedicamento.Sintomas)))
            .Append(_mlContext.MulticlassClassification
                .Trainers.SdcaMaximumEntropy("Label", "Features"))
            .Append(_mlContext.Transforms.Conversion
                .MapKeyToValue("PredictedLabel"));

        _modeloTreinado = pipeline.Fit(dadosTreino);
        _motorDePredicao = _mlContext.Model.CreatePredictionEngine<SintomaMedicamento, SintomaPredicao>(_modeloTreinado);

        Console.WriteLine("Modelo treinado com sucesso.");
    }

    public Medicamento RecomendarMedicamento(List<string> sintomasPaciente)
    {
        if (_motorDePredicao == null)
            throw new InvalidOperationException("O modelo ainda não foi treinado.");

        if (sintomasPaciente == null || sintomasPaciente.Count == 0)
            throw new ArgumentException("Nenhum sintoma informado.");

        var resultados = new Dictionary<string, int>();

        foreach (var sintoma in sintomasPaciente)
        {
            var entrada = new SintomaMedicamento { Sintomas = sintoma.Trim() };
            var predicao = _motorDePredicao.Predict(entrada);

            if (!string.IsNullOrWhiteSpace(predicao.MedicamentoRecomendado))
            {
                if (!resultados.ContainsKey(predicao.MedicamentoRecomendado))
                    resultados[predicao.MedicamentoRecomendado] = 0;

                resultados[predicao.MedicamentoRecomendado]++;
            }
        }

        if (!resultados.Any())
            return null;

        string melhorMedicamentoNome = resultados
            .OrderByDescending(r => r.Value)
            .First().Key;

        return _contexto.Medicamentos.FirstOrDefault(m => m.Nome.Equals(melhorMedicamentoNome, StringComparison.OrdinalIgnoreCase));
    }
}
