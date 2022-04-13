using ControleBar.ConsoleApp.Compartilhado;
using ControleBar.ConsoleApp.ModuloGarcom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleBar.ConsoleApp.ModuluConta
{
    internal class Conta : EntidadeBase
    {
        public List<Pedidos> Produtos;
        public double valorFinal;
        public Garcom Garcom;
        public DateTime date;
        public bool emAberto; // true = aberto | false = fechado;

        public Conta(Garcom garcom)
        {
            Garcom = garcom;
            date = DateTime.Today;
            this.emAberto = true;
        }
    }
}
