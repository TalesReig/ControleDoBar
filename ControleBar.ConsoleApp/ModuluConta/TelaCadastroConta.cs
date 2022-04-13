using ControleBar.ConsoleApp.Compartilhado;
using ControleBar.ConsoleApp.ModuloGarcom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleBar.ConsoleApp.ModuluConta
{
    internal class TelaCadastroConta : TelaBase

    {
        private readonly Garcom garcom;
        private readonly Notificador _notificador;
        private readonly RepositorioConta _repositorioConta;
        private readonly IRepositorio<Garcom> repositorioGarcom;
        private readonly TelaCadastroGarcom telaCadastroGarcom;

        public TelaCadastroConta( Notificador notificador, IRepositorio<Garcom> repositorioGarcom, TelaCadastroGarcom telaCadastroGarcom, RepositorioConta repositorioConta, Garcom garcom)
            : base("Cadastro de Contas:")
        {
            _notificador = notificador;
            this.repositorioGarcom = repositorioGarcom;
            this.telaCadastroGarcom = telaCadastroGarcom;
            _repositorioConta = repositorioConta;
            this.garcom = garcom;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Abrir uma conta");
            Console.WriteLine("Digite 2 para Fechar uma conta");
            Console.WriteLine("Digite 3 para Adicionar pedidos nesta conta");
            Console.WriteLine("Digite 4 para Visualizar Contas em aberto");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public void Abrir()
        {
            MostrarTitulo("Abrindo Conta:");

            Conta novoConta = ObterConta();

            _repositorioConta.Inserir(novoConta);

            _notificador.ApresentarMensagem("Conta com sucesso!", TipoMensagem.Sucesso);
        }

        public void Fechar()
        {
            MostrarTitulo("Encerrando conta:");

            bool ExisteContasEmAberto = VisualizarRegistrosEmAberto("Pesquisando");

            if (ExisteContasEmAberto == false)
            {
                _notificador.ApresentarMensagem("Nenhuma conta em aberto para encerrar.", TipoMensagem.Atencao);
                return;
            }

            int id = ObterNumeroRegistro();

            Conta ContaEncerrada = _repositorioConta.SelecionarRegistro(id);
            ContaEncerrada.emAberto = false;
            garcom.ReceberGorjeta(ContaEncerrada.valorFinal * 0.1);

            _notificador.ApresentarMensagem("Garçom excluído com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistrosEmAberto(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Garçons Cadastrados");

            List<Conta> contas = _repositorioConta.SelecionarTodos();

            if (contas.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum garçom disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Conta Conta in contas)
            {
                if (Conta.emAberto == true)
                    Console.WriteLine(Conta.ToString());
            }

            Console.ReadLine();

            return true;
        }

        private Conta ObterConta()
        {
            int id = telaCadastroGarcom.ObterNumeroRegistro();//Obtendo id do Garçom
            Garcom garcom = repositorioGarcom.SelecionarRegistro(id);

            Conta conta = new Conta(garcom);

            return conta;
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da conta que deseja selecionar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioConta.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("Conta não econtrada, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
