using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.Compartilhado
{
    public class RegistroClinico : EntidadeBase<RegistroClinico>
    {
        public int IdPaciente { get; set; }
        public DateTime DataRegistro { get; set; }
        public string Descricao { get; set; }
        public string Observacoes { get; set; }
        public string Diagnostico { get; set; }

        public RegistroClinico(int idPaciente, DateTime dataRegistro, string descricao, string observacoes, string diagnostico)
        {
            IdPaciente = idPaciente;
            DataRegistro = dataRegistro;
            Descricao = descricao;
            Observacoes = observacoes;
            Diagnostico = diagnostico;
        }

        public override void AtualizarRegistro(RegistroClinico registroEditado)
        {
            DataRegistro = registroEditado.DataRegistro;
            Descricao = registroEditado.Descricao;
            Observacoes = registroEditado.Observacoes;
            Diagnostico = registroEditado.Diagnostico;
        }

        public override string Validar()
        {
            List<string> erros = new();

            if (string.IsNullOrWhiteSpace(Descricao))
                erros.Add("Descrição é obrigatória.");

            if (string.IsNullOrWhiteSpace(Diagnostico))
                erros.Add("Diagnóstico é obrigatório.");

            return string.Join(Environment.NewLine, erros);
        }
    }
}
