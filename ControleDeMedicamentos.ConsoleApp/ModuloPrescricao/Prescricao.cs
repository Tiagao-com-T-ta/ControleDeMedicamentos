using ControleDeMedicamentos.ConsoleApp.ControleDeMedicamentos.ConsoleApp;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPrescricao
{
    public class Prescricao : EntidadeBase<Prescricao>
    {
        public string CrmMedico { get; set; }
        public DateTime Data { get; set; }
        public List<ItemPrescricao> Medicamentos { get; set; }
        public bool Validada { get; set; }
        public Paciente Paciente { get; set; }

        public Prescricao(string crmMedico, DateTime data, List<ItemPrescricao> medicamentos)
        {
            CrmMedico = crmMedico;
            Data = data;
            Medicamentos = medicamentos;
            Validada = false;
            Paciente = null!;
        }

        public override void AtualizarRegistro(Prescricao registroEditado)
        {
            CrmMedico = registroEditado.CrmMedico;
            Data = registroEditado.Data;
            Medicamentos = registroEditado.Medicamentos;
            Validada = registroEditado.Validada;
        }

        public override string Validar()
        {
            List<string> erros = new();

            if (!Regex.IsMatch(CrmMedico ?? "", @"^\d{6}$"))
                erros.Add("CRM deve conter exatamente 6 dígitos.");

            if (Data == DateTime.MinValue || Data > DateTime.Now)
                erros.Add("Data inválida.");

            if (Medicamentos == null || Medicamentos.Count == 0)
                erros.Add("Deve conter pelo menos um medicamento.");

            return string.Join(Environment.NewLine, erros);
        }

        public string ValidarEstoque(IRepositorioMedicamento repositorioMedicamento)
        {
            foreach (var item in Medicamentos)
            {
                var medicamento = repositorioMedicamento.SelecionarRegistroPorId(item.MedicamentoId);
                if (medicamento == null || medicamento.Quantidade < item.Quantidade)
                {
                    return $"Medicamento {medicamento?.Nome} não disponível em quantidade suficiente.";
                }
            }
            return string.Empty;
        }

        public override string ToString()
        {
            return $"ID: {Id} | CRM: {CrmMedico} | Data: {Data:dd/MM/yyyy} | Validada: {(Validada ? "Sim" : "Não")}";
        }
    }

    public class ItemPrescricao
    {
        public int MedicamentoId { get; set; }
        public string? Dosagem { get; set; }
        public string? Periodo { get; set; }
        public int Quantidade { get; set; }
    }
}