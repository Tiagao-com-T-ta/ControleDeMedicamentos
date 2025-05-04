using Microsoft.ML.Data;

namespace ControleDeMedicamentos.ConsoleApp.Data
{
  
    public class SintomaMedicamento
    {
        [LoadColumn(0)]
        public string Sintomas { get; set; }

        [LoadColumn(1)]
        public string MedicamentoRecomendado { get; set; }
    }

  
    public class SintomaPredicao
    {
        [ColumnName("PredictedLabel")]
        public string MedicamentoRecomendado { get; set; }

        public float[] Score { get; set; }
    }
}