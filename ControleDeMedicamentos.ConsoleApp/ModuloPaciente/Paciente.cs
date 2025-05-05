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
            return "Paciente válido";
        }
    }

}

