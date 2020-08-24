using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _19169_19185_ED_Lab
{
    class Labirinto
    {
        private char[,] matriz;
        PilhaLista<Movimento> movimentos;
        public Labirinto(string arquivo)
        {
            lerArquivo(arquivo);
            movimentos = new PilhaLista<Movimento>();
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

        private int[] procurarCaminho(int[] posicaoAtual, DataGridView dgv)
        {
            Movimento direcoes = new Movimento();
            bool moveu = false;
            for(int i = 0; i < direcoes.Direcoes.Length/2; i++)
            {
                int possivelLinha = posicaoAtual[0] + direcoes.Direcoes[i, 0];
                int possivelColuna = posicaoAtual[1] + direcoes.Direcoes[i, 1];
                int[] possivelMovimento = { possivelLinha, possivelColuna };

                if(podeMover(dgv,possivelMovimento))
                {
                    //Thread.Sleep(50);
                    //Application.DoEvents();
                    movimentos.Empilhar(new Movimento(posicaoAtual[0], posicaoAtual[1]));
                    dgv.Rows[posicaoAtual[0]].Cells[posicaoAtual[1]].Style.BackColor = Color.LightGreen; //Pinta a posicao anterior
                    posicaoAtual = possivelMovimento;
                    dgv.Rows[posicaoAtual[0]].Cells[posicaoAtual[1]].Style.BackColor = Color.Green; //Pinta a posicao atual
                    moveu = true;
                    break;
                }
            }
            if (!moveu)
            {
                if (posicaoAtual[0] == 1 && posicaoAtual[1] == 1)
                {
                    MessageBox.Show("Labirinto sem solução", "Sem saída");
                }
                else
                {
                    dgv.Rows[posicaoAtual[0]].Cells[posicaoAtual[1]].Style.BackColor = Color.LightGreen; //Pinta a posicao anterior
                    Movimento ultimoMovimento = movimentos.OTopo();
                    movimentos.Desempilhar();
                    posicaoAtual[0] = ultimoMovimento.Linha;
                    posicaoAtual[1] = ultimoMovimento.Coluna;
                    procurarCaminho(posicaoAtual, dgv);
                }
            }
            return posicaoAtual;
        }

        public void Andar(DataGridView dgv)
        {
            int[] posicaoAtual = { 1, 1 };
            while (matriz[posicaoAtual[0], posicaoAtual[1]] != 'S')
            {
                posicaoAtual = procurarCaminho(posicaoAtual, dgv);

                if (posicaoAtual[0] == 1 && posicaoAtual[1] == 1)
                    break;
               // MessageBox.Show("foi");
            }
        }

        public void Exibir(DataGridView dgv)
        {
            dgv.Rows.Clear();
            definirDgv(dgv);
            for (int i = 0; i < matriz.GetLength(0); i++) 
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    dgv.Rows[i].Cells[j].Value = matriz[i, j]; // carrega cada linha e coluna do DataGridView de acordo com a matriz
                    /*if(matriz[i, j] == '#')
                        dgv.Rows[i].Cells[j].Style.BackColor = Color.LightGray; //Pinta as paredes
                    if (matriz[i, j] == 'I')
                        dgv.Rows[i].Cells[j].Style.BackColor = Color.Green; //Pinta a posicao atual
                    if (matriz[i, j] == 'S')
                        dgv.Rows[i].Cells[j].Style.BackColor = Color.Goldenrod; //Pinta a saida*/
                }
            dgv.CurrentCell = dgv[1, 1];
                   
        }


        private bool podeMover(DataGridView dgv, int[] possivelPosicao)
        {
            int possivelLinha = possivelPosicao[0];
            int possivelColuna = possivelPosicao[1];
            if (matriz[possivelLinha, possivelColuna] == '#')
            {
                dgv.Rows[possivelLinha].Cells[possivelColuna].Style.BackColor = Color.LightGray;
                return false;
            }
            if (dgv.Rows[possivelLinha].Cells[possivelColuna].Style.BackColor == Color.LightGreen)//verifica se já foi nesse espaço
                return false;                                       //e retorna false
             
            return true;
        }

        private void definirDgv( DataGridView dgv)
        {
            dgv.RowCount = matriz.GetLength(0); // define o número de linhas do DataGridView igual ao lido pela matriz
            dgv.ColumnCount = matriz.GetLength(1); // define o número de colunas do DataGridView igual ao lido pela matriz
          /*  int dgvLargura = dgv.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
            int dgvAltura = dgv.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            dgv.Width = dgvLargura + 147;
            dgv.Height = dgvAltura + 47;*/
        }
    }
}
