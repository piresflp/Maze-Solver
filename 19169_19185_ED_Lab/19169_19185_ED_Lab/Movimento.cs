using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19169_19185_ED_Lab
{
    class Movimento
    {
        private int linhaOrigem, linhaDestino, colunaOrigem, colunaDestino;

        public Movimento(int linhaOrigem, int linhaDestino, int colunaOrigem, int colunaDestino)
        {
            this.LinhaOrigem = linhaOrigem;
            this.LinhaDestino = linhaDestino;
            this.ColunaOrigem = colunaOrigem;
            this.ColunaDestino = colunaDestino;
        }

        public int LinhaOrigem { get => linhaOrigem; set => linhaOrigem = value; }
        public int LinhaDestino { get => linhaDestino; set => linhaDestino = value; }
        public int ColunaOrigem { get => colunaOrigem; set => colunaOrigem = value; }
        public int ColunaDestino { get => colunaDestino; set => colunaDestino = value; }
    }
}
