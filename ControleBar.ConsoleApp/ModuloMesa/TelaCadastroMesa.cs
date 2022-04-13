using ControleBar.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleBar.ConsoleApp.ModuloMesa
{
    internal class TelaCadastroMesa : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Mesa> _repositorioMesa;
        private readonly Notificador _notificador;

        public TelaCadastroMesa(IRepositorio<Mesa> repositorioMesa, Notificador notificador)
            : base("Cadastro de Garçons")
        {
            _repositorioMesa = repositorioMesa;
            _notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Garçom");

            Mesa novoMesa = ObterMesa();

            _repositorioMesa.Inserir(novoMesa);

            _notificador.ApresentarMensagem("Garçom cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Garçom");

            bool temRegistrosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRegistrosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum garçom cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroGenero = ObterNumeroRegistro();

            Mesa MesaAtualizado = ObterMesa();

            bool conseguiuEditar = _repositorioMesa.Editar(numeroGenero, MesaAtualizado);

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

            int numeroMesa = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioMesa.Excluir(numeroMesa);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Garçom excluído com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Garçons Cadastrados");

            List<Mesa> garcons = _repositorioMesa.SelecionarTodos();

            if (garcons.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum garçom disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Mesa Mesa in garcons)
                Console.WriteLine(Mesa.ToString());

            Console.ReadLine();

            return true;
        }

        private Mesa ObterMesa()
        {
            Console.Write("Digite o nome do garçom: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o CPF do garçom: ");
            string cpf = Console.ReadLine();

            return new Mesa();
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do garçom que deseja selecionar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioMesa.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do garçom não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
