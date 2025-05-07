using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente
{
    public class Paciente : EntidadeBase<Paciente>
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string CartaoSUS { get; set; }
        public string Endereco { get; set; }

        public Paciente(string nome, string cPF, DateTime dataNascimento, string telefone, string endereco, string cartaoSUS)
        {
            Nome = nome;
            CPF = cPF;
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Endereco = endereco;
            CartaoSUS = cartaoSUS;
        }

        public override void AtualizarRegistro(Paciente registroEditado)
        {
            Nome = registroEditado.Nome;
            CPF = registroEditado.CPF;
            DataNascimento = registroEditado.DataNascimento;
            Telefone = registroEditado.Telefone;
            Endereco = registroEditado.Endereco;
            CartaoSUS = registroEditado.CartaoSUS;


        }
        public override string Validar()
        {
            List<string> erros = new();

            if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 3 || Nome.Length > 100)
                erros.Add("O nome deve conter entre 3 e 100 caracteres.");

            if (string.IsNullOrWhiteSpace(Telefone) ||
                !Regex.IsMatch(Telefone, @"^\(\d{2}\)\s?\d{4,5}-\d{4}$"))
                erros.Add("O telefone está em formato inválido. Use (XX) XXXX-XXXX ou (XX) XXXXX-XXXX.");

            if (!Regex.IsMatch(CPF ?? "", @"^\d{11}$"))
                erros.Add("O CPF deve conter exatamente 11 dígitos.");

            if (string.IsNullOrWhiteSpace(CartaoSUS) || CartaoSUS.Length != 15)
                erros.Add("O ID do Cartão SUS deve conter exatamente 15 dígitos.");

            return string.Join(Environment.NewLine, erros);
        }
        public override string ToString()
        {
            return $"ID: {Id} | Nome: {Nome} | Telefone: {Telefone} | CPF: {CPF}";
        }
    }

}

