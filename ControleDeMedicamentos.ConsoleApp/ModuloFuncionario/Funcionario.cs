﻿using GestaoDeEquipamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionario
{
    public class Funcionario : EntidadeBase<Funcionario>
    {
        public string Nome { get; set; }

        public string Telefone { get; set; }

        public string CPF { get; set; }

        public Funcionario(string nome, string telefone, string cpf)
        {
            Nome = nome;
            Telefone = telefone;
            CPF = cpf;
        }
        public override void AtualizarRegistro(Funcionario registroEditado)
        {
            Nome = registroEditado.Nome;
            Telefone = registroEditado.Telefone;
            CPF = registroEditado.CPF;
           
        }

        public override string Validar()
        {
            return "";
        }
    }
}
