using ControleBar.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleBar.ConsoleApp.ModuluConta
{
    internal class Produto : EntidadeBase
    {
        public string nome;
        public double preco;

        public Produto(string nome, double preco)
        {
            this.nome = nome;
            this.preco = preco;
        }
    }
}
