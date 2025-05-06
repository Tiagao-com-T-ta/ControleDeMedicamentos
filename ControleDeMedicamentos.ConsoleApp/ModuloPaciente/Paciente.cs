using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente
{
    public class Paciente : EntidadeBase<Paciente>
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public override void AtualizarRegistro(Paciente registroEditado)
        {
            Nome = registroEditado.Nome;
            CPF = registroEditado.CPF;
            DataNascimento = registroEditado.DataNascimento;
            Telefone = registroEditado.Telefone;
            Endereco = registroEditado.Endereco;
        }
        public override string Validar()
        {
            List<string> erros = new();

            if (string.IsNullOrEmpty(Nome))
                erros.Add("O campo Nome é obrigatório");
            if (string.IsNullOrEmpty(CPF))
                erros.Add("O campo CPF é obrigatório");
            if (string.IsNullOrEmpty(Endereco))
                erros.Add("O campo Endereço é obrigatório");
            if (DataNascimento == DateTime.MinValue)
                erros.Add("O campo Data de Nascimento é obrigatório");
            if (DataNascimento > DateTime.Now)
                erros.Add("A data de nascimento não pode ser maior que a data atual");
            if (DataNascimento < DateTime.Now.AddYears(-120))
                erros.Add("A data de nascimento não pode ser menor que 120 anos atrás");
            if (string.IsNullOrEmpty(Telefone))
                erros.Add("O campo Telefone é obrigatório");

            if (erros.Count > 0)
                return string.Join(Environment.NewLine, erros);

            else return "Paciente válido";
        }
    }

}

