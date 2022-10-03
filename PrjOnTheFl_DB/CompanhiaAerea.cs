using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrjOnTheFl_DB
{
    internal class CompanhiaAerea
    {
        private string Cnpj { get; set; }
        private string RazaoSocial { get; set; }
        private DateTime DataAbertura { get; set; }
        private DateTime UltimoVoo { get; set; }
        private DateTime DataCadastro { get; set; }
        private string Situacao { get; set; }

        ConexaoBanco conexaoBD = new ConexaoBanco();

        //Caminho para acessar o arquivo de companhias
        //string caminho = "C:\\Users\\artur\\source\\repos\\ProjetoOnTheFly\\ProjetoOnTheFly\\Dados\\CompanhiaAerea.dat";
        public CompanhiaAerea()
        {

        }
        public override string ToString()
        {
            return $"{Cnpj}{RazaoSocial}{DataAbertura}{UltimoVoo}{DataCadastro}{Situacao}";
        }
        //Cadastra CNPJ
        public bool CadCNPJ()
        {
            do
            {
                Console.Write("Digite o CNPJ: ");
                Cnpj = Console.ReadLine().Replace(".", "").Replace("-", "").Replace("/", "");
                if (Cnpj == "0")
                    return false; ;
                if (!ValidaCNPJ(Cnpj))
                {
                    Console.WriteLine("Digite um CNPJ Válido!");
                    Thread.Sleep(2000);
                }
            } while (!ValidaCNPJ(Cnpj));
            //if (VerifCNPJ(this.caminho, Cnpj))
            //{
            //    Console.WriteLine("Este CNPJ já está cadastrado!!");
            //    Thread.Sleep(3000);
            //    return false;
            //}
            return true;
        }
        //Cadastra Data de Abertura
        public bool CadDataAbertura()
        {
            Console.Write("Digite a data de abertura (Mês/Dia/Ano): ");
            DateTime dataAbertura;
            while (!DateTime.TryParse(Console.ReadLine(), out dataAbertura))
            {
                Console.WriteLine("Formato de data incorreto!");
                Console.Write("Digite a data de abertura (Mês/Dia/Ano): ");
            }
            DateTime verData = dataAbertura;
            if (verData > DateTime.Now.AddMonths(-6))
            {
                Console.WriteLine("Não é possível cadastrar empresas com menos de 6 meses!!!");
                Thread.Sleep(2000);
                return false;
            }
            DataAbertura = dataAbertura;
            //if (DataAbertura == "0")
            //    return false;
            return true;
        }
        //Cadastra a Razão Social
        public bool CadRazao()
        {
            Console.Write("Digite a Razão Social:  (Max 50 caracteres): ");
            RazaoSocial = Console.ReadLine();
            if (RazaoSocial == "0")
                return false;
            if (RazaoSocial.Length > 50)
            {
                Console.WriteLine("Infome uma Razão Social menor que 50 caracteres!!!!");
                Thread.Sleep(2000);
            }
            while (RazaoSocial.Length > 50) ;

            for (int i = RazaoSocial.Length; i <= 50; i++)
                RazaoSocial += " ";
            return true;
        }

        //Altera a situação da Companhia 
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

        //Cadastra uma nova Companhia Aerea
        public void CadCompanhia()
        {
            Console.WriteLine(">>> CADASTRO DE COMPANHIA AEREA <<<");
            Console.WriteLine("Para cancelar o cadastro digite 0:\n");

            if (!CadCNPJ())
                return;

            if (!CadDataAbertura())
                return;

            if (!CadRazao())
                return;

            UltimoVoo = DateTime.Now;

            DataCadastro = DateTime.Now;

            Situacao = "A";

            //Insere no arquivo a nova Companhia
            //string caminho = $"C:\\Users\\wessm\\source\\repos\\ProjetoOnTheFly\\ProjetoOnTheFly\\Dados\\CompanhiaAerea.dat";
            //string texto = $"{ToString()}\n";
            //File.AppendAllText(caminho, texto);

            string query = $"INSERT INTO Companhia (CNPJ,Razao_Social,Data_Abertura,Ultimo_Voo,Data_Cadastro,Situacao)" + $"VALUES('{Cnpj}','{RazaoSocial}','{DataAbertura}','{UltimoVoo}','{DataCadastro}','{Situacao}')";

            conexaoBD.Insert(query);


            Console.WriteLine("\nCADASTRO REALIZADO COM SUCESSO!\nPressione Enter para continuar...");
            Console.ReadKey();
        }

        //Altera dados da Companhia
        public void AlteraCompanhia()
        {
            int opc = 4;
            string cnpj;
            bool verifica;

            Console.WriteLine(">>> ALTERAR DADOS DA COMPANHIA <<<\n");
            Console.Write(" Digite o CNPJ da Companhia: ");

            do
            {
                cnpj = Console.ReadLine().Replace(".", "").Replace("-", "").Replace("/", "");
                if (cnpj == "s")
                    return;

                if (!ValidaCNPJ(cnpj))
                {
                    Console.WriteLine("CNPJ inválido!");
                    Thread.Sleep(3000);
                }
            } while (!ValidaCNPJ(cnpj));

            string query = $"SELECT CNPJ,Razao_Social,Data_Abertura,Ultimo_Voo,Data_Cadastro,Situacao FROM Companhia WHERE CNPJ = '{cnpj}'";

            verifica = conexaoBD.Select(query, opc);

            if (verifica)
            {
                int opcao = 0;
                Console.Clear();
                Console.WriteLine(" -- Alterar dados pessoais");
                Console.WriteLine(" Opção 1 : Razãp social");
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
                            Console.Write(" Digite a nova razão socail: ");
                            string novasocial = Console.ReadLine();
                            string queryupdate = $"UPDATE Companhia SET Razao_Social = '{novasocial}' WHERE CNPJ ='{cnpj}'";
                            conexaoBD.Update(queryupdate);

                            query = $"SELECT CNPJ,Razao_Social,Data_Abertura,Ultimo_Voo,Data_Cadastro,Situacao FROM Companhia WHERE CNPJ = '{cnpj}'";

                            conexaoBD.Select(query, opc);


                            break;

                        case 2:
                            Console.Write(" Digite a nova sitaução: ");
                            char altera = char.Parse(Console.ReadLine());
                            queryupdate = $"UPDATE Companhia SET Situacao = '{altera}' WHERE CNPJ ='{cnpj}'";
                            conexaoBD.Update(queryupdate);

                            query = $"SELECT CNPJ,Razao_Social,Data_Abertura,Ultimo_Voo,Data_Cadastro,Situacao FROM Companhia WHERE CNPJ = '{cnpj}'";

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


        //Imprimi as Companhias Cadastradas e Ativas
        public void ImprCompanhia()
        {
            int opc = 4;


            Console.WriteLine(">>> Lista de Companhias cadastradas cadastradas <<<\n");


            string query = $"SELECT CNPJ,Razao_Social,Data_Abertura,Ultimo_Voo,Data_Cadastro,Situacao FROM Companhia";

            conexaoBD.Select(query, opc);
        }

        //Localiza a Comnhania pelo CNPJ
        public void LocalCompanhia(string caminho, string cnpj)
        {
            int opc = 4;
            string cpf;

            Console.WriteLine(">>> ALTERAR DADOS DE PASSAGEIRO <<<\nPara sair digite 's'.\n");
            Console.Write("Digite o CPF do passageiro: ");

            do
            {

                cpf = Console.ReadLine().Replace(".", "").Replace("-", "");

                if (!ValidaCNPJ(cnpj))
                {
                    Console.WriteLine("CPF não encontradp!");
                    Thread.Sleep(3000);
                    Console.Write("Digite outro CPF! : ");
                }
            } while (!ValidaCNPJ(cnpj));

            string query = $"SELECT CNPJ,Razao_Social,Data_Abertura,Ultimo_Voo,Data_Cadastro,Situacao FROM Companhia WHERE CNPJ = '{cnpj}'";

            conexaoBD.Select(query, opc);
        }

        //Verifica se o Cnpj já esta cadastrado
        public bool VerifCNPJ(string cnpj)
        {
            //foreach (string line in File.ReadLines(caminho))
            //{
            //    if (line.Contains(cnpj))
            //    {
            //        return true;
            //    }
            //}
            return false;
        }

        //Verifica O CNPJ se é válido
        public bool ValidaCNPJ(string vrCNPJ)

        {
            string CNPJ = vrCNPJ.Replace(".", "");
            CNPJ = CNPJ.Replace("/", "");
            CNPJ = CNPJ.Replace("-", "");

            int[] digitos, soma, resultado;
            int nrDig;
            string ftmt;
            bool[] CNPJOk;

            ftmt = "6543298765432";
            digitos = new int[14];
            soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;

            CNPJOk = new bool[2];
            CNPJOk[0] = false;
            CNPJOk[1] = false;

            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(
                        CNPJ.Substring(nrDig, 1));

                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(
                          nrDig + 1, 1)));

                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(
                          nrDig, 1)));
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);
                    if ((resultado[nrDig] == 0) || (
                         resultado[nrDig] == 1))

                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == 0);

                    else
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == (
                        11 - resultado[nrDig]));

                }
                return (CNPJOk[0] && CNPJOk[1]);
            }
            catch
            {
                return false;
            }
        }
    }
}
