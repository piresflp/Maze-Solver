using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19169_19185_ED_Lab
{
    class Labirinto
    {
        private char[,] matriz;
        public Labirinto(string arquivo)
        {
            lerArquivo(arquivo);
        }

        private void lerArquivo(string arquivo)
        {
            StreamReader leitor = new StreamReader(arquivo);
            int colunas = int.Parse(leitor.ReadLine());
            int linhas = int.Parse(leitor.ReadLine());

            matriz = new char[linhas, colunas];

            for(int i = 0; i < linhas; i++)
            {
                string linhaLida = leitor.ReadLine();
                for (int j = 0; j < colunas; j++)
                    matriz[i, j] = linhaLida[j];
            }
        }
    }
}
