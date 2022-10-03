using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace PrjOnTheFl_DB
{
    internal class Aeronave
    {
        public string Inscricao { get; set; }
        public string CNPJ { get; set; }
        public int Capacidade { get; set; }
        public DateTime UltimaVenda { get; set; }
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; }

        //string Caminho = $"C:\\Users\\artur\\source\\repos\\ProjetoOnTheFly\\ProjetoOnTheFly\\Dados\\Aeronave.dat";

        ConexaoBanco conexaoBD = new ConexaoBanco();

        public Aeronave()
        {
        }
        public Aeronave(string inscricao, int capacidade, DateTime ultimaVenda, DateTime dataCadastro, char situacao)
        {
            Inscricao = inscricao;
            Capacidade = capacidade;

            UltimaVenda = ultimaVenda;
            DataCadastro = dataCadastro;
            Situacao = situacao;
        }
        public void CadastraAeronave()
        {
            Console.WriteLine(">>> CADASTRO DE AERONAVE <<<");


            CadastroCNPJ();

            if (!CadastraIdAeronave())
                return;

            CadastraQtdPassageiros();

            UltimaVenda = DateTime.Now;
            DataCadastro = DateTime.Now;
            Situacao = 'A';

            string query = $"INSERT INTO Aeronave (Inscricao,CNPJ,Capacidade,Data_Cadastro,Ultima_Venda,Situacao)" + $"VALUES('{Inscricao}','{CNPJ}','{Capacidade}','{DataCadastro}','{UltimaVenda}','{Situacao}')";

            conexaoBD.Insert(query);



            Console.ReadKey();

            Console.ReadKey();
        }
        public bool VerificaAeronave(string caminho, string inscricao)
        {
            foreach (string line in File.ReadLines(caminho))
            {
                if (line.Substring(0, 5).Contains(inscricao))
                {
                    return true;
                }
            }
            return false;
        }
        public bool CadastraIdAeronave()
        {
            do
            {
                Console.Write("Informe o código de identificação da aeronave seguindo o padrão definido pela ANAC (XX-XXX):");
                Inscricao = Console.ReadLine().ToUpper().Trim().Replace("-", "");

            } while (Inscricao.Length != 5);


            //if (VerificaAeronave(Caminho, Inscricao))
            //{
            //    Console.WriteLine("Esta Aeronave já está cadastrada!!");
            //    Thread.Sleep(3000);
            //    return false;
            //}

            return true;
        }
        public bool CadastraQtdPassageiros()
        {
            do
            {
                Console.Write("Informe a capacidade de pessoas que a aeronave comporta: ");
                Capacidade = int.Parse(Console.ReadLine());
            } while (Capacidade < 0 || Capacidade > 999);
            //if (int.Parse(Capacidade) > 9 && int.Parse(Capacidade) < 100)
            //{
            //    Capacidade = "0" + Capacidade;
            //}
            //if (int.Parse(Capacidade) < 10)
            //{
            //    Capacidade = "00" + Capacidade;
            //}
            return true;
        }
        public bool AlteraSituacao()
        {
            // string num;
            do
            {
                Console.Write("Alterar Situação [A] Ativo / [I] Inativo / [0] Cancelar: ");
                Situacao = char.Parse(Console.ReadLine());
                //if (num != "A" && num != "I" && num != "0")
                //{
                //    Console.WriteLine("Digite um opção válida!!!");
                //    Thread.Sleep(2000);
                //}
            } while (Situacao != 'A' && Situacao != 'I' && Situacao != '0');
            if (Situacao == '0')
                return false;
            else
                return true;
        }
        public void ImprimeAeronave(string caminho, string inscricao)
        {
            //foreach (string line in File.ReadLines(caminho))
            //{
            //    if (line.Contains(inscricao))
            //    {
            //        Console.WriteLine($"\nInscrição: {line.Substring(0, 2)}-{line.Substring(2, 3)}");
            //        Console.WriteLine($"Capacidade: {line.Substring(5, 3)}");
            //        Console.WriteLine($"Assentos ocupados: {line.Substring(8, 3)}");
            //        Console.WriteLine($"Ultima venda: {line.Substring(11, 2)}/{line.Substring(13, 2)}/{line.Substring(15, 4)}");
            //        Console.WriteLine($"Data do Cadastro: {line.Substring(19, 2)}/{line.Substring(21, 2)}/{line.Substring(23, 4)}");
            //        if (line.Substring(27, 1).Contains("A"))
            //            Console.WriteLine($"Situação: Ativo");
            //        else
            //            Console.WriteLine($"Situação: Inativo");
            //    }
            //}
        }
        public void ImprimerAeronaves()
        {
            int opc = 5;


            Console.WriteLine(">>> Lista de aeronaves cadastradas <<<\nPara sair digite 's'.\n");


            string query = $"SELECT Inscricao,CNPJ,Capacidade,Assentos_Ocupados,Data_Cadastro,Ultima_Venda,Situacao FROM Aeronave ";

            conexaoBD.Select(query, opc);
        }
        public void LocalizaAeronave()
        {
            int opc = 4;
            string inscricao;

            Console.WriteLine(">>> LOCALIZAR AERONAVE <<<\nPara sair digite 's'.\n");
            Console.Write("Digite o a inscrição da aeronave ");

            do
            {
                inscricao = Console.ReadLine().Replace("-", "");

            } while (Inscricao.Length != 5);


            string query = $"SELECT Inscricao,CNPJ,Capacidade,Assentos_Ocupados,Data_Cadastro,Ultima_Venda,Situacao FROM Aeronave WHERE Inscricao = '{inscricao}'";

            conexaoBD.Select(query, opc);

        }
        public void AlteraDadoAeronave()
        {
            int opc = 5;
            string inscricao;
            bool verifica;

            Console.WriteLine(">>> ALTERAR DADOS DA AERONAVE<<<\n");
            Console.Write(" Digite o codigo de inscrição do avião (XX-XXX): ");

            do
            {
                inscricao = Console.ReadLine().Replace("-", "");

            } while (inscricao.Length != 5);


            string query = $"SELECT Inscricao,CNPJ,Capacidade,Assentos_Ocupados,Data_Cadastro,Ultima_Venda,Situacao FROM Aeronave WHERE Inscricao = '{inscricao}'";

            verifica = conexaoBD.Select(query, opc);

            if (verifica)
            {
                int opcao = 0;
                Console.Clear();
                Console.WriteLine(" -- Alterar dados pessoais");
                Console.WriteLine(" Opção 1 : Capacidade");
                Console.WriteLine(" Opção 2 : Situação ");
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
                            Console.Write(" Digite a nova capacidade: ");
                            int novacap = int.Parse(Console.ReadLine());
                            string queryupdate = $"UPDATE Aeronave SET Capacidade = '{novacap}' WHERE Inscricao ='{inscricao}'";
                            conexaoBD.Update(queryupdate);

                            query = $"SELECT Inscricao,CPNJ,Capacidade,Assento_Ocupados,Data_Cadastro,Ultima_Venda,Situacao FROM Aeronave WHERE Inscricao = '{inscricao}'";

                            conexaoBD.Select(query, opc);


                            break;

                        case 2:
                            Console.Write(" Digite a nova sitaução: ");
                            char altera = char.Parse(Console.ReadLine());
                            queryupdate = $"UPDATE Aeronave SET Capacidade = '{altera}' WHERE Inscricao ='{inscricao}'";
                            conexaoBD.Update(queryupdate);

                            query = $"SELECT Inscricao,CPNJ,Capacidade,Assento_Ocupados,Data_Cadastro,Ultima_Venda,Situacao FROM Aeronave WHERE Inscricao = '{inscricao}'";

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
                } while (opcao > 2);
            }
        }


        public bool CadastroCNPJ()
        {
            do
            {
                Console.Write("Informe o CNPJ da qual a aeronave pertence:");
                CNPJ = Console.ReadLine().Replace(".", "").Replace("-", "").Replace("/", "");
                if (conexaoBD.Verificar(CNPJ, "CNPJ", "Companhia"))
                {
                    return true;
                    //Console.WriteLine("Codigo de inscrição existente!");
                    ////Thread.Sleep(2000);
                }
                else
                {
                    Console.WriteLine("CNPJ NÃO ENCONTRADO");
                    CNPJ = "";
                }
            } while (CNPJ.Length == 0);
            return false;
        }

        public override string ToString()
        {
            return $"{Inscricao}{Capacidade}{UltimaVenda}{DataCadastro}{Situacao}";
        }
    }
}