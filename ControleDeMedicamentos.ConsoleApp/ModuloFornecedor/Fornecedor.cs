using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedor
{
    public class Fornecedor : EntidadeBase<Fornecedor>
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CNPJ { get; set; }
        public Fornecedor() { }

        public Fornecedor(string nome, string telefone, string cnpj)
        {
            Nome = nome;
            Telefone = telefone;
            CNPJ = cnpj;
        }
        public override void AtualizarRegistro(Fornecedor registroEditado)
        {
            Nome = registroEditado.Nome;
            Telefone = registroEditado.Telefone;
            CNPJ = registroEditado.CNPJ;
        }

        public override string Validar()
        {
            List<string> erros = new();

            if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 3 || Nome.Length > 100)
                erros.Add("O nome deve conter entre 3 e 100 caracteres.");

            if (!Regex.IsMatch(Telefone ?? "", @"^\(?\d{2}\)?\s?\d{4,5}-\d{4}$"))
                erros.Add("O telefone está em formato inválido. Use (XX) XXXX-XXXX ou (XX) XXXXX-XXXX.");

            if (!Regex.IsMatch(CNPJ ?? "", @"^\d{14}$"))
                erros.Add("O CNPJ deve conter exatamente 14 dígitos.");

            return string.Join(Environment.NewLine, erros);
        }

        public override string ToString()
        {
            return $"ID: {Id} | Nome: {Nome} | Telefone: {Telefone} | CNPJ: {CNPJ}";
        }
    }
}