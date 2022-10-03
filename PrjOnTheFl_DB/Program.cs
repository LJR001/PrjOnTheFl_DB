﻿using System;

namespace PrjOnTheFl_DB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            MostrarMenuInicial();
        }

        static void MostrarMenuInicial()
        {
            int opcao = 7;
            do
            {
                Console.Clear();
                Console.WriteLine(" °°°  MENU  INICIAL  °°°");
                Console.WriteLine(" Opção 1 : Menu cadastro");
                Console.WriteLine(" Opção 2 : Menu localizar");
                Console.WriteLine(" Opção 3 : Menu editar");
                Console.WriteLine(" Opção 4 : Menu imprimir");
                Console.WriteLine(" Opção 5 : Menu bloqueados e restritos");
                Console.WriteLine(" Opção 6 : Menu imprimir");
                Console.WriteLine(" Opção 0 : Sair");

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
                        case 0:
                            Environment.Exit(0);
                            break;

                        case 1:
                            Console.Clear();
                            MostrarMenuCadastrar();
                            break;

                        case 2:
                            Console.Clear();
                            MostrarMenuLocalizar();
                            break;

                        case 3:
                            Console.Clear();
                            MostrarMenuEditar();
                            break;

                        case 4:
                            Console.Clear();
                            MostrarMenuImprimir();
                            break;

                        case 5:
                            Console.Clear();
                            MenuBloqueadosRestritos();
                            break;


                        //ARRUMAR AQUI !!!!!

                        case 6:
                            Console.Clear();
                            Passageiro paa = new Passageiro();
                            //    paa.DeletaPassageiro();
                            break;

                        default:
                            Console.Write("\n Opcao Inválida!\n Digite novamente: ");
                            break;
                    }
                } while (opcao > 7);
            } while (true);
        }

        static void MostrarMenuCadastrar()
        {
            int opcao = 7;

            Passageiro passageiro = new();
            CompanhiaAerea companhiaAerea = new();
            Aeronave aeronave = new();
            Voo voo = new();
            Venda venda = new();

            Console.WriteLine(" °°°  MENU  CADASTRO  °°°");
            Console.WriteLine(" Opção 1 : Cadastrar passageiro");
            Console.WriteLine(" Opção 2 : Cadastrar companhia aerea");
            Console.WriteLine(" Opção 3 : Cadastrar aeronave");
            Console.WriteLine(" Opção 4 : Cadastro de voo");
            Console.WriteLine(" Opção 5 : Fazer venda de passagem");
            Console.WriteLine(" Opção 6 : Voltar ao Menu Iniciar");
            Console.WriteLine(" Opção 0 : Sair");

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
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        Console.WriteLine(" Cadastrar passageiro");
                        Console.Clear();
                        passageiro.CadastraPassageiro();
                        break;
                    case 2:
                        Console.WriteLine(" Cadastrar companhia aerea");
                        Console.Clear();
                        companhiaAerea.CadCompanhia();
                        break;
                    case 3:
                        Console.WriteLine(" Cadastrar aeronave");
                        Console.Clear();
                        aeronave.CadastraAeronave();
                        break;

                    case 4:
                        Console.WriteLine(" Cadastro de voo");
                        Console.Clear();
                        voo.CadastrarVoo();
                        break;

                    case 5:
                        Console.WriteLine(" Fazer venda de passagem");
                        Console.Clear();
                        venda.FazerVenda();
                        break;
                    case 6:
                        Console.WriteLine(" Menu Inicial");
                        MostrarMenuInicial();
                        break;

                    default:
                        Console.Write("\n Opção invalida\n Digite novamente:");
                        break;
                }
            } while (true);
        }


        static void MostrarMenuLocalizar() //inoperante
        {
            int opcao = 8;

            Passageiro passageiro = new();
            CompanhiaAerea companhiaAerea = new();
            Aeronave aeronave = new();
            Voo voo = new();
            PassagemVoo passagemVoo = new();
            Venda venda = new();

            Console.WriteLine(" °°°  MENU  LOCALIZAR  °°°");
            Console.WriteLine(" Opção 1 : Localizar passageiro");
            Console.WriteLine(" Opção 2 : Localizar companhia aerea");
            Console.WriteLine(" Opção 3 : Localizar aeronave");
            Console.WriteLine(" Opção 4 : Localizar voo");
            Console.WriteLine(" Opção 5 : Localizar passagem");
            Console.WriteLine(" Opção 6 : Localizar venda de passagem");
            Console.WriteLine(" Opção 7 : Voltar ao Menu Iniciar");
            Console.WriteLine(" Opção 0 : Sair");

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
                    case 0:
                        Environment.Exit(0);
                        break;

                    case 1:
                        Console.WriteLine("Localizar passaageiro");
                        Console.Clear();
                        passageiro.LocalizaPassageiro();
                        break;

                    case 2:
                        Console.WriteLine("Localizar companhia aerea");
                        Console.Clear();
                        //companhiaAerea.LocalCompanhia();
                        break;

                    case 3:
                        Console.WriteLine("Localizar aeronave");
                        Console.Clear();
                         aeronave.LocalizaAeronave();
                        break;

                    case 4:
                        Console.WriteLine("Localizar voo");
                        Console.Clear();
                        voo.LocalizaVoo();
                        break;

                    case 5:
                        Console.WriteLine("Localizar passagem");
                        Console.Clear();
                        passagemVoo.LocalPassagem();
                        break;

                    case 6:
                        Console.WriteLine("Localizar venda de passagem");
                        Console.Clear();
                       // venda.Localizar();
                        break;

                    case 7:
                        Console.Clear();
                        MostrarMenuInicial();
                        break;

                    default:
                        Console.Write("\n Opcao Inválida!\n Digite novamente: ");
                        break;
                }

            } while (true);
        }

        static void MostrarMenuEditar()
        {
            int opcao = 0;

            Passageiro passageiro = new();
            CompanhiaAerea companhiaAerea = new();
            Aeronave aeronave = new();
            Voo voo = new();
            PassagemVoo passagemVoo = new();

            Console.WriteLine(" °°°  MENU  EDITAR  °°°");
            Console.WriteLine(" Opção 1 : Editar passageiro");
            Console.WriteLine(" Opção 2 : Editar companhia aerea");
            Console.WriteLine(" Opção 3 : Editar aeronave");
            Console.WriteLine(" Opção 4 : Editar voo");
            Console.WriteLine(" Opção 5 : Editar valor passagem");
            Console.WriteLine(" Opção 6 : Voltar ao Menu Iniciar");
            Console.WriteLine(" Opção 0 : Retorna ao menu INICIAR");

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
                    case 0:
                        Console.WriteLine(" Retornando ao menu anterior");
                        Console.WriteLine();
                        Console.Clear();
                        break;

                    case 1:
                        Console.WriteLine("Editar passageiro");
                        Console.Clear();
                        passageiro.AlteraDadoPassageiro();
                        break;

                    case 2:
                        Console.WriteLine("Editar companhia aerea");
                        Console.Clear();
                        companhiaAerea.AlteraCompanhia();
                        break;

                    case 3:
                        Console.WriteLine("Editar aeronave");
                        Console.Clear();
                        aeronave.AlteraDadoAeronave();
                        break;

                    case 4:
                        Console.WriteLine("Editar voo");
                        Console.Clear();
                        voo.AlteraDadoVoo();
                        break;

                    case 5:
                        Console.WriteLine("Editar passagem");
                        Console.Clear();
                        passagemVoo.AlteraPassagem();
                        break;

                    case 6:
                        Console.Clear();
                        MostrarMenuInicial();
                        break;

                    default:
                        Console.Write("\n Opcao Inválida!\n Digite novamente: ");
                        break;
                }

            } while (opcao > 6);

            return;
        }

        static void MostrarMenuImprimir()
        {
            int opcao = 8;

            Passageiro passageiro = new();
            CompanhiaAerea companhiaAerea = new();
            Aeronave aeronave = new();
            Voo voo = new();
            PassagemVoo passagemVoo = new();
            Venda venda = new();

            Console.WriteLine(" °°°  MENU  IMPRIMIR  °°°");
            Console.WriteLine(" Opção 1 : Imprime passageiros");
            Console.WriteLine(" Opção 2 : Imprime companhias aereas");
            Console.WriteLine(" Opção 3 : Imprime aeronaves");
            Console.WriteLine(" Opção 4 : Imprime voos");
            Console.WriteLine(" Opção 5 : Imprime passagens");
            Console.WriteLine(" Opção 6 : Imprime venda de passagens");
            Console.WriteLine(" Opção 7 : Voltar ao Menu Iniciar");
            Console.WriteLine(" Opção 0 : Sair");

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
                    case 0:
                        Environment.Exit(0);
                        break;

                    case 1:
                        Console.WriteLine("Imprime passageiro");
                        Console.Clear();
                        passageiro.ImprimiPassageiros();
                        break;

                    case 2:
                        Console.WriteLine("Imprime companhia aerea");
                        Console.Clear();
                        companhiaAerea.ImprCompanhia();
                        break;

                    case 3:
                        Console.WriteLine("Imprime aeronave");
                        Console.Clear();
                        aeronave.ImprimerAeronaves();
                        break;

                    case 4:
                        Console.WriteLine("Imprime voo");
                        Console.Clear();
                        voo.ImprimeVoos();
                        break;

                    case 5:
                        Console.WriteLine("Imprime passagem");
                        Console.Clear();
                        passagemVoo.ImprimirPassagens(); ;
                        break;

                    case 6:
                        Console.WriteLine("Imprime venda de passagem");
                        Console.Clear();
                        venda.ImprimeVendas();
                        break;

                    case 7:
                        Console.Clear();
                        MostrarMenuInicial();
                        break;

                    default:
                        Console.Write("\n Opcao Inválida!\n Digite novamente: ");
                        break;
                }

            } while (true);
        }

        static void MenuBloqueadosRestritos()
        {
            int opcao = 4;

            Bloqueado bloq = new();
            Restrito rest = new();

            Console.WriteLine(" °°°  MENU  BLOQUEADOS E RESTRITOS  °°°");
            Console.WriteLine(" Opção 1 : Restrição CPF");
            Console.WriteLine(" Opção 2 : Bloqueio CNPJ");
            Console.WriteLine(" Opção 3 : Voltar ao Menu Iniciar");
            Console.WriteLine(" Opção 0 : Sair");

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
                    case 0:
                        Environment.Exit(0);
                        break;

                    case 1:
                        Console.WriteLine("Restringir CPF");
                        Console.Clear();
                        rest.GerarMenu();

                        break;

                    case 2:
                        Console.WriteLine("Bloquear CNPJ");
                        Console.Clear();
                        bloq.GerarMenu();
                        break;

                    case 3:
                        Console.Clear();
                        MostrarMenuInicial();
                        break;

                    default:
                        Console.Write("\n Opcao Inválida!\n Digite novamente: ");
                        break;
                }

            } while (true);
        }
    }
}
