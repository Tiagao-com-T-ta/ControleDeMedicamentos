using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRecomendacao
{
    public class TelaRecomendacao
    {
        private readonly ContextoDados _contexto;
        private readonly RecomendacaoMedicamentoService _servicoRecomendacao;

        public TelaRecomendacao(ContextoDados contexto)
        {
            _contexto = contexto;
            _servicoRecomendacao = new RecomendacaoMedicamentoService(contexto);
        }

        public void MostrarMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Recomendação de Medicamentos ===");
                Console.WriteLine("1. Treinar Modelo");
                Console.WriteLine("2. Recomendar Medicamento por Sintomas");
                Console.WriteLine("s. Voltar");

                Console.Write("\nEscolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        TreinarModelo();
                        break;
                    case "2":
                        RecomendarMedicamento();
                        break;
                    case "s":
                    case "S":
                        return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public void TreinarModelo()
        {
            try
            {
                _servicoRecomendacao.TreinarModelo();
                Console.WriteLine("Modelo treinado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao treinar modelo: {ex.Message}");
            }
        }

        public void RecomendarMedicamento()
        {
            try
            {
                Console.WriteLine("\nDigite os sintomas do paciente (um por vez).");
                Console.WriteLine("Digite 'fim' para encerrar a entrada.");

                var sintomas = new List<string>();

                while (true)
                {
                    Console.Write("Sintoma: ");
                    string entrada = Console.ReadLine()?.Trim();

                    if (string.Equals(entrada, "fim", StringComparison.OrdinalIgnoreCase))
                        break;

                    if (!string.IsNullOrWhiteSpace(entrada))
                        sintomas.Add(entrada);
                }

                if (sintomas.Count == 0)
                {
                    Console.WriteLine("Nenhum sintoma informado.");
                    Console.ReadKey();
                    return;
                }

                Medicamento medicamento = _servicoRecomendacao.RecomendarMedicamento(sintomas);

                if (medicamento != null)
                {
                    Console.WriteLine($"\nMedicamento recomendado: {medicamento.Nome}");
                }
                else
                {
                    Console.WriteLine("\nNenhum medicamento encontrado para os sintomas informados.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao recomendar medicamento: {ex.Message}");
            }
        }
    }
}
