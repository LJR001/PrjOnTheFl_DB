using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrjOnTheFl_DB
{
    internal class PassagemVoo
    {
        public string Id { get; set; }

        public string InsAeronave { get; set; }

        // public DateTime MyProperty { get; set; }

        public DateTime DataCadastro { get; set; }
        public double Valor { get; set; }
        public char Situacao { get; set; }

        ConexaoBanco conexaoBD = new ConexaoBanco();

        public PassagemVoo()
        {

        }

        public void GerarPassagem(string idAeronave, string idVoo)
        {

            float valorPas;
            //DateTime Data_Voo;

            int capacidade = BuscaEspecifica(idAeronave, "Inscricao", "Aeronave", "Capacidade");

            InsAeronave = idAeronave;

            Console.WriteLine(" Digite o valor da Passagem: ");
            valorPas = float.Parse(Console.ReadLine().Replace(",", "."));




            int i = 0;
            string idUnitario;
            for (i = 1; i <= capacidade; i++)
            {

                //int id = Random(0001, 9999);
                if (i < 10)
                {
                    idUnitario = $"PA000{i}";
                }
                else if (i < 100)
                {
                    idUnitario = $"PA00{i}";

                }
                else if (i < 1000)
                {
                    idUnitario = $"PA0{i}";
                }
                else
                {
                    idUnitario = $"PA{i}";
                }

                string IdPassagem = $"{idUnitario}{idVoo}";

                Id = IdPassagem;
                Valor = valorPas;

                // Data_Voo = BuscaEspecifica(idAeronave, "Inscricao", "Aeronave", "Capacidade");

                DataCadastro = DateTime.Now;

                Situacao = 'L';

                //Console.WriteLine(ToString());
                //string caminho = Caminho;
                //string texto = $"{ToString()}\n";
                //File.AppendAllText(caminho, texto);

                string query = $"INSERT INTO Passagem(IdPassagem,IdVoo,InsAeronave,Data_Voo,Valor,Situacao)" + $"VALUES('{Id}','{idVoo}','{InsAeronave}','{DataCadastro}','{Valor}','{Situacao}')";

                conexaoBD.Insert(query);


                // Console.ReadKey();
            }
        }

        public void GerarMenu()
        {
            Console.WriteLine(" Digite a opção: \n" +
               " 1 - Ver passagens\n" +
               " 2 - Alterar preço da passagem\n" +
               " 3 - Listar passagem");

            int opc = int.Parse(Console.ReadLine());
            switch (opc)
            {
                case 1:
                    ImprimirPassagens();
                    break;
                case 2:
                    AlteraPassagem();
                    break;
                case 3:
                    LocalPassagem();
                    break;

                default: break;
            }
        }

        public void LocalPassagem()
        {
            int opc = 6;
            string idPessoa;
            //bool verifica;
            Console.WriteLine(">>> LOCALIZA DADOS DE PASSAGEM <<<\nPara sair digite 's'.\n");
            Console.Write("Digite o codigo da passagem: ");

            do
            {

                idPessoa = Console.ReadLine().Replace(".", "").Replace("-", "");

                if (!VerificacaoEspecifica(idPessoa, "IdPassagem", "Passagem"))
                {
                    Console.WriteLine("CPF inválido!");
                    Thread.Sleep(3000);
                    Console.Write("Digite um CPF do valido: ");
                }
            } while (!VerificacaoEspecifica(Id, "IdPassagem", "Passagem"));

            string query = $"SELECT IdPassagem,InsAeronave,Data_Voo,Valor,Situacao FROM Passagem WHERE IdPassagem = {idPessoa}";

            conexaoBD.Select(query, opc);
        }

        public void AlteraPassagem()
        {
            int opc = 6;
            string idPessoa;
            bool verifica;
            Console.WriteLine(">>> ALTERAR DADOS DE PASSAGEM <<<\nPara sair digite 's'.\n");
            Console.Write("Digite o codigo da passagem: ");

            do
            {

                idPessoa = Console.ReadLine().Replace(".", "").Replace("-", "");

                if (!VerificacaoEspecifica(idPessoa, "IdPassagem", "Passagem"))
                {
                    Console.WriteLine("CPF inválido!");
                    Thread.Sleep(3000);
                    Console.Write("Digite um CPF do valido: ");
                }
            } while (!VerificacaoEspecifica(Id, "IdPassagem", "Passagem"));

            string query = $"SELECT IdPassagem,InsAeronave,Data_Voo,Valor,Situacao FROM Passagem WHERE IdPassagem = {idPessoa}";

            verifica = conexaoBD.Select(query, opc);

            if (verifica)
            {
                int opcao = 0;
                Console.Clear();
                Console.WriteLine(" -- Alterar dados de passagem");
                Console.WriteLine(" Opção 1 : Aeronave");
                Console.WriteLine(" Opção 2 : Valor");
                Console.WriteLine(" Opção 3 : Situação");
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
                            Console.Write(" Digite o a inscrição da aeronave: ");
                            string novaInscricao = Console.ReadLine();
                            string queryupdate = $"UPDATE Passagem SET Aeronave = '{novaInscricao}' WHERE CPF ='{idPessoa}'";
                            conexaoBD.Update(queryupdate);

                            query = $"SELECT IdPassagem,InsAeronave,Data_Voo,Valor,Situacao FROM Passagem WHERE IdPassagem = {idPessoa}";

                            conexaoBD.Select(query, opc);


                            break;

                        case 2:
                            Console.Write(" Digite o sexo: ");
                            char altera = char.Parse(Console.ReadLine());
                            queryupdate = $"UPDATE Passagem SET Valor = '{altera}' WHERE CPF ='{idPessoa}'";
                            conexaoBD.Update(queryupdate);

                            query = $"SELECT IdPassagem,InsAeronave,Data_Voo,Valor,Situacao FROM Passagem WHERE IdPassagem = {idPessoa}";

                            conexaoBD.Select(query, opc);

                            break;
                        case 3:
                            Console.Write(" Digite o sexo: ");
                            altera = char.Parse(Console.ReadLine());
                            queryupdate = $"UPDATE Passagemo SET Situacao = '{altera}' WHERE CPF ='{idPessoa}'";
                            conexaoBD.Update(queryupdate);

                            query = $"SELECT IdPassagem,InsAeronave,Data_Voo,Valor,Situacao FROM Passagem WHERE IdPassagem = {idPessoa}";

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

        public void ImprimirPassagens()
        {

            int opc = 6;


            Console.WriteLine(">>> Lista de Passagem cadastradas <<<\nPara sair digite 's'.\n");


            string query = $"SELECT IdPassagem,InsAeronave,Data_Voo,Valor,Situacao FROM Passagem";

            conexaoBD.Select(query, opc);

        }

        public bool VerificacaoEspecifica(string dado, string ColChave, string Tabela)
        {
            if (conexaoBD.Verificar(dado, ColChave, Tabela))
            {
                return true;

            }
            else
            {
                Console.WriteLine($"{dado} NÃO ENCONTRADA NO BANCO DE DADOS");
                Thread.Sleep(2000);
                return false;
            }

        }


        public int BuscaEspecifica(string chave, string ColChave, string Tabela, string Coluna)
        {

            int recebido;


            recebido = conexaoBD.BuscaCapacide(chave, ColChave, Tabela, Coluna);


            return recebido;


        }

        public override string ToString()
        {
            return $"{Id}{DataCadastro}{Valor}{Situacao}";
        }

        public int Random(int min, int Max)
        {
            // bool repetido =  false;
            Random r = new Random();
            /* do
             {
                 foreach (string linha in File.ReadLines(Caminho))
                 {
                     if (linha.Contains("PassagemVoo") & linha.Contains(r))
                     {
                         repetido = true;
                         break;
                     }
                 }
             } while (repetido);*/
            return r.Next(0001, 9999);

        }
        public void AlterarSituação()
        {
            char situacao;
            do
            {
                Console.WriteLine(" Alterar para Reservada ou Vendida uma passagem:\n L -  Livre \n P - Paga\n R - Reservada");
                situacao = char.Parse(Console.ReadLine().ToUpper());
                if ((situacao != 'V') && (situacao != 'R') && (situacao != 'L'))
                {
                    Console.WriteLine(" Opção invalida!!!");
                }
            } while ((situacao != 'V') && (situacao != 'R') && (situacao != 'L'));
        }

       



        //public int BuscaEspecificaData(string chave, string ColChave, string Tabela, string Coluna)
        //{

        //    int capacidade = 0;


        //    capacidade = conexaoBD.BuscaEspecifica(chave, ColChave, Tabela, Coluna);


        //    return capacidade;


        //}

    }
}

