using ControleBar.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;

namespace ControleBar.ConsoleApp.ModuloGarcom
{
    public class TelaCadastroGarcom : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Garcom> _repositorioGarcom;
        private readonly Notificador _notificador;

        public TelaCadastroGarcom(IRepositorio<Garcom> repositorioGarcom, Notificador notificador)
            : base("Cadastro de Garçons")
        {
            _repositorioGarcom = repositorioGarcom;
            _notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Garçom");

            Garcom novoGarcom = ObterGarcom();

            _repositorioGarcom.Inserir(novoGarcom);

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

            Garcom garcomAtualizado = ObterGarcom();

            bool conseguiuEditar = _repositorioGarcom.Editar(numeroGenero, garcomAtualizado);

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

            int numeroGarcom = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioGarcom.Excluir(numeroGarcom);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Garçom excluído com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Garçons Cadastrados");

            List<Garcom> garcons = _repositorioGarcom.SelecionarTodos();

            if (garcons.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum garçom disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Garcom garcom in garcons)
                Console.WriteLine(garcom.ToString());

            Console.ReadLine();

            return true;
        }

        private Garcom ObterGarcom()
        {
            Console.Write("Digite o nome do garçom: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o CPF do garçom: ");
            string cpf = Console.ReadLine();

            return new Garcom(nome, cpf);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do garçom que deseja selecionar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioGarcom.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do garçom não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}