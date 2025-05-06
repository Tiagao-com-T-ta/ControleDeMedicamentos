using GestaoDeEquipamentos.ConsoleApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.Util
{
    public static class ValidadorEntrada
    {
        public static string ValidarEntrada(string mensagem)
        {
            string entrada;
            do
            {
                Console.Write(mensagem);
                entrada = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(entrada))
                {
                    Notificador.ExibirMensagem("A entrada não pode ser vazia. Tente novamente.", ConsoleColor.Red);
                }

            } while (string.IsNullOrWhiteSpace(entrada));

            return entrada;
        }
        public static int ValidarInteiro(string mensagem)
        {
            int numero;
            string entrada;
            do
            {
                entrada = ValidarEntrada(mensagem);

                if (!int.TryParse(entrada, out numero))
                {
                    Notificador.ExibirMensagem("Valor inválido. Digite um número inteiro válido.", ConsoleColor.Red);
                }
            } while (!int.TryParse(entrada, out numero));

            return numero;
        }

        public static DateTime ValidarData(string mensagem)
        {
            DateTime data;
            string entrada;
            do
            {
                entrada = ValidarEntrada(mensagem);

                if (!DateTime.TryParse(entrada, out data))
                {
                    Notificador.ExibirMensagem("Data inválida. Digite uma data no formato correto (dd/mm/aaaa).", ConsoleColor.Red);
                }
            } while (!DateTime.TryParse(entrada, out data));

            return data;
        }
    }
}