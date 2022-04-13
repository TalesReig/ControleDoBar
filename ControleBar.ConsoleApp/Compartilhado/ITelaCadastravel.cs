namespace ControleBar.ConsoleApp.Compartilhado
{
    public interface ITelaCadastravel
    {
        void Inserir();
        void Editar();
        void Excluir();
        bool VisualizarRegistros(string tipoVisualizacao);
    }
}
