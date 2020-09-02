using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _19169_19185_ED_Lab
{
    public partial class frmForm : Form
    {
        public frmForm()
        {
            InitializeComponent();
        }
        Labirinto labirinto;
        private void btnAbrir_Click(object sender, EventArgs e)
        {            
            if(dlgArquivo.ShowDialog() == DialogResult.OK)
            {
                labirinto = null;
                dgvCaminhos.DataSource = null;
                dgvCaminhos.Rows.Clear();
                dgvCaminhos.Columns.Clear();
                labirinto = new Labirinto(dlgArquivo.FileName); // cria um novo labirinto
                labirinto.Exibir(dgvLabirinto);
            }
        }

        private void dgvLabirinto_Resize(object sender, EventArgs e)
        {
            
        }

        private void btnEncontrar_Click(object sender, EventArgs e)
        {
            btnEncontrar.Enabled = false;
            btnAbrir.Enabled = false;
            labirinto.Andar(dgvLabirinto, dgvCaminhos);
            labirinto.exibirResultados(dgvCaminhos);
            btnEncontrar.Enabled = true;
            btnAbrir.Enabled = true;
        }

        private void dgvCaminhos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            labirinto.MostrarSolucao(dgvCaminhos,e.RowIndex);
        }
        
    }
}
