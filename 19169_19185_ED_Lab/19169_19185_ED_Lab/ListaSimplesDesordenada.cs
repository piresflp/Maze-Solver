using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace _19169_19185_ED_Lab
{
    class ListaSimplesDesordenada<X> where X : IComparable<X>
    {
        private class No
        {
            private X info;
            private No prox;

            public No(X i, No p)
            {
                Info = i;
                Prox = p;
            }

            public X Info
            {
                get { return this.info; }
                set
                {
                    if (value != null)
                        this.info = value;
                }
            }

            public No Prox
            {
                get { return this.prox; }
                set
                {
                    this.prox = value;
                }
            }
        }

        private No primeiro, ultimo;
        private int qtd;

        public ListaSimplesDesordenada()
        {
            qtd = 0;
        }
        public void ExibirLista()
        {
            No atual = this.primeiro;
            while(atual != null)
            {
                WriteLine(atual.Info);
                atual = atual.Prox;
            }
        }

        public bool isVazia() { return (this.primeiro == null);  }

        public void insiraNoInicio(X i)
        {
            if (i == null)
                throw new Exception("Informação ausente");
            //if (isVazia())
                //this.ultimo = i;

        }
        
        public X getDoInicio()
        {
            if (this.primeiro == null)
                throw new Exception("Nada a obter");            
            return this.primeiro.Info;
        }

        public X getDoFim()
        {
            if (this.ultimo == null)
                throw new Exception("Nada a obter");
            return this.ultimo.Info;
        }
    }
}
