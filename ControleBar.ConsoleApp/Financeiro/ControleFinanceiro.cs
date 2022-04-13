using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleBar.ConsoleApp.Financeiro
{
    internal class ControleFinanceiro
    {
        public int receita;
        
        public void IncrementarReceita(double valorDaConta)
        {
            receita += receita;
        }

        public void MostrarReceita()
        {
            Console.WriteLine("O valor total de vendas no dia de hoke foi de: "+receita+"R$");
        }
    }
}
