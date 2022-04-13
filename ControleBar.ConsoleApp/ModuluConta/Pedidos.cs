using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleBar.ConsoleApp.ModuluConta
{
    internal class Pedidos
    {
        public List<Produto> Produtos { get; set; }

        public void CriarPedido()
        {
            char resposta;
            do
            {
                //Mostrar Produtos

                //Pegar ID do Produto e verificar se existe

                //Adcionar produto na Lista
                Console.WriteLine("Deseja inserir mais um produto(s) para adcionar mais um produto: ");
                resposta = Convert.ToChar(Console.ReadLine().ToUpper());
            } while (resposta == 's');

        }
    }
}
