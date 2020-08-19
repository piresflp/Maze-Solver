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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {            
            if(dlgArquivo.ShowDialog() == DialogResult.OK)
            {
                Labirinto labirinto = new Labirinto(dlgArquivo.FileName); // cria um novo labirinto
                //labirinto.Exibir(dgvLabirinto);
            }
        }
    }
}
