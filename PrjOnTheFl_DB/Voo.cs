using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrjOnTheFl_DB
{
    internal class Voo
    {
        public string Id { get; set; }
        public string IdAeronave { get; set; }
        public string Destino { get; set; }

        public int AssentosOcupados { get; set; }
        public string DataVoo { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Situacao { get; set; }

        ConexaoBanco conexaoBD = new ConexaoBanco();

        //string Caminho = $"C:\\Users\\artur\\source\\repos\\ProjetoOnTheFly\\ProjetoOnTheFly\\Dados\\Voo.dat";
        //string CaminhoIata = $"C:\\Users\\artur\\source\\repos\\ProjetoOnTheFly\\ProjetoOnTheFly\\Dados\\ListaIatas.dat";
        //string CaminhoAeronave = $"C:\\Users\\artur\\source\\repos\\ProjetoOnTheFly\\ProjetoOnTheFly\\Dados\\Aeronave.dat";

        public Voo()
        {

        }

        public Voo(string id, string destino, string idAeronave, int assentosacupados, string dataVoo, DateTime dataCadastro, string situacao)
        {
            Id = id;
            Destino = destino;
            IdAeronave = idAeronave;
            AssentosOcupados = assentosacupados;
            DataVoo = dataVoo;
            DataCadastro = dataCadastro;
            Situacao = situacao;
        }
        #region VerificaDados
        public bool BuscarIata(string destino)
        {
            if (conexaoBD.Verificar(destino, "Sigla", "IATA"))
            {
                return true;

            }
            else
            {
                Console.WriteLine("IATA NÃO ENCONTRADA");
                Thread.Sleep(2000);
                return false;
            }

        }



        public bool ColetaAeronave(string idAeronave)
        {

            if (conexaoBD.Verificar(idAeronave, "Inscricao", "Aeronave"))
            {
                //if (!conexaoBD.Verificar(idAeronave, "InsAeronave", "Voo"))
                //{
                //    return true;
                //}
                //else
                //{
                //    Console.WriteLine("AERONAVE EM USO");
                //    return false;
                //}
                return true;

            }
            else
            {
                Console.WriteLine("AERONAVE NÃO ENCONTRADA");
                Thread.Sleep(2000);
                return false;
            }

        }
        #endregion

        #region InsertVoo
        public void CadastrarVoo()
        {
            Console.WriteLine(">>> CADASTRO DE VOO <<<");

            Console.Write("Informe a IATA do aeroporto de destino:");
            Destino = Console.ReadLine().Replace(".", "").Replace("-", "").Replace("/", "");
            if (!BuscarIata(Destino))
                return;

            Console.Write("Informe a inscrição da aeronave desse voo:");
            IdAeronave = Console.ReadLine().Replace(".", "").Replace("-", "").Replace("/", "");
            if (!ColetaAeronave(IdAeronave))
                return;

            if (!GerarIdVoo())
                return;
            AssentosOcupados = 0;

            Console.Write("Informe a data de partida do voo: ");
            DateTime dataVoo;
            while (!DateTime.TryParse(Console.ReadLine(), out dataVoo))
            {
                Console.Write("Informe a data de partida do voo: ");
            }
            Console.Write("Informe a hora de partida do voo: ");
            DateTime horaVoo;
            while (!DateTime.TryParse(Console.ReadLine(), out horaVoo))
            {
                Console.Write("Informe a hora de partida do voo: ");
            }
            DataVoo = dataVoo.ToString("dd-MM-yyyy") + horaVoo.ToString(" HH:mm");


            DataCadastro = DateTime.Now;

            Situacao = "A";



            string query = $"INSERT INTO Voo(Id,InsAeronave,Destino,Assentos_Ocupados,Data_Voo,Data_Cadastro,Situacao)" + $"VALUES('{Id}','{IdAeronave}','{Destino}','{AssentosOcupados}','{DataVoo}','{DataCadastro}','{Situacao}')";

            conexaoBD.Insert(query);

            PassagemVoo passagemVoo = new();
            passagemVoo.GerarPassagem(IdAeronave, Id);


            Console.WriteLine("\n CADASTRO REALIZADO COM SUCESSO!\nPressione Enter para continuar...");
            Console.ReadKey();

        }

        public bool GerarIdVoo()
        {
            do
            {
                Random random = new Random();
                Id = "V" + random.Next(0001, 9999).ToString("0000");

                if (conexaoBD.Verificar(Id, "Id", "Voo"))
                {

                    return false;

                }
                else
                {
                    return true;

                }
            } while (false);
        }
        #endregion
        public bool VerificaVoo(string caminho, string id)
        {
            foreach (string line in File.ReadLines(caminho))
            {
                if (line.Substring(0, 5).Contains(id))
                {
                    return true;
                }
            }
            return false;
        }

        public bool AlteraSituacao()
        {
            string num;
            do
            {
                Console.Write("Alterar Situação [A] Ativo / [C] Inativo / [0] Cancelar: ");
                num = Console.ReadLine().ToUpper();
                if (num != "A" && num != "C" && num != "0")
                {
                    Console.WriteLine("Digite um opção válida!!!");
                    Thread.Sleep(2000);
                }
            } while (num != "A" && num != "C" && num != "0");

            if (num.Contains("0"))
                return false;
            Situacao = num;
            return true;
        }

        #region LocalizaImprimeVoo
        public void LocalizaVoo()
        {
            int opc = 6;
            string idVoo;

            Console.WriteLine(">>> LOCALIZAR VOO <<<\nPara sair digite 's'.\n");
            Console.Write("Digite oo Id do voo: ");

            do
            {
                idVoo = Console.ReadLine().Replace("-", "");

            } while (idVoo.Length != 5);


            string query = $"SELECT Id,InsAeronave,Destino,Assentos_Ocupados,Data_Voo,Data_Cadastro,Situacao FROM Voo WHERE Id = '{idVoo}'";

            conexaoBD.Select(query, opc);
        }

        public void ImprimeVoos()
        {
            int opc = 6;


            Console.WriteLine(">>> Lista de voos cadastradas <<<\nPara sair digite 's'.\n");


            string query = $"SELECT Id,InsAeronave,Destino,Assentos_Ocupados,Data_Voo,Data_Cadastro,Situacao FROM Voo";

            conexaoBD.Select(query, opc);
        }
        #endregion

        #region AlteraVoo
        public void AlteraDadoVoo()
        {
            int opc = 6;
            string IdVoo;
            bool verifica;

            Console.WriteLine(">>> ALTERAR DADOS DO VOO<<<\n");
            Console.Write(" Digite o codigo de inscrição do Voo: ");

            do
            {
                IdVoo = Console.ReadLine().Replace("-", "");

            } while (IdVoo.Length != 5);


            string query = $"SELECT Id,InsAeronave,Destino,Assentos_Ocupados,Data_Voo,Data_Cadastro,Situacao FROM Voo WHERE Id = '{IdVoo}'";

            verifica = conexaoBD.Select(query, opc);

            if (verifica)
            {
                int opcao = 0;
                Console.Clear();
                Console.WriteLine(" -- Alterar dados pessoais");
                Console.WriteLine(" Opção 1 : Destino");
                Console.WriteLine(" Opção 2 : Aeronave");
                Console.WriteLine(" Opção 3 : Situação ");
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
                            Console.Write(" Digite a nova destino: ");
                            string novaiata = Console.ReadLine();
                            //if (!ColetaDestino())
                            //    return;
                            string queryupdate = $"UPDATE Voo SET Destino = '{novaiata}' WHERE Id ='{IdVoo}'";
                            conexaoBD.Update(queryupdate);

                            query = $"SELECT Id,InsAeronave,Destino,Assentos_Ocupados,Data_Voo,Data_Cadastro,Situacao FROM Voo WHERE Id = '{IdVoo}'";

                            conexaoBD.Select(query, opc);

                            break;
                        case 2:
                            Console.Write(" Digite o id para alterar o avião: ");
                            int alteraaeronave = int.Parse(Console.ReadLine());
                            queryupdate = $"UPDATE Voo SET Destino = '{alteraaeronave}' WHERE Id ='{IdVoo}'";
                            conexaoBD.Update(queryupdate);

                            query = $"SELECT Id,InsAeronave,Destino,Assentos_Ocupados,Data_Voo,Data_Cadastro,Situacao FROM Voo WHERE Id = '{IdVoo}'";

                            conexaoBD.Select(query, opc);
                            break;

                        case 3:
                            Console.Write(" Digite a nova situacao: ");
                            int alterasitaucao = int.Parse(Console.ReadLine());
                            queryupdate = $"UPDATE Voo SET Destino = '{alterasitaucao}' WHERE Id ='{IdVoo}'";
                            conexaoBD.Update(queryupdate);

                            query = $"SELECT Id,InsAeronave,Destino,Assentos_Ocupados,Data_Voo,Data_Cadastro,Situacao FROM Voo WHERE Id = '{IdVoo}'";
                            break;
                    }
                } while (opcao > 3);
            }

        }



        #endregion
        public override string ToString()
        {
            return $"{Id}{Destino}{IdAeronave}{AssentosOcupados}{DataVoo}{DataCadastro}{Situacao}";
        }
    }
}
