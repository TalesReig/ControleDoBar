using ControleBar.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleBar.ConsoleApp.ModuluConta
{
    internal class TelaCadastroProduto : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Produto> _repositorioProduto;
        private readonly Notificador _notificador;

        public TelaCadastroProduto(IRepositorio<Produto> repositorioProduto, Notificador notificador)
            : base("Cadastro de Produtos:")
        {
            _repositorioProduto = repositorioProduto;
            _notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Garçom");

            Produto novoProduto = ObterProduto();

            _repositorioProduto.Inserir(novoProduto);

            _notificador.ApresentarMensagem("Garçom cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Garçom");

            bool temRegistrosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRegistrosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum produto cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroGenero = ObterNumeroRegistro();

            Produto ProdutoAtualizado = ObterProduto();

            bool conseguiuEditar = _repositorioProduto.Editar(numeroGenero, ProdutoAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Garçom editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Garçom");

            bool temFuncionariosRegistrados = VisualizarRegistros("Pesquisando");

            if (temFuncionariosRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum garçom cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroProduto = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioProduto.Excluir(numeroProduto);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Garçom excluído com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Garçons Cadastrados");

            List<Produto> garcons = _repositorioProduto.SelecionarTodos();

            if (garcons.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum garçom disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Produto Produto in garcons)
                Console.WriteLine(Produto.ToString());

            Console.ReadLine();

            return true;
        }

        private Produto ObterProduto()
        {
            Console.Write("Digite o nome do Produto: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o preço da unidade do produto: ");
            int preco = Convert.ToInt32(Console.ReadLine());

            return new Produto(nome, preco);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do garçom que deseja selecionar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioProduto.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do garçom não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
