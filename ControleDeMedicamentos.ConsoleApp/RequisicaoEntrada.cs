using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ControleDeMedicamentos.ConsoleApp
{
    public class RequisicaoEntrada : EntidadeBase<RequisicaoEntrada>
    {
        public Medicamento Medicamento { get; set; }

        public Funcionario Funcionario { get; set; }
        public string DateTime {  get; set; }

        public int Quantidade { get; set; }

        public RequisicaoEntrada() { }


        public RequisicaoEntrada(Medicamento medicamento, Funcionario funcionario, int quantidade, DateTime data)
        {
            Medicamento = medicamento;
            Funcionario = funcionario;
            DateTime = DateTime;
            Quantidade = quantidade;
          
        }

        public override void AtualizarRegistro(RequisicaoEntrada registroEditado)
        {
            Medicamento = registroEditado.Medicamento;
            Funcionario = registroEditado.Funcionario;
            DateTime = registroEditado.DateTime;
            Quantidade = registroEditado.Quantidade;
        }

        public override string Validar()
        {
            return string.Empty;
        }

        public override string ToString()
        {
            return $"ID: {Id} | Data: {DateTime} | Medicamento: {Medicamento.Nome} | Funcionário: {Funcionario.Nome}";
        }


    }
}
