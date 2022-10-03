using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrjOnTheFl_DB
{
    internal class Venda
    {

        public int ID { get; set; }
        public string DataVenda { get; set; }
        public string Passageiro { get; set; }
        public double ValorTotal { get; set; }

        ConexaoBanco conexaoBD = new ConexaoBanco();


        ItemVenda item = new();


        public Venda()
        {

        }
       
        public void GeraId()
        {
            for (int i = 1; i <= 999; i++)
            {
                if (!conexaoBD.Verificar(i.ToString(), "IdVenda", "Venda"))
                {
                    ID = i;
                    break;
                }
            }
        }

        
        public void FazerVenda()
        {
            Console.WriteLine("VENDA DE PASSAGENS\nDigite 0 para sair");
            DataVenda = DateTime.Now.ToString("dd/MM/yyyy");

            string op;
            int num;
            string cpf;
            string idPassagem;
            string numPassagens;
           
            while (true)
            {
                Console.Write("Insira o CPF do comprador: ");
                cpf = Console.ReadLine().Replace(".", "").Replace("-", "");

                if (cpf == "0") return;

                if (conexaoBD.Verificar(cpf, "CPF", "Restritos"))
                    Console.WriteLine("CPF Bloqueado!");
                else
                {
                    if (!conexaoBD.Verificar(cpf, "CPF", "Passageiro"))
                        Console.WriteLine("CPF não cadastrado!");
                    else
                        break;
                }
            }
            Passageiro = cpf;

            do
            {
                do
                {
                    Console.Write("Quantas passagens deseja comprar(max 4): ");
                    numPassagens = Console.ReadLine();
                    if (!int.TryParse(numPassagens, out num))
                        Console.WriteLine("Digite um valor válido");

                } while (!int.TryParse(numPassagens, out num));

            } while (num < 1 && num > 4);

            for (int i = 1; i <= num; i++)
            {
                while (true)
                {
                    Console.Write("Insira o ID da passagem: ");
                    idPassagem = Console.ReadLine();

                    if (idPassagem == "0") return;

                    if (!conexaoBD.Verificar(idPassagem, "ID", "Passagem Voo"))
                        Console.WriteLine("Passagem não encontrada!");
                    else
                        break;
                }
              
               string aeronave = conexaoBD.BuscaInscricao(idPassagem);
               int  capacidade = conexaoBD.BuscarinsCapacidade(aeronave);
               
                string voo = conexaoBD.BuscarVoo(idPassagem);
             
               int assento = conexaoBD.BuscarOcupacao(voo);

                if (assento <= capacidade)
                {
                    assento++;
                    string queryAssento = $"UPDATE Voo SET AssentosOcupados = {assento} WHERE ID = '{voo}'";
                    conexaoBD.Update(queryAssento);
                }
                else
                {
                    Console.WriteLine("Voo não possui mais passagens disponíveis!");
                    return;
                }

                do
                {
                    Console.Write("Deseja vender[V] ou reservar[R]?: ");
                    op = Console.ReadLine().ToUpper();
                    if (op != "V" && op != "R")
                        Console.WriteLine("Dado inválido!");

                } while (op != "V" && op != "R");

                switch (op)
                {
                    case "V":
                        string queryVendaUp = $"UPDATE PassagemVoo SET Situacao = '{op}',DataUltimaOp = '{DataVenda}'  WHERE ID = '{idPassagem}';";
                        conexaoBD.Update(queryVendaUp);
                        break;

                    case "R":
                        string queryRegistra = $"UPDATE PassagemVoo SET Situacao = '{op}',DataUltimaOp = '{DataVenda}'  WHERE ID = '{idPassagem}';";
                        conexaoBD.Update(queryRegistra);
                        break;
                }

                
                string querydataaeronave = $"UPDATE Aeronave SET UltimaVenda = '{DataVenda}' WHERE Inscricao = '{aeronave}'";
                conexaoBD.Update(querydataaeronave);

                ValorTotal += conexaoBD.BuscaValor(idPassagem);
                item.GerarItem(ID, idPassagem);
            }
          
            string querydataPassagem = $"UPDATE Passageiro SET UltimaCompra = '{DataVenda}' WHERE CPF = '{Passageiro}'";
            conexaoBD.Update(querydataPassagem);

        
            string queryVenda = $"INSERT INTO Venda(ID, DataVenda, ValorTotal, FK_Passageiro_CPF) VALUES ({ID}, '{DataVenda}', {ValorTotal}, '{Passageiro}');";
            conexaoBD.Update(queryVenda);

            Console.WriteLine("Passagens compradas/reservadas com sucesso");
            Thread.Sleep(3000);

        }
    
        public void ImprimeVendas()
        {
            int opc = 8;


            Console.WriteLine(">>> Lista de Venda slecionados <<<\n");


            string query = $"SELECT IdVenda,cpfPassageiro,data_venda,Valor_Total FROM Venda";

            conexaoBD.Select(query, opc);
        }
        //Imprimi uma venda em específico
        public void LocalizarVenda()
        {
        //    string id;
        //    int idVenda;

        //    do
        //    {
        //        Console.Write("Digite o Id da Venda[0 para sair]: ");
        //        id = Console.ReadLine();

        //        if (id == "0") return;

        //        if (!int.TryParse(conexaoBD.TratamentoDado(id), out idVenda))
        //            Console.WriteLine("Digite um valor válido");
        //        else
        //        {
        //            if (!conexaoBD.Verificar(idVenda.ToString(), "ID", "Venda"))
        //            {
        //                Console.WriteLine("Venda não encontrada!");
        //                return;
        //            }
        //        }

        //    } while (!int.TryParse(conexaoBD.TratamentoDado(id), out idVenda));

        //    conexaoBD.ReadVenda(idVenda);
        //
        }
    }
}
