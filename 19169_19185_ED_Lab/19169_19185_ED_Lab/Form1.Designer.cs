namespace _19169_19185_ED_Lab
{
    partial class frmForm
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmForm));
            this.dgvLabirinto = new System.Windows.Forms.DataGridView();
            this.dgvCaminhos = new System.Windows.Forms.DataGridView();
            this.lbLabirinto = new System.Windows.Forms.Label();
            this.lbCaminhos = new System.Windows.Forms.Label();
            this.btnEncontrar = new System.Windows.Forms.Button();
            this.btnAbrir = new System.Windows.Forms.Button();
            this.dlgArquivo = new System.Windows.Forms.OpenFileDialog();
            this.btnVoltar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLabirinto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCaminhos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLabirinto
            // 
            this.dgvLabirinto.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLabirinto.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvLabirinto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLabirinto.ColumnHeadersVisible = false;
            this.dgvLabirinto.Enabled = false;
            this.dgvLabirinto.Location = new System.Drawing.Point(12, 87);
            this.dgvLabirinto.Name = "dgvLabirinto";
            this.dgvLabirinto.ReadOnly = true;
            this.dgvLabirinto.RowHeadersVisible = false;
            this.dgvLabirinto.Size = new System.Drawing.Size(502, 403);
            this.dgvLabirinto.TabIndex = 0;
            this.dgvLabirinto.Resize += new System.EventHandler(this.dgvLabirinto_Resize);
            // 
            // dgvCaminhos
            // 
            this.dgvCaminhos.AllowUserToAddRows = false;
            this.dgvCaminhos.AllowUserToDeleteRows = false;
            this.dgvCaminhos.AllowUserToResizeColumns = false;
            this.dgvCaminhos.AllowUserToResizeRows = false;
            this.dgvCaminhos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCaminhos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvCaminhos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCaminhos.ColumnHeadersVisible = false;
            this.dgvCaminhos.Location = new System.Drawing.Point(520, 87);
            this.dgvCaminhos.Name = "dgvCaminhos";
            this.dgvCaminhos.ReadOnly = true;
            this.dgvCaminhos.RowHeadersVisible = false;
            this.dgvCaminhos.Size = new System.Drawing.Size(523, 403);
            this.dgvCaminhos.TabIndex = 0;
            this.dgvCaminhos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCaminhos_CellClick);
            // 
            // lbLabirinto
            // 
            this.lbLabirinto.AutoSize = true;
            this.lbLabirinto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLabirinto.Location = new System.Drawing.Point(12, 64);
            this.lbLabirinto.Name = "lbLabirinto";
            this.lbLabirinto.Size = new System.Drawing.Size(70, 20);
            this.lbLabirinto.TabIndex = 2;
            this.lbLabirinto.Text = "Labirinto";
            // 
            // lbCaminhos
            // 
            this.lbCaminhos.AutoSize = true;
            this.lbCaminhos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCaminhos.Location = new System.Drawing.Point(516, 64);
            this.lbCaminhos.Name = "lbCaminhos";
            this.lbCaminhos.Size = new System.Drawing.Size(173, 20);
            this.lbCaminhos.TabIndex = 3;
            this.lbCaminhos.Text = "Caminhos encontrados";
            // 
            // btnEncontrar
            // 
            this.btnEncontrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEncontrar.Location = new System.Drawing.Point(954, 14);
            this.btnEncontrar.Name = "btnEncontrar";
            this.btnEncontrar.Size = new System.Drawing.Size(89, 63);
            this.btnEncontrar.TabIndex = 5;
            this.btnEncontrar.Text = "Encontrar caminhos";
            this.btnEncontrar.UseVisualStyleBackColor = true;
            this.btnEncontrar.Click += new System.EventHandler(this.btnEncontrar_Click);
            // 
            // btnAbrir
            // 
            this.btnAbrir.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbrir.Location = new System.Drawing.Point(859, 14);
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(89, 63);
            this.btnAbrir.TabIndex = 6;
            this.btnAbrir.Text = "Abrir arquivo";
            this.btnAbrir.UseVisualStyleBackColor = true;
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // dlgArquivo
            // 
            this.dlgArquivo.FileName = "openFileDialog1";
            // 
            // btnVoltar
            // 
            this.btnVoltar.Enabled = false;
            this.btnVoltar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoltar.Image = ((System.Drawing.Image)(resources.GetObject("btnVoltar.Image")));
            this.btnVoltar.Location = new System.Drawing.Point(688, 64);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(28, 23);
            this.btnVoltar.TabIndex = 7;
            this.btnVoltar.UseVisualStyleBackColor = true;
            this.btnVoltar.Visible = false;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // frmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 500);
            this.Controls.Add(this.btnVoltar);
            this.Controls.Add(this.btnAbrir);
            this.Controls.Add(this.btnEncontrar);
            this.Controls.Add(this.lbCaminhos);
            this.Controls.Add(this.lbLabirinto);
            this.Controls.Add(this.dgvCaminhos);
            this.Controls.Add(this.dgvLabirinto);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmForm";
            this.Text = "Resolvedor de Labirintos";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLabirinto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCaminhos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLabirinto;
        private System.Windows.Forms.DataGridView dgvCaminhos;
        private System.Windows.Forms.Label lbLabirinto;
        private System.Windows.Forms.Label lbCaminhos;
        private System.Windows.Forms.Button btnEncontrar;
        private System.Windows.Forms.Button btnAbrir;
        private System.Windows.Forms.OpenFileDialog dlgArquivo;
        private System.Windows.Forms.Button btnVoltar;
    }
}

