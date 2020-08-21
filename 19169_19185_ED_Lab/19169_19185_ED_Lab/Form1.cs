﻿using System;
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

        private void btnAbrir_Click(object sender, EventArgs e)
        {            
            if(dlgArquivo.ShowDialog() == DialogResult.OK)
            {
                Labirinto labirinto = new Labirinto(dlgArquivo.FileName); // cria um novo labirinto
                labirinto.Exibir(dgvLabirinto);
            }
        }

        private void dgvLabirinto_Resize(object sender, EventArgs e)
        {
            
        }

        private void btnEncontrar_Click(object sender, EventArgs e)
        {            

        }
    }
}