using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricao;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicao
{
    public class RequisicaoSaida : EntidadeBase<RequisicaoSaida>
    {
        public DateTime Data { get; set; }
        public Paciente Paciente { get; set; }
        public Prescricao Prescricao { get; set; }
        public List<ItemRequisicao> Itens { get; set; } = new();

        public override void AtualizarRegistro(RequisicaoSaida registroEditado)
        {
            Data = registroEditado.Data;
            Paciente = registroEditado.Paciente;
            Prescricao = registroEditado.Prescricao;
            Itens = registroEditado.Itens;
        }

        public override string Validar()
        {
            List<string> erros = new();

            if (Data == default)
                erros.Add("A data da requisição é obrigatória.");

            if (Paciente == null)
                erros.Add("O paciente é obrigatório.");

            if (Prescricao == null)
                erros.Add("A prescrição médica é obrigatória.");

            if (Itens == null || Itens.Count == 0)
                erros.Add("Deve conter pelo menos um medicamento requisitado.");

            foreach (var item in Itens)
            {
                if (item.Quantidade <= 0)
                    erros.Add($"A quantidade de '{item.Medicamento?.Nome}' deve ser maior que zero.");
            }

            return string.Join(Environment.NewLine, erros);
        }

        public override string ToString()
        {
            return $"ID: {Id} | Data: {Data:dd/MM/yyyy} | Paciente: {Paciente?.Nome} | Prescrição ID: {Prescricao?.Id} | Itens: {Itens.Count}";
        }
    }
}