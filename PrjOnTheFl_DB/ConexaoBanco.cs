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

                                Console.Write(" Id: {0}", leitor.GetString(0));
                                Console.Write("\n InsAeronave: {0} ", leitor.GetString(1));
                                Console.Write("\n Destino: {0} ", leitor.GetString(2));
                                Console.Write("\n Assentos ocupados: {0} ", leitor.GetInt32(3));
                                Console.Write("\n Data Cadastro: {0} ", leitor.GetDateTime(4).ToString("dd/MM/yyyy"));
                                Console.Write("\n Ultima compra: {0} ", leitor.GetDateTime(5).ToString("dd/MM/yyyy"));
                                Console.Write("\n Situação: {0} ", leitor.GetString(6));

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
        public double GetValor(string IdPassagemVoo)
        {
            string queryString = $"SELECT ID, IDVoo, DataUltimaOperacao, Valor, Situacao FROM PassagemVoo WHERE ID = '{IdPassagemVoo}';";
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



    }
}
