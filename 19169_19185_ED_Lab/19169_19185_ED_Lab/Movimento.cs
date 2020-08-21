using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19169_19185_ED_Lab
{
    class Movimento
    {
        private int linha, coluna;
        private static readonly int[,] direcoes = {{-1,0},{-1,1},{0,1},{1,1},{1,0},{1,-1},{0,-1},{-1,-1}};

        public Movimento() {} // construtor feito para acessar os possíveis movimentos
        public Movimento(int novaLinha, int novaColuna) // construtor para alterar o movimento atual
        {
            this.Linha = novaLinha;
            this.Coluna = novaColuna;
        }

        public int Linha { get => linha; set => linha = value; }
        public int Coluna { get => coluna; set => coluna = value; }
        public int[,] Direcoes { get => direcoes;}
    }
}
