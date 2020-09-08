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
        private char[,] matriz; //cria a matriz
        PilhaLista<Movimento> movimentos; //cria a pilha que vai guardar os movimentos feitos
        private int qtdSolucoes; //cria um contador de soluções
        private int qtdErros; //cria um contador de erros
        private PilhaLista<Movimento>[] solucoes = new PilhaLista<Movimento>[100]; //cria um vetor de pilhas que vai guardar as soluções
        private Movimento[] erros = new Movimento[100]; //cria um vetor de pilhas que vai guardar os caminhos errados
        private int[] ultimaTentativa = { 0, 0 }; //cria uma variavel que armazena o ultimo movimento realizado
        private bool voltando = false;
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
        public void Andar(DataGridView dgvLabirinto, DataGridView dgvCaminhos)
        {
            int[] posicaoAtual = { 1, 1 }; //define a posição atual como inicio
            bool terminou = false; //cria um bool para saber se terminou o labirinto
            while (matriz[posicaoAtual[0], posicaoAtual[1]] != 'S')
            {
                posicaoAtual = procurarCaminho(posicaoAtual, dgvLabirinto, terminou); //anda um espaço
                terminou = false; //marca que não terminou
                ultimaTentativa[0] = posicaoAtual[0];//atualiza a ultima tentativa
                ultimaTentativa[1] = posicaoAtual[1];
                if (posicaoAtual[0] == 1 && posicaoAtual[1] == 1) //para o programa se voltou ao começo
                    break; 

                if (matriz[posicaoAtual[0], posicaoAtual[1]] == 'S') //verifica se esta na saida
                {
                    if (qtdSolucoes == solucoes.Length) //aumenta o vetor caso esteja no limite
                        aumentar(0);
                    solucoes[qtdSolucoes] = (PilhaLista<Movimento>)movimentos.Clone(); //adiciona o movimento a lista de solucoes
                    solucoes[qtdSolucoes].Empilhar(new Movimento(posicaoAtual[0], posicaoAtual[1])); //adiciona a saida a ultima solucao
                    terminou = true;//marca que terminou
                    voltando = true;
                    qtdSolucoes++; //aumenta o contador de solucoes
                    Movimento aux = movimentos.OTopo();
                    posicaoAtual[0] = aux.Linha;
                    posicaoAtual[1] = aux.Coluna;
                    movimentos.Desempilhar(); //volta um movimento
                }
            }
        }

        private int[] procurarCaminho(int[] posicaoAtual, DataGridView dgv, bool terminou)
        {
            Movimento direcoes = new Movimento();
            bool moveu = false;
            int possivelLinha = 0, possivelColuna = 0;
            for(int i = 0; i < direcoes.Direcoes.Length/2; i++)
            {
                possivelLinha = posicaoAtual[0] + direcoes.Direcoes[i, 0]; 
                possivelColuna = posicaoAtual[1] + direcoes.Direcoes[i, 1];
                int[] possivelMovimento = { possivelLinha, possivelColuna }; //define um possivel movimento

                if (podeMover(dgv,possivelMovimento, posicaoAtual)) //verifica se esse movimento e valido
                {
                    if (!inverte(possivelMovimento, terminou)) //verifica se precisa inveter a ordem entre andar e empilhar
                    {
                        movimentos.Empilhar(new Movimento(posicaoAtual[0], posicaoAtual[1])); //empilha a posicao atual
                        dgv.Rows[posicaoAtual[0]].Cells[posicaoAtual[1]].Style.BackColor = Color.LightGreen; //Pinta a posicao anterior
                        posicaoAtual = possivelMovimento; //atualiza a posicao atual
                        dgv.Rows[posicaoAtual[0]].Cells[posicaoAtual[1]].Style.BackColor = Color.Green; //Pinta a posicao atual
                    }
                    else
                    {
                        posicaoAtual = possivelMovimento; //atualiza a posicao atual
                        movimentos.Empilhar(new Movimento(posicaoAtual[0], posicaoAtual[1])); //empilha a posicao atual
                    }
                    moveu = true; //indica que andou
                    voltando = false;
                    break;                    
                }
            }
            if (!moveu) //verifica se  nao andou
            {
                if (posicaoAtual[0] == 1 && posicaoAtual[1] == 1) //verifica se esta no inicio
                {
                    if(qtdSolucoes == 0) //verifica se ja foi resolvido
                        MessageBox.Show("Labirinto sem solução", "Sem saída"); //avisa que nao tem saida
                    else
                    {
                        MessageBox.Show("Labirinto solucionado com "+ qtdSolucoes+" possíveis soluções", "Finalizou"); //avisa que acabou
                    }
                }
                else
                {
                    //dgv.Rows[posicaoAtual[0]].Cells[posicaoAtual[1]].Value = "#";
                    dgv.Rows[posicaoAtual[0]].Cells[posicaoAtual[1]].Style.BackColor = Color.LightGray; //Pinta a posicao anterior
                    
                    Movimento ultimoMovimento = movimentos.OTopo();
                    movimentos.Desempilhar();
                    ultimaTentativa[0] = posicaoAtual[0];
                    ultimaTentativa[1] = posicaoAtual[1];
                    posicaoAtual[0] = ultimoMovimento.Linha;
                    posicaoAtual[1] = ultimoMovimento.Coluna;
                    if (!voltando && !usou(ultimaTentativa, posicaoAtual))
                    {
                        if (qtdErros == erros.Length) //aumenta o vetor caso esteja no limite
                            aumentar(1);
                        erros[qtdErros] = new Movimento(ultimaTentativa[0], ultimaTentativa[1]); //adiciona o movimento a lista de erros
                        qtdErros++;
                    }
                    voltando = true;
                    int[] aux = procurarCaminho(posicaoAtual, dgv, terminou);

                    if (!movimentos.EstaVazia)
                    {
                        Movimento copia = movimentos.OTopo();
                        if (posicaoAtual[0] == copia.Linha && posicaoAtual[1] == copia.Coluna)
                            posicaoAtual = aux;
                    }
                }
            }
            return posicaoAtual;
        }

        private bool podeMover(DataGridView dgv, int[] possivelPosicao, int[] posicaoAtual)
        {
            int possivelLinha = possivelPosicao[0];
            int possivelColuna = possivelPosicao[1];
            var valorMatriz = dgv.Rows[possivelPosicao[0]].Cells[possivelPosicao[1]].Value.ToString();
            if (valorMatriz == "#") //verifica se e uma parede
                return false;

            if (ultimaTentativa[0] == possivelPosicao[0] && ultimaTentativa[1] == possivelPosicao[1]) //verifica se quer repetir a tentativa anterior
                return false;
            if (foi(possivelPosicao, posicaoAtual))                             //verifica se já foi nesse espaço
                return false;                                      

            return true;
        }

        private bool foi(int[] posicao, int[] posicaoAtual)
        {
            PilhaLista<Movimento> copia;
            for (int i = 0; i < qtdSolucoes; i++)
            {
                PilhaLista<Movimento> aux = (PilhaLista<Movimento>)solucoes[i].Clone();
                aux = inverso(aux); //deixa a pilha indo da posicao inicial ate o fim
                copia = (PilhaLista<Movimento>)movimentos.Clone();
                copia = inverso(copia); //deixa a pilha indo da posicao inicial ate o fim
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
                        diferente = true;
                    aux.Desempilhar();
                }
                if (!aux.EstaVazia && !diferente)
                {
                    movAux = aux.OTopo();
                    if (movAux.Linha == posicao[0] && movAux.Coluna == posicao[1]) //verifica se esse caminho ja foi feito
                        return true;
                }
            }
            copia = (PilhaLista<Movimento>)movimentos.Clone();
            while (!copia.EstaVazia)
            {
                Movimento movAux = copia.OTopo();
                if (movAux.Linha == posicao[0] && movAux.Coluna == posicao[1]) //verifica se ja foi nesse lugar
                    return true;
                copia.Desempilhar();
            }
            for (int i = 0; i < qtdErros; i++)
            {
                if (erros[i].Linha == posicao[0] && erros[i].Coluna == posicao[1]) //verifica se esse caminho ja foi feito
                    return true;
                
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

        public bool inverte(int[] possivelMovimento, bool terminou)
        {
            bool tem = false;
            if (!movimentos.EstaVazia && terminou == true)
            {
                Movimento direcoes = new Movimento();
                int possivelLinha = 0, possivelColuna = 0;
                for (int i = 0; i < direcoes.Direcoes.Length / 2; i++)
                {
                    possivelLinha = possivelMovimento[0] + direcoes.Direcoes[i, 0];
                    possivelColuna = possivelMovimento[1] + direcoes.Direcoes[i, 1];
                    Movimento copia = movimentos.OTopo();
                    if (copia.Linha == possivelLinha && copia.Coluna == possivelColuna)
                    {
                        tem = true;
                        break;
                    }
                }
            }
            return tem;
        }
        
        private bool usou(int[] posicao, int[] posicaoAtual)
        {
            if (posicaoAtual[0] == posicao[0] && posicaoAtual[1] == posicao[1]) //verifica se esta na posicao
                return true;
            PilhaLista<Movimento> copia;
            copia = (PilhaLista<Movimento>)movimentos.Clone();
            while (!copia.EstaVazia)
            {
                Movimento movAux = copia.OTopo();
                if (movAux.Linha == posicao[0] && movAux.Coluna == posicao[1]) //verifica se ja foi nesse lugar
                    return true;
                copia.Desempilhar();
            }
            for (int i = 0; i < qtdErros; i++)
            {
                if (erros[i].Linha == posicao[0] && erros[i].Coluna == posicao[1]) //verifica se ja foi nesse lugar
                    return true;

            }
            for (int i = 0; i < qtdSolucoes; i++)
            {
                PilhaLista<Movimento> aux = (PilhaLista<Movimento>)solucoes[i].Clone();
                Movimento movAux;
                while (!aux.EstaVazia)
                {
                    movAux = aux.OTopo();
                    if (movAux.Linha == posicao[0] && movAux.Coluna == posicao[1])
                    {
                        return true;
                    }
                    aux.Desempilhar();
                }
            }
            return false;
        }

        private void aumentar(int caso)
        {
            if(caso == 0)
            {
                int aumento = solucoes.Length + 100;
                PilhaLista<Movimento>[] a = new PilhaLista<Movimento>[aumento];
                for (int i = 0; i < solucoes.Length; i++)
                {
                    a[i] = solucoes[i];
                }
                solucoes = a;
            }
            else
            {
                int aumento = erros.Length + 100;
                Movimento[] a = new Movimento[aumento];
                for (int i = 0; i < erros.Length; i++)
                {
                    a[i] = erros[i];
                }
                erros = a;
            }
        }

        public void MostrarSolucao(DataGridView dgv, int solucao)
        {
            limparDgv(dgv);
            definirDgv(dgv);
            for (int i = 0; i < matriz.GetLength(0); i++)
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    dgv.Rows[i].Cells[j].Value = matriz[i, j]; // carrega cada linha e coluna do DataGridView de acordo com a matriz
                    if (matriz[i, j] == 'S')
                        dgv.Rows[i].Cells[j].Style.BackColor = Color.Goldenrod; //Pinta a saida
                }
            PilhaLista<Movimento> copia = (PilhaLista<Movimento>) solucoes[solucao].Clone();
            while (!copia.EstaVazia)
            {
                Movimento aux = copia.OTopo();
                int linha = aux.Linha;
                int coluna = aux.Coluna;
                dgv.Rows[linha].Cells[coluna].Style.BackColor = Color.LightGreen;
                copia.Desempilhar();
            }
            dgv.Enabled = false;
        }
        
        public void Exibir(DataGridView dgv)
        {
            dgv.Rows.Clear();
            definirDgv(dgv);
            for (int i = 0; i < matriz.GetLength(0); i++) 
                for (int j = 0; j < matriz.GetLength(1); j++)                
                    dgv.Rows[i].Cells[j].Value = matriz[i, j]; // carrega cada linha e coluna do DataGridView de acordo com a matriz
            dgv.CurrentCell = dgv[1, 1];
                   
        }
        public void exibirResultados(DataGridView dgv)
        {
            limparDgv(dgv);
            dgv.Enabled = true;
            dgv.RowCount = qtdSolucoes;
            foreach(DataGridViewRow linha in dgv.Rows)            
                linha.Cells[0].Value = "Solução " + (linha.Index+1);            
        }        
        

        private void definirDgv(DataGridView dgv)
        {
            dgv.RowCount = matriz.GetLength(0); // define o número de linhas do DataGridView igual ao lido pela matriz
            dgv.ColumnCount = matriz.GetLength(1); // define o número de colunas do DataGridView igual ao lido pela matriz
        }

        private void limparDgv(DataGridView dgv)
        {
            dgv.CurrentCell = null;
            dgv.DataSource = null;
            dgv.Rows.Clear();
            dgv.Columns.Clear();
        }
    }
}
