using ControleBar.ConsoleApp.Financeiro;
using ControleBar.ConsoleApp.ModuloGarcom;
using ControleBar.ConsoleApp.ModuloMesa;
using ControleBar.ConsoleApp.ModuluConta;
using System;

namespace ControleBar.ConsoleApp.Compartilhado
{
    public class TelaMenuPrincipal
    {
        private readonly Garcom garcom;
        private readonly IRepositorio<Garcom> repositorioGarcom;
        private readonly TelaCadastroGarcom telaCadastroGarcom;

        private readonly IRepositorio<Produto> repositorioProduto;
        private readonly TelaCadastroProduto telaCadastroProduto;

        private readonly IRepositorio<Mesa> repositorioMesa;
        private readonly TelaCadastroMesa telaCadastroMesa;

        private readonly RepositorioConta repositorioConta;
        private readonly TelaCadastroConta telaCadastroConta;

        private readonly ControleFinanceiro controleFinanceiro;

        //private readonly RepositorioProduto repositorioProduto;

        public TelaMenuPrincipal(Notificador notificador)
        {
            garcom = new Garcom();
            repositorioGarcom = new RepositorioGarcom();
            telaCadastroGarcom = new TelaCadastroGarcom(repositorioGarcom, notificador);

            repositorioProduto = new RepositorioProduto();
            telaCadastroProduto = new TelaCadastroProduto(repositorioProduto, notificador);

            repositorioMesa = new RepositorioMesa();
            telaCadastroMesa = new TelaCadastroMesa(repositorioMesa, notificador);

            repositorioConta = new RepositorioConta();
            telaCadastroConta = new TelaCadastroConta(notificador, repositorioGarcom, telaCadastroGarcom, repositorioConta, garcom);

            controleFinanceiro = new ControleFinanceiro();
            PopularAplicacao();
        }

        public string MostrarOpcoes()
        {
            Console.Clear();

            Console.WriteLine("Controle de Mesas de Bar 1.0");

            Console.WriteLine();

            Console.WriteLine("Digite 1 para Gerenciar Garçons");

            Console.WriteLine("Digite 2 para Gerenciar Produtos");

            Console.WriteLine("Digite 3 para Gerenciar Mesas");

            Console.WriteLine("Digite 4 para Gerenciar Contas");

            Console.WriteLine("Digite 5 paraer a receita do dia atual: ");

            Console.WriteLine("Digite s para sair");

            string opcaoSelecionada = Console.ReadLine();

            return opcaoSelecionada;
        }

        public TelaBase ObterTela()
        {
            string opcao = MostrarOpcoes();

            TelaBase tela = null;

            if (opcao == "1")
                tela = telaCadastroGarcom;

            else if (opcao == "2")
                tela = telaCadastroProduto;

            else if (opcao == "3")
                tela = telaCadastroMesa;

            else if (opcao == "4")
                tela = telaCadastroConta;

            else if (opcao == "5")
                 controleFinanceiro.MostrarReceita();

            return tela;
        }

        private void PopularAplicacao()
        {
            var garcom = new Garcom("Julinho", "230.232.519-98");
            repositorioGarcom.Inserir(garcom);
        }
    }
}
