using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjOnTheFl_DB
{
    internal class ConexaoBanco
    {

        private static string Conexao = "Data Source=localhost;Initial Catalog=Aeroporto;User id=sa;Password=eXPERT.87;";

        private static SqlConnection Conexaosql = new SqlConnection(Conexao);


        public ConexaoBanco()
        {

        }
        public string Caminho()
        {
            return Conexao;
        }
        public void Insert(string query)
        {
            try
            {
                Conexaosql.Open();
                SqlCommand cmd = new SqlCommand(query, Conexaosql);
                cmd.ExecuteNonQuery();
                Conexaosql.Close();

            }
            catch (Exception ex)
            {

                Console.WriteLine(" Erro ao se comunicar com o banco\n" + ex.Message);


                Console.WriteLine("\nPressione ENTER para continuar");
                Console.ReadKey();
                Console.Clear();
                Conexaosql.Close();
            }

        }
        public bool Select(string query, int opc)
        {
            bool retorna = false;
            try
            {
                Conexaosql.Open();
                SqlCommand cmd = new SqlCommand(query, Conexaosql);
                cmd.ExecuteNonQuery();

                switch (opc)
                {
                    case 1:
                        using (SqlDataReader leitor = cmd.ExecuteReader())
                        {

                            while (leitor.Read())
                            {
                                Console.WriteLine("\nCADASTRO DE PASSAGEIRO ENCONTRADO!\n");

                                Console.Write(" CPF: {0}", leitor.GetString(0));
                                Console.Write("\n Nome: {0} ", leitor.GetString(1));
                                Console.Write("\n Data de Nascimento: {0} ", leitor.GetDateTime(2).ToString("dd/MM/yyyy"));
                                Console.Write("\n Sexo:{0} ", leitor.GetString(3));
                                Console.Write("\n Ultima compra: {0} ", leitor.GetDateTime(4).ToString("dd/MM/yyyy"));
                                Console.Write("\n Data de cadastro: {0} ", leitor.GetDateTime(5).ToString("dd/MM/yyyy"));
                                Console.Write("\n Situação: {0} ", leitor.GetString(6));

                                Console.WriteLine("\nPressione Enter para continuar...");

                                retorna = true;
                                Console.ReadKey();

                                Console.Clear();
                            }

                        }

                        break;

                    case 2:
                        using (SqlDataReader leitor = cmd.ExecuteReader())
                        {


                            while (leitor.Read())
                            {


                                Console.Write(" CPF: {0}", leitor.GetString(0));


                            }
                            Console.WriteLine("\n\nPressione Enter para continuar...");

                            retorna = true;
                            Console.ReadKey();
                            Console.Clear();
                        }

                        break;

                    case 3:
                        using (SqlDataReader leitor = cmd.ExecuteReader())
                        {


                            while (leitor.Read())
                            {


                                Console.Write(" CNPJ: {0}", leitor.GetString(0));


                            }
                            Console.WriteLine("\n\nPressione Enter para continuar...");

                            retorna = true;
                            Console.ReadKey();
                            Console.Clear();
                        }

                        break;

                    case 4:
                        using (SqlDataReader leitor = cmd.ExecuteReader())
                        {

                            while (leitor.Read())
                            {
                                Console.WriteLine("\nCADASTRO COMPANHIA ENCONTRADO!\n");

                                Console.Write(" CNPJ: {0}", leitor.GetString(0));
                                Console.Write("\n Razao Social: {0} ", leitor.GetString(1));
                                Console.Write("\n Data de Abertura: {0} ", leitor.GetDateTime(2).ToString("dd/MM/yyyy"));
                                Console.Write("\n Ultimo Voo: {0} ", leitor.GetDateTime(3).ToString("dd/MM/yyyy"));
                                Console.Write("\n Data Cadastro: {0} ", leitor.GetDateTime(4).ToString("dd/MM/yyyy"));
                                Console.Write("\n Situacao: {0} ", leitor.GetString(5));

                                Console.WriteLine("\nPressione Enter para continuar...");

                                retorna = true;
                                Console.ReadKey();
                                Console.Clear();
                            }

                        }

                        break;
                    case 5:
                        using (SqlDataReader leitor = cmd.ExecuteReader())
                        {

                            while (leitor.Read())
                            {
                                Console.WriteLine("\nCADASTRO DE AERONAVE ENCONTRADO!\n");

                                Console.Write(" Inscricao: {0}", leitor.GetString(0));
                                Console.Write("\n CNPJ: {0} ", leitor.GetString(1));
                                Console.Write("\n Capacidade: {0} ", leitor.GetInt32(2));

                                Console.Write("\n Data Cadastro: {0} ", leitor.GetDateTime(3).ToString("dd/MM/yyyy"));
                                Console.Write("\n Ultima compra: {0} ", leitor.GetDateTime(4).ToString("dd/MM/yyyy"));
                                Console.Write("\n Situação: {0} ", leitor.GetString(5));

                                Console.WriteLine("\nPressione Enter para continuar...");

                                retorna = true;
                                Console.ReadKey();

                                Console.Clear();
                            }

                        }
                        break;
                    case 6:
                        using (SqlDataReader leitor = cmd.ExecuteReader())
                        {

                            while (leitor.Read())
                            {
                                Console.WriteLine("\nCADASTRO DE PASSAGEM ENCONTRADO!\n");

                                Console.Write(" Codigo da Passagem: {0}", leitor.GetString(0));
                                Console.Write("\n Inscricão da aeronave: {0} ", leitor.GetString(1));
                                Console.Write("\n Destino: {0} ", leitor.GetDateTime(2).ToString("dd/MM/yyyy"));
                                Console.Write("\n Assentos ocupados: {0} ", leitor.GetDouble(3));
                                Console.Write("\n Data Cadastro: {0} ", leitor.GetString(4));
                                

                                Console.WriteLine("\nPressione Enter para continuar...");

                                retorna = true;
                                Console.ReadKey();

                                Console.Clear();
                            }

                        }
                        break;

                    case 7:

                        break;



                    case 0:
                        using (SqlDataReader leitor = cmd.ExecuteReader())
                        {


                            if (leitor.Read())
                            {
                                Console.WriteLine(" {0} ja existe em nosso banco de dados ", leitor.GetString(0));
                            }
                            Console.WriteLine("\n\nPressione Enter para continuar...");

                            retorna = true;
                            Console.ReadKey();
                            Console.Clear();
                        }
                        break;
                    default:
                        break;


                }
                cmd.ExecuteNonQuery();
                Conexaosql.Close();
                return retorna;

            }
            catch (Exception ex)
            {

                Console.WriteLine(" Erro ao se comunicar com o banco\n" + ex.Message);
                Conexaosql.Close();

                Console.WriteLine("\nPressione ENTER para continuar");
                Console.ReadKey();
                Console.Clear();
                return false;
            }
        }
        public void Update(string query)
        {
            try
            {
                Conexaosql.Open();
                SqlCommand cmd = new SqlCommand(query, Conexaosql);
                cmd.ExecuteNonQuery();
                Conexaosql.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine(" Erro ao se comunicar com o banco\n" + ex.Message);
                Conexaosql.Close();

                Console.WriteLine("\nPressione ENTER para continuar");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void Delete(string query)
        {
            try
            {
                Conexaosql.Open();
                SqlCommand cmd = new SqlCommand(query, Conexaosql);
                cmd.ExecuteNonQuery();
                Conexaosql.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine(" Erro ao se comunicar com o banco\n" + ex.Message);
                Conexaosql.Close();

                Console.WriteLine("\nPressione ENTER para continuar");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public bool Verificar(string dado, string campo, string tabela)
        {
            string queryString = $"SELECT {campo} FROM {tabela} WHERE {campo} = '{dado}'";
            try
            {
                Conexaosql.Open();
                SqlCommand cmd = new SqlCommand(queryString, Conexaosql);
                cmd.ExecuteNonQuery();


                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Conexaosql.Close();
                        return true;
                    }
                    else
                    {
                        Conexaosql.Close();
                        return false;
                    }
                }

            }

            catch (Exception ex)
            {

                Console.WriteLine(" Erro ao se comunicar com o banco\n" + ex.Message);
                Conexaosql.Close();



                Console.WriteLine("\nPressione ENTER para continuar");
                Console.ReadKey();
                Console.Clear();
                return false;
            }

        }
        public int BuscaCapacide(string dado, string campo, string tabela, string valorretorna)
        {
            int retorna = 0;
            string queryString = $"SELECT {valorretorna} FROM {tabela} WHERE {campo} = '{dado}'";
            try
            {
                Conexaosql.Open();
                SqlCommand cmd = new SqlCommand(queryString, Conexaosql);
                cmd.ExecuteNonQuery();


                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        retorna = reader.GetInt32(0);
                        Conexaosql.Close();
                    }
                    else
                    {
                        Conexaosql.Close();

                    }
                }



            }

            catch (Exception ex)
            {

                Console.WriteLine(" Erro ao se comunicar com o banco\n" + ex.Message);
                Conexaosql.Close();



                Console.WriteLine("\nPressione ENTER para continuar");
                Console.ReadKey();
                Console.Clear();

            }
            return retorna;
        }
        public double BuscaValor(string Passagem)
        {
            string queryString = $"SELECT IdPAssagem, Data_Voo, Valor, Situacao FROM PassagemVoo WHERE ID = '{Passagem}';";
            double valor = 0;
            try
            {
                SqlCommand command = new SqlCommand(queryString, Conexaosql);
                Conexaosql.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        valor = reader.GetDouble(2);
                    }
                }
                Conexaosql.Close();
                return valor;
            }
            catch (Exception e)
            {
                Conexaosql.Close();
                Console.WriteLine("Erro ao comunicar com o banco\n" + e.Message + "\nTecle [ENTER] para continuar.");
                Console.ReadKey();
                return 0;
            }
        }

        public string BuscaInscricao(string Passagem)
        {
            string queryString = $"SELECT IdPAssagem FROM Passagem WHERE ID = '{Passagem}';";

            string voo = "";
            string aeronave = "";
            try
            {
                SqlCommand cmd = new SqlCommand(queryString, Conexaosql);

                Conexaosql.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        voo = reader.GetString(0);
                    }
                }
                Conexaosql.Close();

            }
            catch (Exception e)
            {
                Conexaosql.Close();

            
                Console.WriteLine($"Erro de acesso ao banco!!!\n{e.Message}");
                Console.WriteLine("Pressione Enter para continuar....");
                Console.ReadKey();
                return "";
            }

            string queryString1 = $"SELECT insAeronave FROM Voo WHERE ID = '{voo}';";

            try
            {
                SqlCommand command = new SqlCommand(queryString1, Conexaosql);

                Conexaosql.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        aeronave = reader.GetString(0);
                    }
                }
                Conexaosql.Close();
                return aeronave;
            }
            catch (Exception e)
            {
                Conexaosql.Close();
                Console.WriteLine($"Erro ao acessar o Banco de Dados!!!\n{e.Message}");
                Console.WriteLine("Pressione Enter para continuar....");
                Console.ReadKey();
                return "";
            }

        }

        public int BuscarinsCapacidade(string inscricao)
        {
            string queryString = $"SELECT SELECT Inscricao, CNPJ,Capacidade, data_Cadastro, Ultima_Venda, Situacao FROM Aeronave WHERE Inscricao WHERE Inscricao = '{inscricao}';";

            int capacidade = 0;

            try
            {
                SqlCommand command = new SqlCommand(queryString, Conexaosql);

                Conexaosql.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        capacidade = reader.GetInt32(2);
                    }
                }
                Conexaosql.Close();
                return capacidade;
            }
            catch (Exception e)
            {
                Conexaosql.Close();
                Console.WriteLine($"Erro ao acessar o Banco de Dados!!!\n{e.Message}");
                Console.WriteLine("Tecle Enter para continuar....");
                Console.ReadKey();
                return 0;
            }
        }

        public string BuscarVoo(string idPassagem)
        {
            string queryString = $"SELECT insAeronave FROM Voo WHERE Id = '{idPassagem}';";

            string valor = "";

            try
            {
                SqlCommand command = new SqlCommand(queryString, Conexaosql);

                Conexaosql.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        valor = reader.GetString(0);
                    }
                }
                Conexaosql.Close();
                return valor;
            }
            catch (Exception e)
            {
                Conexaosql.Close();
                Console.WriteLine($"Erro ao acessar o Banco de Dados!!!\n{e.Message}");
                Console.WriteLine("Tecle Enter para continuar....");
                Console.ReadKey();
                return "";
            }
        }
        public int BuscarOcupacao(string voo)
        {
            string queryString = $"SELECT Id, Assentos_Ocupados FROM Voo WHERE Id = '{voo}';";

            int valor = 0;

            try
            {
                SqlCommand command = new SqlCommand(queryString, Conexaosql);

                Conexaosql.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        valor = reader.GetInt32(0);
                    }
                }
                Conexaosql.Close();
                return valor;
            }
            catch (Exception e)
            {
                Conexaosql.Close();
                Console.WriteLine($"Erro ao acessar o Banco de Dados!!!\n{e.Message}");
                Console.WriteLine("Tecle Enter para continuar....");
                Console.ReadKey();
                return 0;
            }
        }

    }
}
