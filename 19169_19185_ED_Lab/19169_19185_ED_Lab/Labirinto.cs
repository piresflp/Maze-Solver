using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            StreamReader leitor = new StreamReader(arquivo); // define um StreamReader
            int colunas = int.Parse(leitor.ReadLine()); // lê a quantidade de colunas
            int linhas = int.Parse(leitor.ReadLine()); // lê a quantidade de colunas

            matriz = new char[linhas, colunas]; // instancia a matriz de acordo com os dados lidos

            for(int i = 0; i < linhas; i++) // lê o labirinto 
            {
                string linhaLida = leitor.ReadLine();
                for (int j = 0; j < colunas; j++)
                    matriz[i, j] = linhaLida[j]; // salva os dados lidos na matriz
            }
        }

        public void Exibir(DataGridView dgv)
        {
            definirDgv(dgv);
            for (int i = 0; i < matriz.GetLength(0); i++) 
                for (int j = 0; j < matriz.GetLength(1); j++)
                    dgv.Rows[i].Cells[j].Value = matriz[i, j]; // carrega cada linha e coluna do DataGridView de acordo com a matriz


        }

        private void definirDgv(DataGridView dgv)
        {
            dgv.RowCount = matriz.GetLength(0); // define o número de linhas do DataGridView igual ao lido pela matriz
            dgv.ColumnCount = matriz.GetLength(1); // define o número de colunas do DataGridView igual ao lido pela matriz
            dgv.ColumnHeadersVisible = false;
            dgv.RowHeadersVisible = false;
        }
    }
}
