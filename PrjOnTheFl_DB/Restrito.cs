using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrjOnTheFl_DB
{
    internal class Restrito
    {
        public string Cpf { get; set; }

        ConexaoBanco conexaoBD = new ConexaoBanco();

        // string Caminho = $"C:\\Users\\artur\\source\\repos\\ProjetoOnTheFly\\ProjetoOnTheFly\\Dados\\Restritos.dat";
        public void GerarMenu()
        {

            int opc = 5;
            bool retorna = true;

            Console.WriteLine(" Digite a opção: \n" +
                "1 - Adicionar CPF\n" +
                "2 - Verificar CPF\n" +
                "3 - Remover CPF\n" +
                "4 - Voltar ao Menu Restritos\n" +
                "0 - Voltar ao Menu Anterior");

            do
            {
                try
                {
                    opc = int.Parse(Console.ReadLine());

                }
                catch (Exception)
                {
                }

                switch (opc)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        AddRestricaoCpf();
                        break;
                    case 2:

                        break;
                    case 3:
                        RetirarRestricaoCpf();
                        break;
                    case 4:
                        ListarCpfRestringido();
                        break;
                    default: break;
                }
            } while (retorna);
        }

        public void ListarCpfRestringido()
        {
            int opc = 2;

            Console.Clear();
            Console.WriteLine(" Lista de CPF restingido ");

            string query = $"SELECT CPF FROM Restrito ";

            conexaoBD.Select(query, opc);

        }

        public void VerificarCnpj()
        {
            int opc = 2;
            bool verifica;
            {
                bool validar;
                do
                {
                    Console.WriteLine(" Digite o CPF para ser verificado ");
                    Cpf = Console.ReadLine().Replace(".", "").Replace("-", "");
                    validar = ValidarCPF(Cpf);
                    if (validar)
                    {
                        string query = $"SELECT CNPJ FROM Restrito WHERE CPF ={Cpf}";

                        verifica = conexaoBD.Select(query, opc);
                        if (verifica)
                        {
                            Console.WriteLine($" Está Restrito ");
                        }
                        else
                        {
                            Console.WriteLine($"\n '{Cpf}' Não está bloqueado");
                        }

                        Console.WriteLine("\n CNPJ foi adicionado a lista de bloqueado");
                        Console.ReadKey();
                    }
                    else if (!validar)
                    {
                        Console.WriteLine(" CPF invalido");
                        Console.WriteLine(" \nDigite um CPF valido\n");
                    }

                } while (validar == false);

            }
        }

        public void AddRestricaoCpf()
        {
            bool validar;

            do
            {
                Console.WriteLine(" Digite o CPF que sera bloqueado");
                Cpf = Console.ReadLine().Replace(".", "").Replace("-", "");
                validar = ValidarCPF(Cpf);
                if (validar)
                {



                    string query = $"INSERT INTO Restrito (CPF) VALUES('{Cpf}')";

                    conexaoBD.Insert(query);

                    Console.WriteLine($"\n CPF '{Cpf}' foi restringido\n Pressione ENTER para continuar");
                    Console.ReadKey();
                }
                else if (validar == false)
                {
                    Console.WriteLine(" CPF invalido");

                    Console.WriteLine(" Digite um CPF valido\n");
                }
            } while (validar == false);

        }

        public void RetirarRestricaoCpf()
        {
            bool validar;

            do
            {
                Console.WriteLine(" Digite o CPF que sera bloqueado");
                Cpf = Console.ReadLine().Replace(".", "").Replace("-", "");
                validar = ValidarCPF(Cpf);
                if (validar)
                {



                    string query = $"DELETE FROM Restrito WHERE CPF=('{Cpf}')";

                    conexaoBD.Insert(query);

                    Console.WriteLine($"\n Foi retirado a restrição do '{Cpf}' \n Pressione ENTER para continuar");
                    Console.ReadKey();
                }
                else if (validar == false)
                {
                    Console.WriteLine(" CPF invalido");

                    Console.WriteLine(" Digite um CPF valido\n");
                }
            } while (validar == false);
        }


        public override string ToString()
        {
            return $"{Cpf}";
        }

        private static bool ValidarCPF(string vrCPF)

        {
            string valor = vrCPF.Replace(".", "");

            valor = valor.Replace("-", "");

            if (valor.Length != 11)
                return false;

            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)

                if (valor[i] != valor[0])

                    igual = false;

            if (igual || valor == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)

                numeros[i] = int.Parse(

                  valor[i].ToString());

            int soma = 0;

            for (int i = 0; i < 9; i++)

                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)

            {
                if (numeros[9] != 0)
                    return false;
            }

            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;

            for (int i = 0; i < 10; i++)

                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)

            {
                if (numeros[10] != 0)
                    return false;
            }

            else
                if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }

    }
}
