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
        private int qtdSolucoes;
        private PilhaLista<Movimento>[] solucoes = new PilhaLista<Movimento>[100];
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

                if(podeMover(dgv,possivelMovimento, posicaoAtual))
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
                    if(qtdSolucoes == 0)
                        MessageBox.Show("Labirinto sem solução", "Sem saída");
                    else
                    {
                        MessageBox.Show("Labirinto solucionado com "+ qtdSolucoes+" possíveis soluções", "Finalizou");
                    }
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
                    solucoes[qtdSolucoes] = (PilhaLista<Movimento>)movimentos.Clone();
                    solucoes[qtdSolucoes].Empilhar(new Movimento(posicaoAtual[0], posicaoAtual[1]));
                    qtdSolucoes++;
                    Movimento aux = movimentos.OTopo();
                    posicaoAtual[0] = aux.Linha;
                    posicaoAtual[1] = aux.Coluna;
                    movimentos.Desempilhar();
                }
                // MessageBox.Show("foi");
            }
        }

        private void MostrarSolucao(DataGridView dgv)
        {
            if (qtdSolucoes == 0)
                definirDgv(dgv);
            else
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


        private bool podeMover(DataGridView dgv, int[] possivelPosicao, int[] posicaoAtual)
        {
            int possivelLinha = possivelPosicao[0];
            int possivelColuna = possivelPosicao[1];
            var valorMatriz = dgv.Rows[possivelPosicao[0]].Cells[possivelPosicao[1]].Value.ToString();
            if (valorMatriz == "#")            
                return false;
            
            if (foi(possivelPosicao, posicaoAtual))                             //verifica se já foi nesse espaço
                return false;                                      //e retorna false
             
            return true;
        }

        private bool foi (int[] posicao, int[] posicaoAtual)
        {

            PilhaLista<Movimento> copia;
            for (int i = 0; i < qtdSolucoes; i++)
            {

                PilhaLista<Movimento> aux = (PilhaLista<Movimento>)solucoes[i].Clone();
                aux = inverso(aux);
                copia = (PilhaLista<Movimento>)movimentos.Clone();
                copia = inverso(copia);
                Movimento movAux;
                bool diferente = false;
                while (!aux.EstaVazia && !copia.EstaVazia)
                {
                    Movimento mov = copia.OTopo();
                    movAux = aux.OTopo();
                    if (movAux.Linha != mov.Linha || movAux.Coluna != mov.Coluna)
                    {
                        diferente = true;
                        break;
                    }
                    aux.Desempilhar();
                    copia.Desempilhar();
                }
                if (!aux.EstaVazia && !diferente)
                {
                    movAux = aux.OTopo();
                    if (movAux.Linha != posicaoAtual[0] && movAux.Coluna != posicaoAtual[1])
                        return false;
                    aux.Desempilhar();
                }
                if (!aux.EstaVazia && !diferente)
                {
                    movAux = aux.OTopo();
                    if (movAux.Linha == posicao[0] && movAux.Coluna == posicao[1])
                        return true;
                }
            }    
            copia = (PilhaLista<Movimento>)movimentos.Clone();
            while (!copia.EstaVazia)
            {
                Movimento movAux = copia.OTopo();
                if (movAux.Linha == posicao[0] && movAux.Coluna == posicao[1])
                    return true;
                copia.Desempilhar();
            }
            
            return false;
        }
        public PilhaLista<Movimento> inverso(PilhaLista<Movimento> pilha)
        {
            PilhaLista<Movimento> aux = new PilhaLista<Movimento>();
            while (!pilha.EstaVazia)
            {
                aux.Empilhar(pilha.OTopo());
                pilha.Desempilhar();
            }
            return aux;
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
