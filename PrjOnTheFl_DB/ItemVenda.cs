using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrjOnTheFl_DB
{
    internal class ItemVenda
    {
        private int Id { get; set; }
        private double ValorUnitario { get; set; }
        private int IdVenda { get; set; }
        private string IdPassagem { get; set; }

        ConexaoBanco conexaoBD = new ConexaoBanco();

        public ItemVenda()
        {
        }

        //Gera Id 
        public void GeraId()
        {
            for (int i = 1; i <= 999; i++)
            {
                if (!conexaoBD.Verificar(i.ToString(), "IdItem", "ItemVenda"))
                {
                    Id = i;
                    break;
                }
            }
        }
        //Gera a venda do item
        public void GerarItem(int idVenda, string passagemVooId)
        {

            this.ValorUnitario = conexaoBD.BuscaValor(passagemVooId);
            this.IdVenda = idVenda;
            this.IdPassagem = passagemVooId;

            string query = $"INSERT INTO ItemVenda (codPAssagem, cpdVenda, ValorTotal) VALUES ('{IdPassagem}', '{IdVenda}', '{ValorUnitario}');"; 
            conexaoBD.Insert(query);
        }
        //Altera o voo da venda
        public void AlteraPassagem(int idItem, string Passagem)
        {
            
            string queryItem = $"UPDATE ItemVenda SET codPassagem = '{Passagem}' WHERE IdItem = {idItem}";
            conexaoBD.Update(queryItem);

            
            this.ValorUnitario = conexaoBD.BuscaValor(Passagem);
            string queryPas = $"UPDATE ItemVenda SET Valor_Unitario = '{ValorUnitario}' WHERE codPassagem = {Passagem}";
            conexaoBD.BuscaValor(queryPas);
        }
        //Imprimi o item da venda
        public void ImprimiItemVenda(int id)
        {
           // conexaoBD.ReadItemVenda(id);
        }

    }
}