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
        int qtdSolucoes;
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
            int possivelLinha = 0, possivelColuna = 0;
            for(int i = 0; i < direcoes.Direcoes.Length/2; i++)
            {
                possivelLinha = posicaoAtual[0] + direcoes.Direcoes[i, 0];
                possivelColuna = posicaoAtual[1] + direcoes.Direcoes[i, 1];
                int[] possivelMovimento = { possivelLinha, possivelColuna };

                if(podeMover(dgv,possivelMovimento))
                {
                    
                    movimentos.Empilhar(new Movimento(posicaoAtual[0], posicaoAtual[1]));
                    dgv.Rows[posicaoAtual[0]].Cells[posicaoAtual[1]].Style.BackColor = Color.LightGreen; //Pinta a posicao anterior
                    posicaoAtual = possivelMovimento;
                    dgv.Rows[posicaoAtual[0]].Cells[posicaoAtual[1]].Style.BackColor = Color.Green; //Pinta a posicao atual
                    moveu = true;
                    Thread.Sleep(50);
                    Application.DoEvents();
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
                    dgv.Rows[posicaoAtual[0]].Cells[posicaoAtual[1]].Value = "#";
                    dgv.Rows[posicaoAtual[0]].Cells[posicaoAtual[1]].Style.BackColor = Color.LightGray; //Pinta a posicao anterior
                    Movimento ultimoMovimento = movimentos.OTopo();
                    movimentos.Desempilhar();
                    posicaoAtual[0] = ultimoMovimento.Linha;
                    posicaoAtual[1] = ultimoMovimento.Coluna;
                    procurarCaminho(posicaoAtual, dgv);
                    Application.DoEvents();
                }
            }
            return posicaoAtual;
        }

        public void Andar(DataGridView dgvLabirinto, DataGridView dgvCaminhos)
        {
            int[] posicaoAtual = { 1, 1 };
            while (matriz[posicaoAtual[0], posicaoAtual[1]] != 'S')
            {
                posicaoAtual = procurarCaminho(posicaoAtual, dgvLabirinto);

                if (posicaoAtual[0] == 1 && posicaoAtual[1] == 1)
                    break;

                if (matriz[posicaoAtual[0], posicaoAtual[1]] == 'S')
                {
                    MostrarSolucao(dgvCaminhos);
                    qtdSolucoes++;
                }
                // MessageBox.Show("foi");
            }
        }

        private void MostrarSolucao(DataGridView dgv)
        {
            definirDgv(dgv);
            if (qtdSolucoes > 0)
                aumentarDgv(dgv, matriz.GetLength(0) + 1);
            for (int i = 0; i < matriz.GetLength(0); i++)
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    int linha = qtdSolucoes * matriz.GetLength(0) + qtdSolucoes + i;
                    dgv.Rows[linha].Cells[j].Value = matriz[i, j]; // carrega cada linha e coluna do DataGridView de acordo com a matriz
                    if (matriz[i, j] == 'S')
                        dgv.Rows[linha].Cells[j].Style.BackColor = Color.Goldenrod; //Pinta a saida
                }
            PilhaLista<Movimento> copia = (PilhaLista<Movimento>) movimentos.Clone();
            while (!copia.EstaVazia)
            {
                Movimento aux = copia.OTopo();
                int linha = qtdSolucoes * matriz.GetLength(0) + qtdSolucoes + aux.Linha;
                int coluna = aux.Coluna;
                dgv.Rows[linha].Cells[coluna].Style.BackColor = Color.LightGreen;
                copia.Desempilhar();
            }
        }
        private void aumentarDgv(DataGridView dgv, int aumento)
        {
            dgv.RowCount += aumento; //Aumenta as linhas do DataGridView
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
            var valorMatriz = dgv.Rows[possivelPosicao[0]].Cells[possivelPosicao[1]].Value.ToString();
            if (valorMatriz == "#")            
                return false;
            
            if (dgv.Rows[possivelLinha].Cells[possivelColuna].Style.BackColor == Color.LightGreen)//verifica se já foi nesse espaço
                return false;                                      //e retorna false
             
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
