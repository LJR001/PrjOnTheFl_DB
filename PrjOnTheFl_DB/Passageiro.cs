using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrjOnTheFl_DB
{
    internal class Passageiro
    {
        private string Cpf { get; set; }
        private string Nome { get; set; }

        private DateTime DataNascimento;
        private string Sexo { get; set; }

        private DateTime UltimaCompra;
        private DateTime DataCadastro;
        private string Situacao { get; set; }

        ConexaoBanco conexaoBD = new ConexaoBanco();

        //Caminho para acessar o arquivo de passageiros
        //    string caminho = "C:\\Users\\artur\\source\\repos\\ProjetoOnTheFly\\ProjetoOnTheFly\\Dados\\Passageiro.dat";

        public Passageiro()
        {

        }
        public override string ToString()
        {
            return $"{Cpf}{Nome}{DataNascimento}{Sexo}{UltimaCompra}{DataCadastro}{Situacao}";
        }
        #region CadastrarPessoa
        //Cadastra o CPF
        public bool CadastraCpf()
        {
            do
            {
                Console.Write("Digite seu CPF: ");
                Cpf = Console.ReadLine().Replace(".", "").Replace("-", "");
                if (Cpf == "0")
                    return false;
                if (!ValidaCPF(Cpf))
                {
                    Console.WriteLine("Digite um CPF Válido!");
                    Thread.Sleep(2000);
                }
            } while (!ValidaCPF(Cpf));

            //Verificação CPF

            if (conexaoBD.Verificar(Cpf, "CPF", "Passageiro"))
            {
                Console.WriteLine("Este CPF já está cadastrado!!");
                Thread.Sleep(3000);
                return false;
            }
            return true;
        }
        //Cadastra o Nome
        public bool CadastraNome()
        {
            do
            {
                Console.Write("Digite seu Nome (Max 50 caracteres): ");
                Nome = Console.ReadLine();
                if (Nome == "0")
                    return false;
                if (Nome.Length > 50)
                {
                    Console.WriteLine("Infome um nome menor que 50 caracteres!!!!");
                    Thread.Sleep(2000);
                }
            } while (Nome.Length > 50);

            for (int i = Nome.Length; i <= 50; i++)
                Nome += " ";
            return true;
        }
        //Cadastra a data de nascimento
        public bool CadastraDataNasc()
        {
            Console.Write("Digite sua data de nascimento (Mês/Dia/Ano): ");
            DateTime dataNasc;
            while (!DateTime.TryParse(Console.ReadLine(), out dataNasc))
            {
                /* if (DataNascimento.Contains("0"))
                     return false;
                 Console.WriteLine("Formato de data incorreto!");
                 Console.Write("Digite sua data de nascimento (Mês/Dia/Ano): ");*/
            }

            DataNascimento = dataNasc;
            return true;
        }
        //Cadastra o sexo do passageiro
        public bool CadastraSexo()
        {
            do
            {
                Console.WriteLine("Digite seu sexo [M] Masculino / [F] Feminino / [N] Prefere não informar: ");
                Sexo = Console.ReadLine().ToUpper();
                if (Sexo == "0")
                    return false;
                if (Sexo != "M" && Sexo != "N" && Sexo != "F")
                {
                    Console.WriteLine("Digite um opção válida!!!");
                    Thread.Sleep(2000);
                }
            } while (Sexo != "M" && Sexo != "N" && Sexo != "F");
            return true;
        }
        //Altera a situação do cadastro do passageiro
        public bool AlteraSituacao()
        {
            string num;
            do
            {
                Console.Write("Alterar Situação [A] Ativo / [I] Inativo / [0] Cancelar: ");
                num = Console.ReadLine().ToUpper();
                if (num != "A" && num != "I" && num != "0")
                {
                    Console.WriteLine("Digite um opção válida!!!");
                    Thread.Sleep(2000);
                }
            } while (num != "A" && num != "I" && num != "0");

            if (num.Contains("0"))
                return false;
            Situacao = num;
            return true;
        }
        //Cadastra um novo passageiro
        public void CadastraPassageiro()
        {
            Console.WriteLine(">>> CADASTRO DE PASSAGEIRO <<<");
            Console.WriteLine("Para cancelar o cadastro digite 0:\n");

            if (!CadastraCpf())
                return;

            if (!CadastraNome())
                return;

            if (!CadastraDataNasc())
                return;

            if (!CadastraSexo())
                return;

            UltimaCompra = DateTime.Now;

            DataCadastro = DateTime.Now;

            Situacao = "A";

            //Caminho para gravar o novo passageiro
            /*  string caminho = $"C:\\Users\\wessm\\source\\repos\\ProjetoOnTheFly\\ProjetoOnTheFly\\Dados\\Passageiro.dat";
              string texto = $"{ToString()}\n";
              File.AppendAllText(caminho, texto);*/

            string query = $"INSERT INTO Passageiro (CPF,Nome,Data_Nascimento,Sexo,Ultima_Compra,Data_Cadastro,Situacao)" + $"VALUES('{Cpf}','{Nome}','{DataNascimento}','{Sexo}','{UltimaCompra}','{DataCadastro}','{Situacao}')";

            conexaoBD.Insert(query);
            int opc = 1;

            query = $"SELECT CPF,Nome,Data_Nascimento,Sexo,Ultima_Compra,Ultima_Compra,Situacao FROM Passageiro WHERE CPF = {Cpf}";
            conexaoBD.Select(query, opc);

            Console.WriteLine("\nCADASTRO REALIZADO COM SUCESSO!\nPressione Enter para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
        #endregion
        //Altera o cadastro do passageiro
        #region AlteraPessoa
        public void AlteraDadoPassageiro()
        {
            int opc = 1;
            string cpf;
            bool verifica;
            Console.WriteLine(">>> ALTERAR DADOS DE PASSAGEIRO <<<\nPara sair digite 's'.\n");
            Console.Write("Digite o CPF do passageiro: ");

            do
            {

                cpf = Console.ReadLine().Replace(".", "").Replace("-", "");

                if (!ValidaCPF(cpf))
                {
                    Console.WriteLine("CPF inválido!");
                    Thread.Sleep(3000);
                    Console.Write("Digite um CPF do valido: ");
                }
            } while (!ValidaCPF(cpf));

            string query = $"SELECT CPF,Nome,Data_Nascimento,Sexo,Ultima_Compra,Ultima_Compra,Situacao FROM Passageiro WHERE CPF = {cpf}";

            verifica = conexaoBD.Select(query, opc);

            if (verifica)
            {
                int opcao = 0;
                Console.Clear();
                Console.WriteLine(" -- Alterar dados pessoais");
                Console.WriteLine(" Opção 1 : Nome");
                Console.WriteLine(" Opção 2 : Sexo");
                Console.WriteLine(" Opção 3 : Situaçã");
                Console.WriteLine(" Opção 0 : Retorna ao menu anterior ");

                Console.Write("\n Informe a opção: ");
                do
                {
                    try
                    {
                        opcao = int.Parse(Console.ReadLine());

                    }
                    catch (Exception)
                    {

                    }

                    switch (opcao)
                    {
                        case 1:
                            Console.Write(" Digite o novo nome: ");
                            string novonome = Console.ReadLine();
                            string queryupdate = $"UPDATE Passageiro SET Nome = '{novonome}' WHERE CPF ='{cpf}'";
                            conexaoBD.Update(queryupdate);

                            query = $"SELECT CPF,Nome,Data_Nascimento,Sexo,Ultima_Compra,Ultima_Compra,Situacao FROM Passageiro WHERE CPF = {cpf}";

                            conexaoBD.Select(query, opc);


                            break;

                        case 2:
                            Console.Write(" Digite o sexo: ");
                            char altera = char.Parse(Console.ReadLine());
                            queryupdate = $"UPDATE Passageiro SET Sexo = '{altera}' WHERE CPF ='{cpf}'";
                            conexaoBD.Update(queryupdate);

                            query = $"SELECT CPF,Nome,Data_Nascimento,Sexo,Ultima_Compra,Ultima_Compra,Situacao FROM Passageiro WHERE CPF = {cpf}";

                            conexaoBD.Select(query, opc);

                            break;
                        case 3:
                            Console.Write(" Digite o sexo: ");
                            altera = char.Parse(Console.ReadLine());
                            queryupdate = $"UPDATE Passageiro SET Situacao = '{altera}' WHERE CPF ='{cpf}'";
                            conexaoBD.Update(queryupdate);

                            query = $"SELECT CPF,Nome,Data_Nascimento,Sexo,Ultima_Compra,Ultima_Compra,Situacao FROM Passageiro WHERE CPF = {cpf}";

                            conexaoBD.Select(query, opc);

                            break;
                        case 0:
                            Console.WriteLine(" Retornando ao menus anterior");
                            Console.WriteLine();
                            Console.Clear();
                            break;

                        default:
                            Console.WriteLine("Opção invalida !!!!\n Digite uma opção valida");
                            break;
                    }
                } while (opcao > 3);
            }

        }
        #endregion
        //Imprimi os passageiros cadastrados e ativos

        #region ImprimirLocalizarPessoa
        public void ImprimiPassageiros()
        {

            int opc = 1;


            Console.WriteLine(">>> Lista de Passageiros cadastradas <<<\nPara sair digite 's'.\n");


            string query = $"SELECT CPF,Nome,Data_Nascimento,Sexo,Ultima_Compra,Ultima_Compra,Situacao FROM Passageiro";

            conexaoBD.Select(query, opc);

        }


        //localiza um passageiro em específico
        public void LocalizaPassageiro()
        {
            int opc = 1;
            string cpf;

            Console.WriteLine(">>> LOCALIZA DADOS DE PASSAGEIRO <<<\nPara sair digite 's'.\n");
            Console.Write("Digite o CPF do passageiro: ");

            do
            {

                cpf = Console.ReadLine().Replace(".", "").Replace("-", "");

                if (!ValidaCPF(cpf))
                {
                    Console.WriteLine("CPF não encontradp!");
                    Thread.Sleep(3000);
                    Console.Write("Digite outro CPF! : ");
                }
            } while (!ValidaCPF(cpf));

            string query = $"SELECT CPF,Nome,Data_Nascimento,Sexo,Ultima_Compra,Ultima_Compra,Situacao FROM Passageiro WHERE CPF = {cpf}";

            conexaoBD.Select(query, opc);

        }
        #endregion
        /*
        public void DeletaPassageiro()
        {
            int opc = 1;
            string cpf;
            int verifica = 0;
            Console.WriteLine(">>> ALTERAR DADOS DE PASSAGEIRO <<<\nPara sair digite 's'.\n");
            Console.Write("Digite o CPF do passageiro: ");

            do
            {

                cpf = Console.ReadLine().Replace(".", "").Replace("-", "");

                if (!ValidaCPF(cpf))
                {
                    Console.WriteLine("CPF inválido!");
                    Thread.Sleep(3000);
                    Console.Write("Digite um CPF do valido: ");
                }
            } while (!ValidaCPF(cpf));

            string query = $"SELECT CPF,Nome,Data_Nascimento,Sexo,Ultima_Compra,Ultima_Compra,Situacao FROM Passageiro WHERE CPF = {cpf}";

            conexaoBD.Select(query, opc);
            
            Console.WriteLine(" Deseja deletar esse passageiro?\n");
            Console.WriteLine(" Opção 1 : SIM");
            Console.WriteLine(" Opção 0 : NÃO");
            verifica = int.Parse(Console.ReadLine());

            if(verifica == 1)
            {
                Console.WriteLine(" Cadastro deletado");
                query = $"DELETE FROM Passageiro WHERE CPF ='{cpf}'";
                conexaoBD.Delete(query);
            }
            else
            {
                Console.WriteLine(" Cadastro não deletado");
            }
        }*/

        //Valida o CPF
        private static bool ValidaCPF(string vrCPF)
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
