using ControleBar.ConsoleApp.Compartilhado;
using ControleBar.ConsoleApp.ModuluConta;

namespace ControleBar.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TelaMenuPrincipal telaMenuPrincipal = new TelaMenuPrincipal(new Notificador());

            while (true)
            {
                TelaBase telaSelecionada = telaMenuPrincipal.ObterTela();

                if (telaSelecionada is null)
                    break;

                string opcaoSelecionada = telaSelecionada.MostrarOpcoes();

                if (telaSelecionada is ITelaCadastravel)
                {
                    ITelaCadastravel telaCadastroBasico = (ITelaCadastravel)telaSelecionada;

                    if (opcaoSelecionada == "1")
                        telaCadastroBasico.Inserir();

                    if (opcaoSelecionada == "2")
                        telaCadastroBasico.Editar();

                    if (opcaoSelecionada == "3")
                        telaCadastroBasico.Excluir();

                    if (opcaoSelecionada == "4")
                        telaCadastroBasico.VisualizarRegistros("Tela");
                }
                else if (telaSelecionada is TelaCadastroConta)
                {
                    TelaCadastroConta telaCadastroBasico = (TelaCadastroConta)telaSelecionada;
                    if (opcaoSelecionada == "1")
                        telaCadastroBasico.Abrir();

                    if (opcaoSelecionada == "2")
                        telaCadastroBasico.Fechar();

                    if (opcaoSelecionada == "3")
                        //telaCadastroBasico.adcionarPedido();

                    if (opcaoSelecionada == "4")
                        telaCadastroBasico.VisualizarRegistrosEmAberto("Tela");
                }
            }
        }
    }
}
